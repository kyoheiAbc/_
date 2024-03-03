using System.Collections.Generic;
using UnityEngine;
public class Board
{
    private Puyo[,] array = new Puyo[16, 8];
    public Board(List<Puyo> list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].position.y == 0.5f)
            {
                this.Set(list[i]);
                continue;
            }
            if (list[i].freeze.GetProgress() == 1)
            {
                if (this.Get(list[i].position + Vector2.down) != null) this.Set(list[i]);
            }
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