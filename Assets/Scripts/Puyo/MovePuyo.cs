using System.Collections.Generic;
using UnityEngine;
public class MovePuyo
{
    Puyo puyo;
    public MovePuyo(Puyo puyo)
    {
        this.puyo = puyo;
    }
    public Vector2 Execute(Vector2 v, List<Puyo> list)
    {
        Vector2 position = this.puyo.position;
        _Execute(v, list);
        return this.puyo.position - position;
    }
    private void _Execute(Vector2 v, List<Puyo> list)
    {
        Vector2 _position = this.puyo.position;
        this.puyo.position += v;

        Puyo c = Collision.Get(this.puyo, list);

        if (c == null) return;

        if (v.y != 0)
        {
            this.puyo.position.y = c.position.y - Mathf.Sign(v.y);
            return;
        }

        if (v.x != 0)
        {
            float y = c.position.y;
            if (Mathf.Abs(this.puyo.position.y - y) > 0.5f)
            {
                this.puyo.position.y = y + Mathf.Sign(this.puyo.position.y - y);
                if (Collision.Get(puyo, list) == null) return;
            }
        }
        this.puyo.position = _position;
    }
}