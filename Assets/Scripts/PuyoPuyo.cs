using System.Collections.Generic;
using UnityEngine;

public class PuyoPuyo
{
    private Puyo[] puyo;
    public PuyoPuyo(Puyo p0, Puyo p1)
    {
        this.puyo = new Puyo[] { p0, p1 };
        this.puyo[0].SetPuyoPuyo(this);
        this.puyo[1].SetPuyoPuyo(this);
    }
    public Vector2 Move(Vector2 v)
    {
        Vector2 p = this.puyo[0].GetPosition();
        for (int i = 0; i < 2; i++)
        {
            if (this.puyo[i].Move(v) != v)
            {
                this.sync(i);
                if (this.puyo[1 - i].isColliding())
                {
                    this.puyo[0].SetPosition(p);
                    this.sync(0);
                }
                break;
            }
        }
        return this.puyo[0].GetPosition() - p;
    }


    private void sync(int i)
    {
        this.puyo[1 - i].SetPosition(
            this.puyo[i].GetPosition() + Vector2.right * (1 - 2 * i)
        );
    }

}
