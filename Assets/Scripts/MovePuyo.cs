using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovePuyo
{
    Puyo puyo;

    public MovePuyo(Puyo puyo)
    {
        puyo = this.puyo;
    }

    public Vector2 Execute(Vector2 v, List<Puyo> list)
    {
        Vector2 position = puyo.position;
        _Execute(v, list);
        return puyo.position - position;
    }

    private void _Execute(Vector2 v, List<Puyo> list)
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

}
