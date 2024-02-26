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
    private int _i = 0;
    public bool b = false;
    public Count end = new Count(60);
    public void Start()
    {
        this.i = 0;
        this.end.i = 0;
        this._i = this.i;
        this.b = false;
    }
    public void Add(int i)
    {
        if (i == 0) return;
        new _Combo(this, i);
        this.end.i = 0;
    }
    public void Update()
    {
        this.b = this.i != this._i;
        this._i = this.i;
        this.end.Update();
        if (this.end.Finish()) this.i = 0;
    }

}