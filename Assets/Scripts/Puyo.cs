using System.Collections.Generic;
using UnityEngine;
public class Puyo
{
    public static readonly int FIRE = 30;
    public static readonly int FREEZE = 20;
    private static readonly Vector2 DOWN = Vector2.down * 0.2f;
    public Vector2 position;
    public int color;
    public Count freeze = new Count(Puyo.FREEZE);
    public Count fire = new Count(Puyo.FIRE);
    public MovePuyo movePuyo;
    public Puyo(int color, Vector2 position)
    {
        this.position = position;
        this.color = color;
        this.movePuyo = new MovePuyo(this);
    }
    public void Update(List<Puyo> list)
    {
        this.freeze.Update();
        this.fire.Update();
        if (this.position.x == 0.5f || this.position.x == 7.5f || this.position.y == 0.5f || this.position.y == 15.5f)
        {
            this.freeze.Start();
            return;
        }
        if (this.fire.GetProgress() == 1) return;
        if (this.movePuyo.Execute(Puyo.DOWN, list) == Vector2.zero)
        {
            this.freeze.Start();
        }
        else
        {
            this.freeze = new Count(Puyo.FREEZE);
        }
    }
}

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
        _execute(v, list);
        return this.puyo.position - position;
    }
    private void _execute(Vector2 v, List<Puyo> list)
    {
        Vector2 p = this.puyo.position;
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
        this.puyo.position = p;
    }
}