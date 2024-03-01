using UnityEngine;
class Input
{
    private Vector2 position;
    private bool down;
    private bool move;
    public Input()
    {
        this.Start();
    }
    public void Start()
    {
        this.down = false;
        this.move = false;
    }
    public Vector2 Update()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            this.position = UnityEngine.Input.mousePosition;
            this.down = true;
            return Vector2.zero;
        }
        else if (this.down && UnityEngine.Input.GetMouseButton(0))
        {
            Vector2 v = (Vector2)UnityEngine.Input.mousePosition - this.position;
            if (v.x >= Screen.width * Static.THRESHOLD / 100f)
            {
                this.move = true;
                this.position += v;
                return Vector2.right;
            }
            if (v.x <= -Screen.width * Static.THRESHOLD / 100f)
            {
                this.move = true;
                this.position += v;
                return Vector2.left;
            }
            if (v.y >= Screen.width * Static.THRESHOLD / 100f)
            {
                this.move = true;
                this.position += v;
                return Vector2.up;
            }
            if (v.y <= -Screen.width * Static.THRESHOLD / 100f)
            {
                this.move = true;
                this.position += v;
                return Vector2.down;
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
class InputAndroid
{
    private Vector2 position;
    private bool down;
    public InputAndroid()
    {
        this.Start();
    }
    public void Start()
    {
        this.down = false;
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
                if (t.position.x < 0.5f * Screen.width)
                {
                    this.down = true;
                    this.position = t.position;
                }
            }

            if (t.phase == TouchPhase.Ended)
            {
                if (t.position.x < 0.5f * Screen.width) this.down = false;
            }

            if (this.down && t.position.x < 0.5f * Screen.width)
            {
                Vector2 v = (Vector2)t.position - this.position;
                if (v.x >= Screen.width * Static.THRESHOLD / 100f)
                {
                    this.position += v;
                    return Vector2.right;
                }
                if (v.x <= -Screen.width * Static.THRESHOLD / 100f)
                {
                    this.position += v;
                    return Vector2.left;
                }
                if (v.y >= Screen.width * Static.THRESHOLD / 100f)
                {
                    this.position += v;
                    return Vector2.up;
                }
                if (v.y <= -Screen.width * Static.THRESHOLD / 100f)
                {
                    this.position += v;
                    return Vector2.down;
                }
            }
        }
        return Vector2.zero;
    }
}