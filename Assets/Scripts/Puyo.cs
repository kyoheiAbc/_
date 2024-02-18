using UnityEngine;

public class Puyo
{
    private Vector2 position;
    public Vector2 GetPosition() { return this.position; }
    public void SetPosition(Vector2 p) { this.position = p; }
    private int color;
    public int GetColor() { return this.color; }
    private int i = 0;
    private int j = 0;
    public int GetJ() { return this.j; }
    private bool remove = false;
    public void SetRemove(bool b) { this.remove = b; }
    public bool GetRemove() { return this.remove; }
    public int GetI() { return this.i; }

    public Puyo(int color, Vector2 position)
    {
        this.position = position;
        this.color = color;
    }
    public void Update(Collision c)
    {
        if (this.i < 256) this.i++;
        if (this.j < 256) this.j++;
        if (!this.remove) this.j = 0;

        if (this.Move(Main.PUYO_DOWN, c) != Vector2.zero)
        {
            this.i = 0;
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

