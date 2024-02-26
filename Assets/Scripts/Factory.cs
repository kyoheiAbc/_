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
        this.puyoPuyo = null;

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
    }

    public void Update()
    {
        this.Sort();

        if (this.puyoPuyo == null)
        {
            for (int i = this.list.Count - 1; i >= 0; i--)
            {
                this.list[i].Update(this.list);
                if (this.list[i].fire.Finish())
                {
                    this.list.RemoveAt(i);
                }
            }
            return;
        }

        Puyo[] a = this.puyoPuyo.array;
        float y = this.puyoPuyo.GetPosition().y;
        bool b = false;
        for (int i = this.list.Count - 1; i >= 0; i--)
        {
            if (a[0] == this.list[i]) continue;
            if (a[1] == this.list[i]) continue;

            if (!b && this.list[i].position.y > y)
            {
                b = true;
                this.puyoPuyo.Update(this.list);
                if (this.puyoPuyo.disconnect.Finish())
                {
                    this.puyoPuyo = null;
                    if (a[0].position.y > a[1].position.y)
                    {
                        (a[1], a[0]) = (a[0], a[1]);
                    }
                    a[0].Update(this.list);
                    a[1].Update(this.list);
                }
            }
            this.list[i].Update(this.list);
            if (this.list[i].fire.Finish())
            {
                this.list.RemoveAt(i);
            }
        }
    }

    private Puyo NewPuyo(int color, Vector2 position)
    {
        this.list.Add(new Puyo(color, position));
        return this.list[this.list.Count - 1];
    }
    public void NewPuyoPuyo()
    {
        if (this.puyoPuyo != null) return;
        int[] a = this.nextColor.Get();
        this.puyoPuyo = new PuyoPuyo(this.NewPuyo(a[0], new Vector2(3.5f, 12.5f)), this.NewPuyo(a[1], new Vector2(3.5f, 13.5f)));
    }
    public void Sort()
    {
        this.list.Sort((p0, p1) => p1.position.y.CompareTo(p0.position.y));
    }

    public List<Puyo> NewGarbagePuyo(int i)
    {
        int[] a;
        int _i = 0;
        for (int y = 0; y < (i - 1) / 6 + 1; y++)
        {
            a = Utility.Shuffle(new int[] { 1, 2, 3, 4, 5, 6 });
            for (int x = 0; x < 6; x++)
            {
                _i++;
                Puyo p = new Puyo(10, new Vector2(a[x] + 0.5f, 14.5f - y));
                if (Collision.Get(p, this.list) == null) list.Add(p);
                if (_i == i) break;
            }
        }
        return list;
    }
}