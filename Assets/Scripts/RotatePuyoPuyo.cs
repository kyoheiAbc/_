using System.Collections;
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
        if (puyoPuyo.array[0].GetPosition() == puyoPuyo.array[1].GetPosition() + Vector2.left) return 0;
        if (puyoPuyo.array[0].GetPosition() == puyoPuyo.array[1].GetPosition() + Vector2.up) return 1;
        if (puyoPuyo.array[0].GetPosition() == puyoPuyo.array[1].GetPosition() + Vector2.right) return 2;
        if (puyoPuyo.array[0].GetPosition() == puyoPuyo.array[1].GetPosition() + Vector2.down) return 3;
        return -1;
    }

    public void Execute(List<Puyo> list)
    {
        int rotate = Get();
        rotate++;
        if (rotate == 4) rotate = 0;

        Vector2 p = puyoPuyo.array[0].GetPosition();
        puyoPuyo.array[1].SetPosition(p);

        if (rotate == 0)
            puyoPuyo.array[1].movePuyo.Execute(Vector2.right, list);
        else if (rotate == 1)
            puyoPuyo.array[1].movePuyo.Execute(Vector2.down, list);
        else if (rotate == 2)
            puyoPuyo.array[1].movePuyo.Execute(Vector2.left, list);
        else if (rotate == 3)
            puyoPuyo.array[1].movePuyo.Execute(Vector2.up, list);


        puyoPuyo.Sync(1, rotate);

        if (Collision.Get(puyoPuyo.array[0], list) != null)
        {
            rotate++;
            if (rotate == 4) rotate = 0;

            puyoPuyo.array[1].SetPosition(p);
            puyoPuyo.Sync(1, rotate);
        }
    }

}
