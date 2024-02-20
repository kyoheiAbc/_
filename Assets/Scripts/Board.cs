using System.Collections.Generic;
using UnityEngine;
public class Board
{
    private Puyo[,] array = new Puyo[16, 8];
    public Board(List<Puyo> l)
    {
        foreach (Puyo p in l)
        {
            if (p.position.y == 0.5f)
            {
                this.Set(p);
                continue;
            }
            // if (p.freeze.i <= Main.FREEZE) continue;
            this.Set(p);
        }
    }
    public void Set(Puyo p)
    {
        this.array[(int)p.position.y, (int)p.position.x] = p;
    }
    public Puyo Get(Vector2 p)
    {
        return this.array[(int)p.y, (int)p.x];
    }
    public List<Puyo> GetRlud(Vector2 p)
    {
        return new List<Puyo>{
            this.Get(p + Vector2.right),
            this.Get(p + Vector2.left),
            this.Get(p + Vector2.up),
            this.Get(p + Vector2.down)
        };
    }
}