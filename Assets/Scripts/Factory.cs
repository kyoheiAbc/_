using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory
{
    private List<Puyo> list = new List<Puyo>();
    private Color color = new Color();
    private int[] nextColor = new int[4];
    public int[] GetNextColor() { return this.nextColor; }

    public List<Puyo> GetList() { return this.list; }

    public void Reset()
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

        this.color.Reset();
        this.nextColor = new int[] { this.color.Get(), this.color.Get(), this.color.Get(), this.color.Get() };
    }
    public Factory()
    {
        this.Reset();
    }
    private Puyo NewPuyo(int color, Vector2 p)
    {
        this.list.Add(new Puyo(color, p));
        return this.list[this.list.Count - 1];
    }
    public PuyoPuyo NewPuyoPuyo()
    {
        int c0 = this.nextColor[0];
        int c1 = this.nextColor[1];
        this.nextColor[0] = this.nextColor[2];
        this.nextColor[1] = this.nextColor[3];
        this.nextColor[2] = this.color.Get();
        this.nextColor[3] = this.color.Get();

        return new PuyoPuyo(this.NewPuyo(c0, new Vector2(3.5f, 12.5f)), this.NewPuyo(c1, new Vector2(3.5f, 13.5f)));
    }
    public void Sort()
    {
        this.list.Sort((p0, p1) => p0.GetPosition().y.CompareTo(p1.GetPosition().y));
    }
    public void Remove()
    {
        for (int i = this.list.Count - 1; i >= 0; i--)
        {
            if (this.list[i].fire.i >= Main.REMOVE)
            {
                this.list.RemoveAt(i);
            }
        }
    }
}
