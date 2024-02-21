using System.Collections.Generic;
using UnityEngine;

public class PuyoPuyo
{
    public Puyo[] array;
    public Count disconnect = new Count(60);
    private static readonly Vector2 DOWN = Vector2.down * 0.02f;
    public MovePuyoPuyo movePuyoPuyo;
    public RotatePuyoPuyo rotatePuyoPuyo;
    public PuyoPuyo(Puyo p0, Puyo p1)
    {
        this.array = new Puyo[] { p0, p1 };
        this.movePuyoPuyo = new MovePuyoPuyo(this);
        this.rotatePuyoPuyo = new RotatePuyoPuyo(this);
    }
    public Vector2 GetPosition()
    {
        return 0.5f * (this.array[0].position + this.array[1].position);
    }
    public void Update(List<Puyo> list)
    {
        this.disconnect.Update();

        if (this.movePuyoPuyo.Execute(PuyoPuyo.DOWN, list) == Vector2.zero)
        {
            this.disconnect.Start();
        }
        else
        {
            this.disconnect.i = 0;
        }
    }

    public void Drop(List<Puyo> list)
    {
        while (true)
        {
            if (Vector2.zero == this.movePuyoPuyo.Execute(Vector2.down, list))
            {
                this.disconnect.i = this.disconnect.I;
                return;
            }
        }
    }

    public void Sync(int i, int rotate)
    {
        if (rotate == 0)
            this.array[1 - i].position = this.array[i].position + Vector2.right * (1 - 2 * i);
        else if (rotate == 1)
            this.array[1 - i].position = this.array[i].position + Vector2.down * (1 - 2 * i);
        else if (rotate == 2)
            this.array[1 - i].position = this.array[i].position + Vector2.left * (1 - 2 * i);
        else if (rotate == 3)
            this.array[1 - i].position = this.array[i].position + Vector2.up * (1 - 2 * i);
    }
}