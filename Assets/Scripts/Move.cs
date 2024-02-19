using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    static public Vector2 Puyo(Puyo puyo, Vector2 v, List<Puyo> list)
    {
        Vector2 position = puyo.position;
        _Puyo(puyo, v, list);
        return puyo.position - position;
    }

    static private void _Puyo(Puyo puyo, Vector2 v, List<Puyo> list)
    {

        Vector2 _position = puyo.position;
        puyo.position += v;

        Puyo p = Collision.Get(puyo, list);

        if (p == null) return;

        if (v.y != 0)
        {
            puyo.position.y = p.GetPosition().y - Mathf.Sign(v.y);
            return;
        }

        if (v.x != 0)
        {
            float y = p.GetPosition().y;
            if (Mathf.Abs(puyo.position.y - y) > 0.5f)
            {
                puyo.position.y = y + Mathf.Sign(puyo.position.y - y);
                if (Collision.Get(puyo, list) == null) return;
            }
        }

        puyo.position = _position;
    }

    static public Vector2 PuyoPuyo_(PuyoPuyo puyoPuyo, Vector2 v, List<Puyo> list)
    {
        Vector2 position = puyoPuyo.GetPosition();
        _PuyoPuyo(puyoPuyo, v, list);
        return puyoPuyo.GetPosition() - position;
    }
    static private void _PuyoPuyo(PuyoPuyo puyoPuyo, Vector2 v, List<Puyo> list)
    {
        Vector2 p = puyoPuyo.array[0].GetPosition();
        int rotate = Rotate.Get(puyoPuyo);

        int[] a = new int[] { 0, 1 };
        if (Rotate.Get(puyoPuyo) == 0 && v.x > 0) a = new int[] { 1, 0 };
        if (Rotate.Get(puyoPuyo) == 1 && v.y < 0) a = new int[] { 1, 0 };
        if (Rotate.Get(puyoPuyo) == 2 && v.x < 0) a = new int[] { 1, 0 };
        if (Rotate.Get(puyoPuyo) == 3 && v.y > 0) a = new int[] { 1, 0 };

        foreach (int i in a)
        {
            if (Move.Puyo(puyoPuyo.array[i], v, list) != v)
            {
                PuyoPuyo.Sync(puyoPuyo, i, rotate);
                if (Collision.Get(puyoPuyo.array[1 - i], list) != null)
                {

                    puyoPuyo.array[0].SetPosition(p);
                    PuyoPuyo.Sync(puyoPuyo, 0, rotate);
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
