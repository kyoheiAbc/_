using System;
using System.Collections.Generic;
using UnityEngine;


public class Puyo
{
    private Vector2 position;
    public Vector2 GetPosition() { return this.position; }
    private int color;
    public int GetColor() { return this.color; }
    private bool freeze;


    public Puyo(int color, Vector2 position, bool freeze)
    {
        this.position = position;
        this.color = color;
        this.freeze = freeze;
    }

    public void Update(Collision c)
    {
        this.Move(0.01f * Vector2.down, c);
    }

    public void Move(Vector2 v, Collision c)
    {
        if (this.freeze) return;

        Vector2 _position = this.position;
        this.position += v;

        List<Puyo> list = c.Get(this);

        if (list.Count == 0) return;

        if (list.Count == 1)
        {
            if (v.y != 0)
            {
                this.position.y = list[0].GetPosition().y - Mathf.Sign(v.y);
                return;
            }

            if (v.x != 0)
            {
                float y = list[0].GetPosition().y;
                if (Mathf.Abs(this.position.y - y) > 0.5f)
                {
                    this.position.y = y + Mathf.Sign(this.position.y - y);

                    if (!c.IfCollision(this)) return;
                }
            }
        }

        this.position = _position;
    }




}

