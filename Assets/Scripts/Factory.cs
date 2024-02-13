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
                    this.NewPuyo(-1, new Vector2(x + 0.5f, y + 0.5f));
                }
            }
        }
    }
    private Puyo NewPuyo(int color, Vector2 p)
    {
        this.list.Add(new Puyo(color, p));
        return this.list[this.list.Count - 1];
    }
    public PuyoPuyo NewPuyoPuyo(Color c)
    {
        return new PuyoPuyo(this.NewPuyo(c.Get(), new Vector2(3.5f, 12.5f)), this.NewPuyo(c.Get(), new Vector2(4.5f, 12.5f)));
    }
    public void Sort()
    {
        this.list.Sort((p0, p1) => p0.GetPosition().y.CompareTo(p1.GetPosition().y));
    }
    public void Remove()
    {
        for (int i = this.list.Count - 1; i >= 0; i--)
        {
            if (this.list[i].GetRemove() && this.list[i].GetJ() >= Main.REMOVE)
            {
                Main.Destroy(this.list[i].GetTransform().gameObject);
                this.list.RemoveAt(i);
            }
        }
    }
}
