using UnityEngine;
using System.Collections.Generic;
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
    static public Puyo Get(Vector2 position, List<Puyo> list)
    {
        foreach (Puyo l in list)
        {
            if (Vector2.SqrMagnitude(position - l.position) < 1) return l;
        }
        return null;
    }
}