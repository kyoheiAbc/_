using System.Collections.Generic;
using UnityEngine;
public class Combo
{
    private class _Combo : CustomGameObject
    {
        private Combo parent;
        int i;
        public _Combo(Combo parent, int i) : base(30)
        {
            this.parent = parent;
            this.i = i;
        }
        public override void End()
        {
            this.parent.i += this.i;
        }
    }
    public int i = 0;
    public Count end = new Count(120);
    public void Start()
    {
        this.i = 0;
        this.end.i = 0;
    }
    public void Add(int i)
    {
        new _Combo(this, i);
    }
    public void Update()
    {
        this.end.Update();
        if (this.end.Finish()) this.i = 0;
    }

}