using System.Collections.Generic;
using UnityEngine;

public class PuyoManager
{
    private PuyoPuyo puyoPuyo = null;
    private List<Puyo> list = new List<Puyo>();
    private Colors colors = new Colors();
    private Field field = new Field();
    public PuyoManager()
    {
        for (int y = 0; y < 14; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (y == 0 || x == 0 || x == 7)
                {
                    this.field.SetPuyo(this.NewPuyo(new Vector2(x + 0.5f, y + 0.5f), this.colors.GetColor()));
                }
            }
        }

        this.field.SetPuyo(this.NewPuyo(new Vector2(3 + 0.5f, 6 + 0.5f), this.colors.GetColor()));
        this.field.SetPuyo(this.NewPuyo(new Vector2(5 + 0.5f, 6 + 0.5f), this.colors.GetColor()));
        this.field.SetPuyo(this.NewPuyo(new Vector2(5 + 0.5f, 8 + 0.5f), this.colors.GetColor()));

        this.NewPuyo(new Vector2(1 + 0.5f, 16.1f + 0.5f), this.colors.GetColor());
        this.NewPuyo(new Vector2(5 + 0.5f, 16.1f + 0.5f), this.colors.GetColor());
        this.NewPuyo(new Vector2(4 + 0.5f, 13.2f + 0.5f), this.colors.GetColor());
        this.NewPuyo(new Vector2(6 + 0.5f, 12.3f + 0.5f), this.colors.GetColor());
    }

    public Puyo NewPuyo(Vector2 position, int color)
    {
        list.Add(new Puyo(color, position, this.list));
        return this.list[this.list.Count - 1];
    }
    public void Update()
    {
        for (int i = 0; i < this.list.Count; i++)
        {
            if (this.list[i] == this.field.GetPuyo(list[i].GetPosition())) continue;
            if (this.list[i].GetPuyoPuyo() != null) continue;
            this.list[i].Move(new Vector2(0, -0.01f));
        }
        if (this.puyoPuyo == null)
        {
            this.puyoPuyo = new PuyoPuyo(
                this.NewPuyo(new Vector2(3 + 0.5f, 12 + 0.5f), this.colors.GetColor()),
                this.NewPuyo(new Vector2(4 + 0.5f, 12 + 0.5f), this.colors.GetColor())
            );
        }
        this.puyoPuyo.Move(new Vector2(0, -0.01f));
    }
    public Vector2 MovePuyoPuyo(Vector2 v)
    {
        return this.puyoPuyo.Move(v);
    }

}


public class Colors
{
    private int[] array = new int[]{
        0,1,2,3,
        0,1,2,3,
        0,1,2,3,
        0,1,2,3
    };
    private int i = 16;

    private void Reset()
    {
        this.i = 0;

        for (int i_ = this.array.Length - 1; i_ > 0; i_--)
        {
            int r = UnityEngine.Random.Range(0, i_ + 1);
            int temp = this.array[r];
            this.array[r] = this.array[i_];
            this.array[i_] = temp;
        }
    }
    public int GetColor()
    {
        if (this.i > this.array.Length - 1) this.Reset();
        this.i++;
        return this.array[i - 1];
    }
}
