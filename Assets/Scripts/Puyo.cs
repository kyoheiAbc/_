using System;
using System.Collections.Generic;
using UnityEngine;


public class Puyo
{
    private Vector2 position;
    public Vector2 GetPosition() { return this.position; }
    public void SetPosition(Vector2 p) { this.position = p; }

    private int color;
    public int GetColor() { return this.color; }
    private int i = 0;
    public int GetI() { return this.i; }
    private bool freeze;
    private PuyoPuyo puyoPuyo;
    public PuyoPuyo GetPuyoPuyo() { return this.puyoPuyo; }
    public void SetPuyoPuyo(PuyoPuyo puyoPuyo) { this.puyoPuyo = puyoPuyo; }


    public Puyo(int color, Vector2 position, bool freeze)
    {
        this.position = position;
        this.color = color;
        this.freeze = freeze;
    }

    public void Update(Collision c)
    {
        if (this.puyoPuyo != null)
        {
            return;
        }

        if (this.i < 256) this.i++;

        if (this.Move(0.01f * Vector2.down, c) != Vector2.zero)
        {
            this.i = 0;
        }

        Puyo p = c.Get(this.position + Vector2.down);
        if (p != null && p.GetPuyoPuyo() != null)
        {
            this.i = 0;
        }
    }

    public Vector2 Move(Vector2 v, Collision c)
    {
        if (this.freeze) return Vector2.zero;

        Vector2 _position = this.position;
        this.position += v;

        List<Puyo> list = c.Get(this);

        if (this.puyoPuyo != null)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (this.puyoPuyo == list[i].GetPuyoPuyo())
                    list.RemoveAt(i);
            }
        }

        if (list.Count == 0) return this.position - _position;

        if (list.Count == 1)
        {
            if (v.y != 0)
            {
                this.position.y = list[0].GetPosition().y - Mathf.Sign(v.y);
                return this.position - _position;
            }

            if (v.x != 0)
            {
                float y = list[0].GetPosition().y;
                if (Mathf.Abs(this.position.y - y) > 0.5f)
                {
                    this.position.y = y + Mathf.Sign(this.position.y - y);

                    if (!c.IfCollision(this)) return this.position - _position;
                }
            }
        }

        this.position = _position;
        return Vector2.zero;
    }




}

