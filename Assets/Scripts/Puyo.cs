using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Puyo
{
    private Vector2 position;

    public Vector2 Position() => this.position;
    public Puyo(Vector2 position)
    {
        this.position = position;
    }

    public void Move(Vector2 movement, List<Puyo> list)
    {
        Vector2 newPosition = position + movement;
        if (!IsCollision(newPosition, list))
            position = newPosition;
    }

    private bool IsCollision(Vector2 newPosition, List<Puyo> list)
    {
        foreach (Puyo p in list)
        {
            if (p != this && Vector2.SqrMagnitude(p.Position() - newPosition) < 1)
                return true;
        }
        return false;
    }
}

