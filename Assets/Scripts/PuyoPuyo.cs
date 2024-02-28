using System.Collections.Generic;
using UnityEngine;

public class PuyoPuyo
{
    public static readonly int DISCONNECT = 60;
    private static readonly Vector2 DOWN = Vector2.down * 0.02f;
    public Puyo[] array;
    public Count disconnect = new Count(PuyoPuyo.DISCONNECT);
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
            this.disconnect.Launch();
        }
        else
        {
            this.disconnect = new Count(PuyoPuyo.DISCONNECT);
        }
    }

    public void Drop(List<Puyo> list)
    {
        while (true)
        {
            if (Vector2.zero == this.movePuyoPuyo.Execute(Vector2.down, list))
            {
                this.disconnect.i = this.disconnect.max;
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


public class MovePuyoPuyo
{
    PuyoPuyo puyoPuyo;
    public MovePuyoPuyo(PuyoPuyo puyoPuyo)
    {
        this.puyoPuyo = puyoPuyo;
    }
    public Vector2 Execute(Vector2 v, List<Puyo> list)
    {
        Vector2 position = this.puyoPuyo.GetPosition();
        _execute(v, list);
        Vector2 p = this.puyoPuyo.GetPosition() - position;
        if (v == Vector2.down && p == Vector2.zero) this.puyoPuyo.disconnect.i = this.puyoPuyo.disconnect.max;
        return p;
    }
    private void _execute(Vector2 v, List<Puyo> list)
    {
        Vector2 position = this.puyoPuyo.array[0].position;
        int rotate = this.puyoPuyo.rotatePuyoPuyo.Get();

        int[] a = new int[] { 0, 1 };
        if (this.puyoPuyo.rotatePuyoPuyo.Get() == 0 && v.x > 0) a = new int[] { 1, 0 };
        if (this.puyoPuyo.rotatePuyoPuyo.Get() == 1 && v.y < 0) a = new int[] { 1, 0 };
        if (this.puyoPuyo.rotatePuyoPuyo.Get() == 2 && v.x < 0) a = new int[] { 1, 0 };
        if (this.puyoPuyo.rotatePuyoPuyo.Get() == 3 && v.y > 0) a = new int[] { 1, 0 };

        foreach (int i in a)
        {
            if (this.puyoPuyo.array[i].movePuyo.Execute(v, list) != v)
            {
                this.puyoPuyo.Sync(i, rotate);
                if (Collision.Get(this.puyoPuyo.array[1 - i], list) != null)
                {
                    this.puyoPuyo.array[0].position = position;
                    this.puyoPuyo.Sync(0, rotate);
                }
                else break;
            }
        }
    }
}

public class RotatePuyoPuyo
{
    PuyoPuyo puyoPuyo;
    public RotatePuyoPuyo(PuyoPuyo puyoPuyo)
    {
        this.puyoPuyo = puyoPuyo;
    }
    public int Get()
    {
        if (this.puyoPuyo.array[0].position == this.puyoPuyo.array[1].position + Vector2.left) return 0;
        if (this.puyoPuyo.array[0].position == this.puyoPuyo.array[1].position + Vector2.up) return 1;
        if (this.puyoPuyo.array[0].position == this.puyoPuyo.array[1].position + Vector2.right) return 2;
        if (this.puyoPuyo.array[0].position == this.puyoPuyo.array[1].position + Vector2.down) return 3;
        return -1;
    }
    public void Execute(List<Puyo> list)
    {
        int rotate = Get();
        rotate++;
        if (rotate == 4) rotate = 0;

        Vector2 position = this.puyoPuyo.array[0].position;
        this.puyoPuyo.array[1].position = position;

        if (rotate == 0)
            this.puyoPuyo.array[1].movePuyo.Execute(Vector2.right, list);
        else if (rotate == 1)
            this.puyoPuyo.array[1].movePuyo.Execute(Vector2.down, list);
        else if (rotate == 2)
            this.puyoPuyo.array[1].movePuyo.Execute(Vector2.left, list);
        else if (rotate == 3)
            this.puyoPuyo.array[1].movePuyo.Execute(Vector2.up, list);

        this.puyoPuyo.Sync(1, rotate);

        if (Collision.Get(this.puyoPuyo.array[0], list) != null)
        {
            rotate++;
            if (rotate == 4) rotate = 0;

            this.puyoPuyo.array[1].position = position;
            this.puyoPuyo.Sync(1, rotate);
        }
    }
}