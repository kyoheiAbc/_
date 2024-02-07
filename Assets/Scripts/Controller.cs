using UnityEngine;
class Controller
{
    private Camera camera;
    private Vector2 position;
    public Controller(Camera camera)
    {
        this.camera = camera;
    }
    public Vector2 Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.position = this.camera.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
        {
            Vector2 v = (Vector2)this.camera.ScreenToWorldPoint(Input.mousePosition) - this.position;
            v = Mathf.Abs(v.x) >= Mathf.Abs(v.y) ? new Vector2(v.x, 0) : new Vector2(0, v.y);
            if (v.magnitude > 1)
            {
                v = v.normalized;
                this.position += v;
                return v;
            }
        }
        return Vector2.zero;
    }
}
