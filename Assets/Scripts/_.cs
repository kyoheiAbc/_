using System.Collections.Generic;
using UnityEngine;
public class Static
{
    public static int[] Shuffle(int[] array)
    {
        int[] a = (int[])array.Clone();
        for (int i = a.Length - 1; i > 0; i--)
        {
            int r = UnityEngine.Random.Range(0, i + 1);
            (a[r], a[i]) = (a[i], a[r]);
        }
        return a;
    }
}
public class CustomGameObject
{
    protected int i;
    public CustomGameObject(int i)
    {
        Main.list.Add(this);
        this.i = i;
    }
    virtual public void Update()
    {
        this.i--;
        if (this.i == 0)
        {
            this.End();
            Main.list.Remove(this);
        }
    }
    virtual public void End() { }
}
public class Count
{
    public int i;
    public readonly int max;
    public Count(int i)
    {
        this.i = 0;
        this.max = i;
    }
    public void Update()
    {
        if (0 < this.i && this.i < this.max) this.i++;
    }
    public void Start()
    {
        if (this.i == 0) this.i++;
    }
    public bool Finish()
    {
        return this.i == this.max;
    }
}
public class Collision
{
    static public Puyo Get(Puyo puyo, List<Puyo> list)
    {
        foreach (Puyo l in list)
        {
            if (puyo == l) continue;
            if (Vector2.SqrMagnitude(puyo.position - l.position) < 1) return l;
        }
        return null;
    }
}