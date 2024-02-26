using UnityEngine;
class Input
{
    private Camera camera;
    private Vector2 position;
    private bool down;
    private bool move;
    public Input(Camera camera)
    {
        this.camera = camera;
        this.Start();
    }
    public void Start()
    {
        this.down = false;
        this.move = false;
    }
    public Vector2 Update()
    {
        for (int i = 0; i < UnityEngine.Input.touchCount; i++)
        {
            Touch t = UnityEngine.Input.GetTouch(i);
            if (t.phase == TouchPhase.Began)
            {
                if (t.position.x > 0.5f * Screen.width)
                {
                    return Vector2.right + Vector2.down;
                }
            }
        }

        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            this.position = this.camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            this.down = true;
            return Vector2.zero;
        }
        else if (this.down && UnityEngine.Input.GetMouseButton(0))
        {
            Vector2 v = (Vector2)this.camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition) - this.position;
            v = Mathf.Abs(v.x) >= Mathf.Abs(v.y) ? new Vector2(v.x, 0) : new Vector2(0, v.y);
            if (v.y > 0)
            {
                if (v.y < 2) return Vector2.zero;
                this.move = true;
                this.position += v;
                return Vector2.up;
            }
            else if (v.magnitude >= 1)
            {
                this.move = true;
                v = v.normalized;
                this.position += v;
                return v;
            }
            return Vector2.zero;
        }
        else if (this.down && !this.move && UnityEngine.Input.GetMouseButtonUp(0))
        {
            return Vector2.right + Vector2.down;
        }
        else
        {
            this.Start();
            return Vector2.zero;
        }
    }
}

public class Input_
{

    private Vector2 position;


    public Vector2 Update()
    {
        for (int i = 0; i < UnityEngine.Input.touchCount; i++)
        {
            Touch t = UnityEngine.Input.GetTouch(i);

            if (t.phase == TouchPhase.Began)
            {
                if (t.position.x > 0.5f * Screen.width)
                {
                    return Vector2.right + Vector2.down;
                }
                if (t.position.x < 0.5f * Screen.width)
                {
                    this.position = t.position;
                }
            }


            if (t.position.x < 0.5f * Screen.width)
            {
                Vector2 v = t.position - this.position;
                v = Mathf.Abs(v.x) >= Mathf.Abs(v.y) ? new Vector2(v.x, 0) : new Vector2(0, v.y);
                if (v.magnitude >= 1)
                {
                    v = v.normalized;
                    this.position += v;
                    return v;
                }
            }
        }
        return Vector2.zero;
    }
}