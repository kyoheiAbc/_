using System.Collections.Generic;
using UnityEngine;
public class MovePuyoPuyo
{
    PuyoPuyo puyoPuyo;
    public MovePuyoPuyo(PuyoPuyo puyoPuyo)
    {
        this.puyoPuyo = puyoPuyo;
    }
    public Vector2 Execute(Vector2 v, List<Puyo> list)
    {
        Vector2 position = this.puyoPuyo.GetPosition();
        _Execute(v, list);
        Vector2 p = this.puyoPuyo.GetPosition() - position;
        if (v == Vector2.down && p == Vector2.zero) this.puyoPuyo.disconnect.i = this.puyoPuyo.disconnect.I;
        return p;
    }
    private void _Execute(Vector2 v, List<Puyo> list)
    {
        Vector2 position = this.puyoPuyo.array[0].position;
        int rotate = this.puyoPuyo.rotatePuyoPuyo.Get();

        int[] a = new int[] { 0, 1 };
        if (this.puyoPuyo.rotatePuyoPuyo.Get() == 0 && v.x > 0) a = new int[] { 1, 0 };
        if (this.puyoPuyo.rotatePuyoPuyo.Get() == 1 && v.y < 0) a = new int[] { 1, 0 };
        if (this.puyoPuyo.rotatePuyoPuyo.Get() == 2 && v.x < 0) a = new int[] { 1, 0 };
        if (this.puyoPuyo.rotatePuyoPuyo.Get() == 3 && v.y > 0) a = new int[] { 1, 0 };

        foreach (int i in a)
        {
            if (this.puyoPuyo.array[i].movePuyo.Execute(v, list) != v)
            {
                this.puyoPuyo.Sync(i, rotate);
                if (Collision.Get(this.puyoPuyo.array[1 - i], list) != null)
                {
                    this.puyoPuyo.array[0].position = position;
                    this.puyoPuyo.Sync(0, rotate);
                }
                else break;
            }
        }
    }
}