using UnityEditor.Rendering;
using UnityEngine;

public class Offset
{
    public int temporary = 0;
    public int _temporary = 0;
    public int i = 0;

    public Offset()
    {
        this.Start();
    }
    public void Start()
    {
        this.temporary = 0;
        this._temporary = 0;

        this.i = 0;
    }
    public void Update(Factory factory, Combo combo, Bot bot)
    {
        if (combo.b)
        {
            this.temporary += combo.i;
        }
        if (combo.end.Finish())
        {
            this.i += this.temporary;
            this.temporary = 0;
        }

        if (bot.combo.b)
        {
            this._temporary += bot.combo.i;
        }
        if (bot.combo.end.Finish())
        {
            this.i -= this._temporary;
            this._temporary = 0;
        }

        if (this.i > 0)
        {
            if (bot.combo.i != 0) return;
            bot.health -= this.i;
            this.i = 0;
        }
        else if (this.i < 0)
        {
            if (combo.i != 0) return;
            factory.NewGarbagePuyo(-i);
            this.i = 0;
        }
    }
}