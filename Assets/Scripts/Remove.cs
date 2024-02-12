using System.Collections.Generic;
using UnityEngine;

public class Remove
{
    public bool Ready(List<Puyo> l)
    {
        foreach (Puyo p in l)
        {
            if (p.GetPuyoPuyo() != null) continue;
            if (p.GetI() < 10) return false;
            if (p.GetRemove() && p.GetJ() < 10) return false;
        }
        return true;
    }

    public bool Execute(Board board)
    {
        bool b = false;
        for (int y = 1; y < 15; y++)
        {
            for (int x = 1; x < 7; x++)
            {
                if (board.Get(new Vector2(x, y)) == null) continue;
                if (this.Count(board.Get(new Vector2(x, y)), board) >= 4)
                {
                    b = true;
                    bool[,] ba = new bool[16, 8];
                    this.Puyo(board.Get(new Vector2(x, y)), ba, board);
                }
            }
        }
        return b;
    }

    private int Count(Puyo p, Board b)
    {
        bool[,] ba = new bool[16, 8];
        int i = 0;
        Count_(p, ba, ref i, b);
        return i;
    }

    private void Count_(Puyo puyo, bool[,] ba, ref int i, Board b)
    {
        Vector2 p = puyo.GetPosition();
        ba[(int)p.y, (int)p.x] = true;
        i++;
        List<Puyo> rltb = b.GetRlud(p);
        foreach (Puyo l in rltb)
        {
            if (l == null) continue;
            if (l.GetColor() == -1) continue;
            if (l.GetColor() != puyo.GetColor()) continue;

            if (!ba[(int)l.GetPosition().y, (int)l.GetPosition().x])
            {
                Count_(l, ba, ref i, b);
            }
        }
    }

    private void Puyo(Puyo puyo, bool[,] ba, Board b)
    {
        if (puyo == null) return;
        int c = puyo.GetColor();
        if (c == -1) return;

        Vector2 p = puyo.GetPosition();
        if (ba[(int)p.y, (int)p.x] == true) return;
        ba[(int)p.y, (int)p.x] = true;
        puyo.SetRemove(true);

        List<Puyo> list = b.GetRlud(p);
        foreach (Puyo l in list)
        {
            if (l == null) continue;
            if (c == l.GetColor()) Puyo(l, ba, b);
        }
    }
}
