using System.Collections.Generic;
using UnityEngine;

public class Puyo
{
    private GameObject gameObject;
    private Vector2 position;
    private int color;
    private PuyoPuyo puyoPuyo;
    readonly private List<Puyo> list;

    public Puyo(int color, Vector2 position, List<Puyo> list)
    {
        this.position = position;
        this.puyoPuyo = null;
        this.gameObject = Main.Instantiate(this.position);
        this.color = color;
        this.gameObject.transform.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(this.color / 5f, 0.5f, 1.0f);
        this.list = list;
    }

    public void SetPuyoPuyo(PuyoPuyo puyoPuyo)
    {
        this.puyoPuyo = puyoPuyo;
    }
    public PuyoPuyo GetPuyoPuyo()
    {
        return this.puyoPuyo;
    }

    public Vector2 Move(Vector2 v)
    {
        Vector2 p = this.position;
        this.position += v;

        for (int i = 0; i < this.list.Count; i++)
        {
            if (this.list[i] == this) continue;

            if (this.puyoPuyo != null && this.puyoPuyo == this.list[i].GetPuyoPuyo()) continue;

            Vector2 l = this.list[i].GetPosition();
            if (Vector2.SqrMagnitude(this.position - l) >= 1) continue;


            if (v.x != 0)
            {
                if (Mathf.Abs(this.position.y - l.y) < 0.25)
                {
                    this.position = p;
                    break;
                }
                this.position.y = l.y + Mathf.Sign(this.position.y - l.y);
                if (this.isColliding())
                {
                    this.position = p;
                    break;
                }
            }
            else
            {
                this.position.y = l.y - Mathf.Sign(v.y);
            }
        }

        this.gameObject.transform.position = this.position;
        return this.position - p;

    }
    public Vector2 GetPosition()
    {
        return this.position;
    }
    public void SetPosition(Vector2 p)
    {
        this.position = p;
        this.gameObject.transform.position = this.position;
    }
    public bool isColliding()
    {
        for (int i = 0; i < this.list.Count; i++)
        {
            if (this.list[i] == this) continue;
            if (Vector2.SqrMagnitude(this.position - this.list[i].GetPosition()) < 1) return true;
        }
        return false;
    }
}

