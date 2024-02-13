using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PuyoPuyo
{
    private Puyo[] array;
    public Puyo[] GetArray() { return this.array; }
    private int i = 0;

    public PuyoPuyo(Puyo p0, Puyo p1)
    {
        this.array = new Puyo[] { p0, p1 };
        p0.SetPuyoPuyo(this);
        p1.SetPuyoPuyo(this);
    }
    public Vector2 GetPosition()
    {
        return 0.5f * (this.array[0].GetPosition() + this.array[1].GetPosition());
    }
    public void Update(Collision c)
    {
        if (this.i < 256) this.i++;


        if (this.Move(Main.DOWN, c) != Vector2.zero)
        {
            this.i = 0;
        }

        if (this.i == Main.BREAK)
        {
            this.array[0].SetPuyoPuyo(null);
            this.array[1].SetPuyoPuyo(null);
            this.array = new Puyo[] { null, null };
        }

    }

    public Vector2 Move(Vector2 v, Collision c)
    {
        Vector2 p = this.array[0].GetPosition();

        if (v == Vector2.right)
        {
            this.array[1].Move(v, c);
            this.Sync(1);
            if (c.Get(this.array[0]) != null)
            {
                this.array[0].SetPosition(p);
                this.Sync(0);
            }
            return this.array[0].GetPosition() - p;
        }

        for (int i = 0; i < 2; i++)
        {
            if (this.array[i].Move(v, c) != v)
            {
                this.Sync(i);
                if (c.Get(this.array[1 - i]) != null)
                {

                    this.array[0].SetPosition(p);
                    this.Sync(0);
                }
                else
                {
                    break;
                }
            }
        }
        return this.array[0].GetPosition() - p;
    }

    private void Sync(int i)
    {
        this.array[1 - i].SetPosition(this.array[i].GetPosition() + Vector2.right * (1 - 2 * i));
    }
}
