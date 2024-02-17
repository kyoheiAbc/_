using System.Collections.Generic;
using UnityEngine;

public class Remove
{
    int i = 0;
    public int GetI() { return this.i; }
    public void SetI(int i) { this.i = i; }

    public bool Ready(List<Puyo> l)
    {
        foreach (Puyo p in l)
        {
            // bug
            if (p.GetPuyoPuyo() != null) continue;
            if (p.GetI() <= Main.FREEZE) return false;
            if (p.GetRemove() && p.GetJ() <= Main.REMOVE) return false;
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
                if (board.Get(new Vector2(x, y)).GetRemove()) continue;

                if (this.Count(board.Get(new Vector2(x, y)), board) >= 4)
                {
                    b = true;
                    this.i++;
                    bool[,] ba = new bool[16, 8];
                    this.Puyo(board.Get(new Vector2(x, y)), ba, board);
                }
            }
        }
        return b;
    }

    private int Count(Puyo p, Board b)
    {
        return Count_(p, new bool[16, 8], 0, b);
    }

    private int Count_(Puyo puyo, bool[,] ba, int i, Board b)
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
