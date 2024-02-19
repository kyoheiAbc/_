using System.Collections.Generic;
using UnityEngine;

public class Puyo
{
    public Vector2 position;
    public Vector2 GetPosition() { return this.position; }
    public void SetPosition(Vector2 p) { this.position = p; }
    private int color;
    public int GetColor() { return this.color; }
    public Count freeze = new Count();
    public Count fire = new Count();
    private readonly Vector2 DOWN = 0.2f * Vector2.down;
    public MovePuyo movePuyo;

    public Puyo(int color, Vector2 position)
    {
        this.position = position;
        this.color = color;
        this.movePuyo = new MovePuyo(this);
    }
    public void Update(List<Puyo> list)
    {
        freeze.Update();
        fire.Update();
        if (this.position.x == 0.5f || this.position.x == 7.5f || this.position.y == 0.5f || this.position.y == 15.5f)
        {
            this.freeze.Start();
            return;
        }

        if (this.movePuyo.Execute(this.DOWN, list) == this.DOWN)
        {
            this.freeze.i = 0;
        }
        else
        {
            this.freeze.Start();
        }
    }


}

