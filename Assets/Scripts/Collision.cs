using UnityEngine;
using System.Collections.Generic;
public class Collision
{
    private List<Puyo> list;
    public Collision(List<Puyo> l)
    {
        this.list = l;
    }
    public List<Puyo> Get(Puyo puyo)
    {
        List<Puyo> returnList = new List<Puyo>();
        Vector2 position = puyo.GetPosition();
        foreach (Puyo l in this.list)
        {
            if (puyo == l) continue;
            Vector2 p = l.GetPosition();
            if (position.x != p.x) continue;
            if (Mathf.Abs(position.y - p.y) >= 1) continue;
            returnList.Add(l);
        }
        return returnList;
    }
    public bool IfCollision(Puyo p)
    {
        foreach (Puyo l in this.list)
        {
            if (p == l) continue;
            if (Vector2.SqrMagnitude(p.GetPosition() - l.GetPosition()) < 1) return true;
        }
        return false;
    }
}