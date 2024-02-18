using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PuyoPuyo
{
    private Puyo[] array;
    public Puyo[] GetArray() { return this.array; }
    private I disconnect = new I();

    public PuyoPuyo(Puyo p0, Puyo p1)
    {
        this.array = new Puyo[] { p0, p1 };
    }
    public Vector2 GetPosition()
    {
        return 0.5f * (this.array[0].GetPosition() + this.array[1].GetPosition());
    }
    public void Update(Collision c)
    {
        this.disconnect.Update();

        if (this.Move(Main.DOWN, c) != Vector2.zero)
        {
            this.disconnect.i = 0;
        }
        else
        {
            this.disconnect.Start();
        }

        if (this.disconnect.i == Main.BREAK)
        {
            this.array = new Puyo[] { null, null };
        }

    }

    public Vector2 Move(Vector2 v, Collision c)
    {
        Vector2 p = this.array[0].GetPosition();
        int rotate = GetRotate();

        for (int i = 0; i < 2; i++)
        {
            if (this.array[i].Move(v, c) != v)
            {
                this.Sync(i, rotate);
                if (c.Get(this.array[1 - i]) != null)
                {

                    this.array[0].SetPosition(p);
                    this.Sync(0, rotate);
                }
                else
                {
                    break;
                }
            }
        }
        return this.array[0].GetPosition() - p;
    }

    public void Drop(Collision c)
    {
        while (true)
        {
            if (Vector2.zero == this.Move(Vector2.down, c))
            {
                this.disconnect.i = Main.BREAK - 1;
                return;
            }
        }
    }

    private int GetRotate()
    {
        if (this.array[0].GetPosition() == this.array[1].GetPosition() + Vector2.left) return 0;
        if (this.array[0].GetPosition() == this.array[1].GetPosition() + Vector2.up) return 1;
        if (this.array[0].GetPosition() == this.array[1].GetPosition() + Vector2.right) return 2;
        if (this.array[0].GetPosition() == this.array[1].GetPosition() + Vector2.down) return 3;
        return -1;
    }

    public void Rotate(Collision c)
    {
        int rotate = GetRotate();
        rotate++;
        if (rotate == 4) rotate = 0;

        Vector2 p = this.array[0].GetPosition();
        this.array[1].SetPosition(p);

        if (rotate == 0)
            this.array[1].Move(Vector2.right, c);
        else if (rotate == 1)
            this.array[1].Move(Vector2.down, c);
        else if (rotate == 2)
            this.array[1].Move(Vector2.left, c);
        else if (rotate == 3)
            this.array[1].Move(Vector2.up, c);

        this.Sync(1, rotate);

        if (c.Get(this.array[0]) != null)
        {
            rotate++;
            if (rotate == 4) rotate = 0;

            this.array[1].SetPosition(p);
            this.Sync(1, rotate);
        }
    }

    private void Sync(int i, int rotate)
    {
        if (rotate == 0)
            this.array[1 - i].SetPosition(this.array[i].GetPosition() + Vector2.right * (1 - 2 * i));
        else if (rotate == 1)
            this.array[1 - i].SetPosition(this.array[i].GetPosition() + Vector2.down * (1 - 2 * i));
        else if (rotate == 2)
            this.array[1 - i].SetPosition(this.array[i].GetPosition() + Vector2.left * (1 - 2 * i));
        else if (rotate == 3)
            this.array[1 - i].SetPosition(this.array[i].GetPosition() + Vector2.up * (1 - 2 * i));
    }
}
