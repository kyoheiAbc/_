using System.Collections.Generic;
using UnityEngine;
public class Combo
{
    public int i = 0;
    public Count end = new Count(Static.COMBO);
    List<Child> list = new List<Child>();
    public bool update = false;

    public void Add(int i)
    {
        if (i == 0) return;
        list.Add(new Child(i));
        end = new Count(Static.COMBO);
    }
    public void Update()
    {
        this.end.Update();
        update = false;
        for (int i = list.Count - 1; i >= 0; i--)
        {
            list[i].Update();
            if (list[i].GetProgress() == 1)
            {
                this.i += list[i].i;
                list.Remove(list[i]);
                update = true;
            }
        }
        if (this.end.GetProgress() == 1)
        {
            if (this.i > 0) update = true;
            this.i = 0;
        }
    }
    private class Child : Count
    {
        public readonly int i;
        public Child(int i) : base(30)
        {
            this.i = i;
            base.Start();
        }
    }
}