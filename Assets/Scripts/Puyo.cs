using UnityEngine;

public class Puyo
{
    private Vector2 position;
    public Vector2 GetPosition() { return this.position; }
    public void SetPosition(Vector2 p) { this.position = p; }
    private int color;
    public int GetColor() { return this.color; }
    public I freeze = new I();
    public I fire = new I();

    public Puyo(int color, Vector2 position)
    {
        this.position = position;
        this.color = color;
    }
    public void Update(Collision c)
    {
        freeze.Update();
        fire.Update();

        if (this.Move(Main.PUYO_DOWN, c) != Vector2.zero)
        {
            this.freeze.i = 0;
        }
        else
        {
            this.freeze.Start();
        }
    }

    public Vector2 Move(Vector2 v, Collision c)
    {
        if (this.position.x == 0.5f) return Vector2.zero;
        if (this.position.x == 7.5f) return Vector2.zero;
        if (this.position.y == 0.5f) return Vector2.zero;
        if (this.position.y == 15.5f) return Vector2.zero;

        Vector2 _position = this.position;
        this.position += v;

        Puyo p = c.Get(this);

        if (p == null) return this.position - _position;



        if (v.y != 0)
        {
            this.position.y = p.GetPosition().y - Mathf.Sign(v.y);
            return this.position - _position;
        }

        if (v.x != 0)
        {
            float y = p.GetPosition().y;
            if (Mathf.Abs(this.position.y - y) > 0.5f)
            {
                this.position.y = y + Mathf.Sign(this.position.y - y);
                if (c.Get(this) == null) return this.position - _position;
            }
        }

        this.position = _position;
        return Vector2.zero;
    }


}

