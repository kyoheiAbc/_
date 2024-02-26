using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fire
{
    Board board;

    public Fire(Board b)
    {
        this.board = b;
    }
    static public bool Ready(PuyoPuyo puyoPuyo, List<Puyo> list)
    {
        foreach (Puyo l in list)
        {
            if (puyoPuyo != null && puyoPuyo.array[0] == l) continue;
            if (puyoPuyo != null && puyoPuyo.array[1] == l) continue;

            if (!l.freeze.Finish()) return false;
            if (0 < l.fire.i && !l.fire.Finish()) return false;
        }
        return true;
    }

    public int Execute()
    {
        int i = 0;
        for (int y = 1; y < 15; y++)
        {
            for (int x = 1; x < 7; x++)
            {
                if (this.board.Get(new Vector2(x, y)) == null) continue;
                if (this.board.Get(new Vector2(x, y)).fire.i == 1) continue;

                if (this.Count(this.board.Get(new Vector2(x, y))) >= 4)
                {
                    i++;
                    bool[,] a = new bool[16, 8];
                    this.Puyo(this.board.Get(new Vector2(x, y)), a);
                }
            }
        }
        return i;
    }

    private int Count(Puyo p)
    {
        return this._Count(p, new bool[16, 8], 0);
    }

    private int _Count(Puyo puyo, bool[,] a, int i)
    {
        int I = i;
        Vector2 p = puyo.position;
        a[(int)p.y, (int)p.x] = true;
        I++;
        List<Puyo> rltb = this.board.GetRlud(p);
        foreach (Puyo l in rltb)
        {
            if (l == null) continue;
            if (l.color == -1 || l.color == 10) continue;
            if (l.color != puyo.color) continue;

            if (!a[(int)l.position.y, (int)l.position.x])
            {
                I = this._Count(l, a, I);
            }
        }
        return I;
    }

    private void Puyo(Puyo puyo, bool[,] a)
    {
        if (puyo == null) return;
        int c = puyo.color;
        if (c == -1 || c == 10) return;

        Vector2 p = puyo.position;
        if (a[(int)p.y, (int)p.x] == true) return;
        a[(int)p.y, (int)p.x] = true;
        puyo.fire.Start();

        List<Puyo> list = this.board.GetRlud(p);
        foreach (Puyo l in list)
        {
            if (l == null) continue;
            if (l.color == 10) l.fire.Start();
            if (c == l.color) this.Puyo(l, a);
        }
    }
}
