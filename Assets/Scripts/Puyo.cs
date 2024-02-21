using System.Collections.Generic;
using UnityEngine;
public class Puyo
{
    public Vector2 position;
    public int color;
    public Count freeze = new Count(30);
    public Count fire = new Count(30);
    private static readonly Vector2 DOWN = Vector2.down * 0.3f;
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
        if (this.movePuyo.Execute(Puyo.DOWN, list) == Vector2.zero)
        {
            this.freeze.Start();
        }
        else
        {
            this.freeze.i = 0;
        }
    }
}