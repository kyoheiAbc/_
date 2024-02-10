using UnityEngine;
using System.Collections.Generic;
public class Collision
{
    private List<Puyo> list;
    public Collision(List<Puyo> l)
    {
        this.list = l;
    }
    public Puyo Get(Puyo p)
    {
        foreach (Puyo l in this.list)
        {
            if (p == l) continue;
            if (Vector2.SqrMagnitude(p.GetPosition() - l.GetPosition()) < 1) return l;
        }
        return null;
    }
}