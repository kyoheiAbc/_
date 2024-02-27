using System.Collections.Generic;
using UnityEngine;

public class CustomGameObject
{
    int i;
    public int GetI()
    {
        return this.i;
    }
    public CustomGameObject(int i)
    {
        Main.list.Add(this);
        this.i = i;
    }
    virtual public void Update()
    {
        this.i--;
        if (this.i > 0) return;
        Main.list.Remove(this);
        this.End();
    }
    virtual public void End()
    {
    }
}

public class Utility
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

public class Count
{
    public int i = 0;
    public int I;
    public Count(int i)
    {
        this.I = i;
    }
    public void Update()
    {
        if (0 < this.i && this.i < this.I) this.i++;
    }
    public void Start()
    {
        if (this.i == 0) this.i++;
    }
    public bool Finish()
    {
        return this.i == this.I;
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