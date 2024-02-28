using System.Collections.Generic;
using UnityEngine;
public class Board
{
    private Puyo[,] array = new Puyo[16, 8];
    public Board(List<Puyo> list)
    {
        foreach (Puyo l in list)
        {
            if (l.position.y == 0.5f)
            {
                this.Set(l);
                continue;
            }
            if (this.Get(l.position + Vector2.down) == null) continue;
            if (l.freeze.Finish()) this.Set(l);
        }
    }
    public void Set(Puyo puyo)
    {
        this.array[(int)puyo.position.y, (int)puyo.position.x] = puyo;
    }
    public Puyo Get(Vector2 position)
    {
        return this.array[(int)position.y, (int)position.x];
    }
    public List<Puyo> GetRlud(Vector2 position)
    {
        return new List<Puyo>{
            this.Get(position + Vector2.right),
            this.Get(position + Vector2.left),
            this.Get(position + Vector2.up),
            this.Get(position + Vector2.down)
        };
    }
}