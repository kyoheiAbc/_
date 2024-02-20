using System.Collections.Generic;
using UnityEngine;
public class RotatePuyoPuyo
{
    PuyoPuyo puyoPuyo;
    public RotatePuyoPuyo(PuyoPuyo puyoPuyo)
    {
        this.puyoPuyo = puyoPuyo;
    }
    public int Get()
    {
        if (this.puyoPuyo.array[0].position == this.puyoPuyo.array[1].position + Vector2.left) return 0;
        if (this.puyoPuyo.array[0].position == this.puyoPuyo.array[1].position + Vector2.up) return 1;
        if (this.puyoPuyo.array[0].position == this.puyoPuyo.array[1].position + Vector2.right) return 2;
        if (this.puyoPuyo.array[0].position == this.puyoPuyo.array[1].position + Vector2.down) return 3;
        return -1;
    }
    public void Execute(List<Puyo> list)
    {
        int rotate = Get();
        rotate++;
        if (rotate == 4) rotate = 0;

        Vector2 position = this.puyoPuyo.array[0].position;
        this.puyoPuyo.array[1].position = position;

        if (rotate == 0)
            this.puyoPuyo.array[1].movePuyo.Execute(Vector2.right, list);
        else if (rotate == 1)
            this.puyoPuyo.array[1].movePuyo.Execute(Vector2.down, list);
        else if (rotate == 2)
            this.puyoPuyo.array[1].movePuyo.Execute(Vector2.left, list);
        else if (rotate == 3)
            this.puyoPuyo.array[1].movePuyo.Execute(Vector2.up, list);

        this.puyoPuyo.Sync(1, rotate);

        if (Collision.Get(this.puyoPuyo.array[0], list) != null)
        {
            rotate++;
            if (rotate == 4) rotate = 0;

            this.puyoPuyo.array[1].position = position;
            this.puyoPuyo.Sync(1, rotate);
        }
    }
}