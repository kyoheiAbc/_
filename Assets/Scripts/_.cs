using System.Collections.Generic;
using UnityEngine;
public class Static
{
    public static int THRESHOLD = 3;
    public static int COLOR = 4;
    public static int FIRE = 4;
    public static int DOWN = 3;
    public static int COMBO = 90;
    public static int BOT_HEALTH = 64;
    public static int BOT_ATTACK = 100;
    public static int BOT_SPEED = 3 * 60;
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
    virtual public void Clear()
    {
        Main.list.Remove(this);
    }
}
public class Count
{
    public int i;
    public readonly int maximum;
    public Count(int i)
    {
        this.i = 0;
        this.maximum = i;
    }
    public void Update()
    {
        if (0 < this.i && this.i < this.maximum) this.i++;
    }
    public void Launch()
    {
        if (this.i == 0) this.i++;
    }
    public bool Finish()
    {
        return this.i == this.maximum;
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