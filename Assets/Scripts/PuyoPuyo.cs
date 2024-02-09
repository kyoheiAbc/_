using System.Collections.Generic;
using UnityEngine;

public class PuyoPuyo
{
    private Puyo[] array;
    private int i = 0;
    public int GetI() { return this.i; }

    public PuyoPuyo(Puyo p0, Puyo p1)
    {
        this.array = new Puyo[] { p0, p1 };
        p0.SetPuyoPuyo(this);
        p1.SetPuyoPuyo(this);
    }

    public void Update(Collision c)
    {
        if (this.i < 256) this.i++;

        if (this.Move(0.1f * Vector2.down, c) != Vector2.zero)
        {
            this.i = 0;
        }

        if (this.i == 30)
        {
            this.array[0].SetPuyoPuyo(null);
            this.array[1].SetPuyoPuyo(null);
            this.array[0] = null;
            this.array[1] = null;
        }
    }

    public Vector2 Move(Vector2 v, Collision c)
    {
        Vector2 p = this.array[0].GetPosition();


        for (int i = 0; i < 2; i++)
        {
            if (this.array[i].Move(v, c) != v)
            {
                this.Sync(i);
                if (c.IfCollision(this.array[1 - i]))
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
