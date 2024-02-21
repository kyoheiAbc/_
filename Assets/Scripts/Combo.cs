using System.Collections.Generic;
using UnityEngine;
public class Combo
{
    public int i;
    public Count end = new Count(120);
    private List<Buffer> list = new List<Buffer>();
    public Combo()
    {
        this.Start();
    }
    public void Start()
    {
        this.i = 0;
        this.end = new Count(120);
        this.list.Clear();
    }
    public void Update()
    {
        this.end.Update();

        if (this.end.Finish()) this.i = 0;

        for (int i = this.list.Count - 1; i >= 0; i--)
        {
            this.list[i].Update();
        }
    }
    public void Add(int i, int frame)
    {
        this.list.Add(new Buffer(this, i, frame));
        this.end.i = 0;
    }
    private class Buffer
    {
        private Combo parent;
        private int i;
        private int delay;
        public Buffer(Combo parent, int i, int delay)
        {
            this.parent = parent;
            this.i = i;
            this.delay = delay;
        }
        public void Update()
        {
            this.delay--;
            if (this.delay == 0)
            {
                this.parent.i += this.i;
                this.parent.list.Remove(this);
            }
        }
    }
}