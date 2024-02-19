using System.Collections.Generic;
using UnityEngine;

public class Rotate
{
    static public int Get(PuyoPuyo puyoPuyo)
    {
        if (puyoPuyo.array[0].GetPosition() == puyoPuyo.array[1].GetPosition() + Vector2.left) return 0;
        if (puyoPuyo.array[0].GetPosition() == puyoPuyo.array[1].GetPosition() + Vector2.up) return 1;
        if (puyoPuyo.array[0].GetPosition() == puyoPuyo.array[1].GetPosition() + Vector2.right) return 2;
        if (puyoPuyo.array[0].GetPosition() == puyoPuyo.array[1].GetPosition() + Vector2.down) return 3;
        return -1;
    }

    static public void Execute(PuyoPuyo puyoPuyo, List<Puyo> list)
    {
        int rotate = Get(puyoPuyo);
        rotate++;
        if (rotate == 4) rotate = 0;

        Vector2 p = puyoPuyo.array[0].GetPosition();
        puyoPuyo.array[1].SetPosition(p);

        if (rotate == 0)
            Move.Puyo(puyoPuyo.array[1], Vector2.right, list);
        else if (rotate == 1)
            Move.Puyo(puyoPuyo.array[1], Vector2.down, list);

        else if (rotate == 2)
            Move.Puyo(puyoPuyo.array[1], Vector2.left, list);
        else if (rotate == 3)
            Move.Puyo(puyoPuyo.array[1], Vector2.up, list);


        PuyoPuyo.Sync(puyoPuyo, 1, rotate);

        if (Collision.Get(puyoPuyo.array[0], list) != null)
        {
            rotate++;
            if (rotate == 4) rotate = 0;

            puyoPuyo.array[1].SetPosition(p);
            PuyoPuyo.Sync(puyoPuyo, 1, rotate);
        }
    }

}