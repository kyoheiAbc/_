using System.Collections.Generic;
using UnityEngine;
public class Combo
{
    public int i = 0;
    private int j = 0;
    public Count end;
    public bool update;
    public void Start()
    {
        this.i = 0;
        this.j = this.i;
        this.end = new Count(Static.COMBO);
        this.update = false;
    }
    public void Add(int i)
    {
        if (i == 0) return;
        new _combo(this, i);
        this.end = new Count(Static.COMBO);
    }
    public void Update()
    {
        this.end.Update();
        if (this.end.Finish()) this.i = 0;
        this.update = this.i != this.j;
        this.j = this.i;
    }
    private class _combo : CustomGameObject
    {
        readonly private Combo parent;
        readonly private int combo;
        public _combo(Combo parent, int i) : base(Puyo.FIRE)
        {
            this.parent = parent;
            this.combo = i;
        }
        public override void End()
        {
            this.parent.i += this.combo;
        }
    }
}