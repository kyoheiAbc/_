using UnityEngine;
public class Field
{
    private Puyo[,] array = new Puyo[14, 8];
    public void SetPuyo(Puyo puyo)
    {
        Vector2 p = puyo.GetPosition();
        this.array[(int)p.y, (int)p.x] = puyo;
    }
    public Puyo GetPuyo(Vector2 position)
    {
        if ((int)position.y >= this.array.GetLength(0)) return null;
        return this.array[(int)position.y, (int)position.x];
    }
}
