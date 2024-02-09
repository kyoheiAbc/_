using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory
{
    private List<Puyo> list = new List<Puyo>();
    public List<Puyo> GetList() { return this.list; }

    public Factory()
    {
        for (int y = 0; y < 16; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (x == 0 || x == 7 || y == 0 || y == 15)
                {
                    this.NewPuyo(-1, new Vector2(x + 0.5f, y + 0.5f), true);
                }
            }
        }
    }

    public Puyo NewPuyo(int color, Vector2 p, bool freeze)
    {
        this.list.Add(new Puyo(color, p, freeze));
        return this.list[this.list.Count - 1];
    }
    public void ListSort()
    {
        this.list.Sort((p0, p1) => p0.GetPosition().y.CompareTo(p1.GetPosition().y));
    }
}
