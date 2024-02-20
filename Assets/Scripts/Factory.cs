using System.Collections.Generic;
using UnityEngine;
public class Factory
{
    public PuyoPuyo puyoPuyo = null;
    public List<Puyo> list = new List<Puyo>();
    public NextColor nextColor = new NextColor();
    public Factory()
    {
        this.Start();
    }
    public void Start()
    {
        this.list.Clear();
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

        this.nextColor = new NextColor();

        NewPuyoPuyo();
    }

    private Puyo NewPuyo(int color, Vector2 position)
    {
        this.list.Add(new Puyo(color, position));
        return this.list[this.list.Count - 1];
    }
    public PuyoPuyo NewPuyoPuyo()
    {
        int[] a = this.nextColor.Get();
        this.puyoPuyo = new PuyoPuyo(this.NewPuyo(a[0], new Vector2(3.5f, 12.5f)), this.NewPuyo(a[1], new Vector2(3.5f, 13.5f)));
        return this.puyoPuyo;
    }
    public void Sort()
    {
        this.list.Sort((p0, p1) => p0.position.y.CompareTo(p1.position.y));
    }
    public void Remove()
    {
        for (int i = this.list.Count - 1; i >= 0; i--)
        {
            if (this.list[i].fire.Finish())
            {
                this.list.RemoveAt(i);
            }
        }
    }
}