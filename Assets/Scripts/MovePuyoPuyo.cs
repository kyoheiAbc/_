using System.Collections;
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
        Vector2 position = puyoPuyo.GetPosition();
        _Execute(v, list);
        Debug.Log("b");

        return puyoPuyo.GetPosition() - position;
    }
    private void _Execute(Vector2 v, List<Puyo> list)
    {
        Debug.Log("a");
        Vector2 p = puyoPuyo.array[0].GetPosition();
        int rotate = puyoPuyo.rotatePuyoPuyo.Get();

        int[] a = new int[] { 0, 1 };
        if (puyoPuyo.rotatePuyoPuyo.Get() == 0 && v.x > 0) a = new int[] { 1, 0 };
        if (puyoPuyo.rotatePuyoPuyo.Get() == 1 && v.y < 0) a = new int[] { 1, 0 };
        if (puyoPuyo.rotatePuyoPuyo.Get() == 2 && v.x < 0) a = new int[] { 1, 0 };
        if (puyoPuyo.rotatePuyoPuyo.Get() == 3 && v.y > 0) a = new int[] { 1, 0 };

        foreach (int i in a)
        {
            if (puyoPuyo.array[i].movePuyo.Execute(v, list) != v)
            {
                puyoPuyo.Sync(i, rotate);
                if (Collision.Get(puyoPuyo.array[1 - i], list) != null)
                {

                    puyoPuyo.array[0].SetPosition(p);
                    puyoPuyo.Sync(0, rotate);
                }
                else
                {
                    break;
                }
            }
        }
        return;
    }


}