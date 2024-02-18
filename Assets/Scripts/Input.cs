using UnityEngine;
class Input
{
    private Vector2 position;
    private bool down;
    public void SetDown(bool b) { this.down = b; }
    private bool move;

    public Input()
    {
        this.Reset();
    }

    public void Reset()
    {
        this.down = false;
        this.move = false;
    }
    public Vector2 Update(Camera camera)
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            this.position = camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            this.down = true;
            this.move = false;
        }
        if (!this.down) return Vector2.zero;

        if (UnityEngine.Input.GetMouseButton(0))
        {
            Vector2 v = (Vector2)camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition) - this.position;
            v = Mathf.Abs(v.x) >= Mathf.Abs(v.y) ? new Vector2(v.x, 0) : new Vector2(0, v.y);
            if (v.magnitude >= 1)
            {
                this.move = true;
                v = v.normalized;
                this.position += v;
                return v;
            }
        }
        else if (!this.move && UnityEngine.Input.GetMouseButtonUp(0))
        {
            return Vector2.right + Vector2.up;
        }
        return Vector2.zero;
    }
}
