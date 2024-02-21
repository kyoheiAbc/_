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

        float y = this.puyoPuyo.GetPosition().y;
        bool b = false;
        for (int i = this.list.Count - 1; i >= 0; i--)
        {
            if (this.puyoPuyo != null && this.puyoPuyo.array[0] == this.list[i]) continue;
            if (this.puyoPuyo != null && this.puyoPuyo.array[1] == this.list[i]) continue;

            if (!b && this.list[i].position.y > y)
            {
                b = true;
                this.puyoPuyo.Update(this.list);
                if (this.puyoPuyo.disconnect.Finish())
                {
                    this.puyoPuyo = null;
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
}