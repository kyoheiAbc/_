using UnityEngine;
class Input
{
    private Camera camera;
    private Vector2 position;
    public Input(Camera c)
    {
        this.camera = c;
    }
    public Vector2 Update()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            this.position = this.camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
        }
        else if (UnityEngine.Input.GetMouseButton(0) || UnityEngine.Input.GetMouseButtonUp(0))
        {
            Vector2 v = (Vector2)this.camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition) - this.position;
            v = Mathf.Abs(v.x) >= Mathf.Abs(v.y) ? new Vector2(v.x, 0) : new Vector2(0, v.y);
            if (v.magnitude >= 1)
            {
                v = v.normalized;
                this.position += v;
                return v;
            }
        }
        return Vector2.zero;
    }
}
