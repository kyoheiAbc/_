using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PuyoPuyo
{
    public Puyo[] array;
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
    public void Update(List<Puyo> list)
    {
        this.disconnect.Update();

        if (Move.PuyoPuyo_(this, Main.PUYO_PUYO_DOWN, list) != Vector2.zero)
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



    public static void Drop(PuyoPuyo puyoPuyo, List<Puyo> list)
    {
        while (true)
        {
            if (Vector2.zero == Move.PuyoPuyo_(puyoPuyo, Vector2.down, list))
            {
                puyoPuyo.disconnect.i = Main.BREAK - 1;
                return;
            }
        }
    }



    public static void Sync(PuyoPuyo puyoPuyo, int i, int rotate)
    {
        if (rotate == 0)
            puyoPuyo.array[1 - i].SetPosition(puyoPuyo.array[i].GetPosition() + Vector2.right * (1 - 2 * i));
        else if (rotate == 1)
            puyoPuyo.array[1 - i].SetPosition(puyoPuyo.array[i].GetPosition() + Vector2.down * (1 - 2 * i));
        else if (rotate == 2)
            puyoPuyo.array[1 - i].SetPosition(puyoPuyo.array[i].GetPosition() + Vector2.left * (1 - 2 * i));
        else if (rotate == 3)
            puyoPuyo.array[1 - i].SetPosition(puyoPuyo.array[i].GetPosition() + Vector2.up * (1 - 2 * i));
    }
}
