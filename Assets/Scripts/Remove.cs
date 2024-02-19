using System.Collections.Generic;
using UnityEngine;

public class Remove
{
    static public bool Ready(List<Puyo> l)
    {
        foreach (Puyo p in l)
        {
            // bug
            if (p.freeze.i <= Main.FREEZE) return false;
            if (0 < p.fire.i && p.fire.i <= Main.REMOVE) return false;
        }
        return true;
    }

    public static int Execute(Board board)
    {
        int i = 0; for (int y = 1; y < 15; y++)
        {
            for (int x = 1; x < 7; x++)
            {
                if (board.Get(new Vector2(x, y)) == null) continue;
                if (board.Get(new Vector2(x, y)).fire.i != 0) continue;

                if (Count(board.Get(new Vector2(x, y)), board) >= 4)
                {
                    i++;
                    bool[,] ba = new bool[16, 8];
                    Puyo(board.Get(new Vector2(x, y)), ba, board);
                }
            }
        }
        return i;
    }

    static private int Count(Puyo p, Board b)
    {
        return Count_(p, new bool[16, 8], 0, b);
    }

    static private int Count_(Puyo puyo, bool[,] ba, int i, Board b)
    {
        int returnI = i;
        Vector2 p = puyo.GetPosition();
        ba[(int)p.y, (int)p.x] = true;
        returnI++;
        List<Puyo> rltb = b.GetRlud(p);
        foreach (Puyo l in rltb)
        {
            if (l == null) continue;
            if (l.GetColor() == -1) continue;
            if (l.GetColor() != puyo.GetColor()) continue;

            if (!ba[(int)l.GetPosition().y, (int)l.GetPosition().x])
            {
                returnI = Count_(l, ba, returnI, b);
            }
        }
        return returnI;
    }

    static private void Puyo(Puyo puyo, bool[,] ba, Board b)
    {
        if (puyo == null) return;
        int c = puyo.GetColor();
        if (c == -1) return;

        Vector2 p = puyo.GetPosition();
        if (ba[(int)p.y, (int)p.x] == true) return;
        ba[(int)p.y, (int)p.x] = true;
        puyo.fire.Start();

        List<Puyo> list = b.GetRlud(p);
        foreach (Puyo l in list)
        {
            if (l == null) continue;
            if (c == l.GetColor()) Puyo(l, ba, b);
        }
    }
}
