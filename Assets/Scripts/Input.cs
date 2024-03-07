using UnityEngine;
class Input
{
    private Vector2 anchor;
    public Vector2 Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.RightArrow)) return Vector2.right;
        if (UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow)) return Vector2.left;
        if (UnityEngine.Input.GetKeyDown(KeyCode.UpArrow)) return Vector2.up;
        if (UnityEngine.Input.GetKeyDown(KeyCode.DownArrow)) return Vector2.down;
        if (UnityEngine.Input.GetKeyDown(KeyCode.Return)) return Vector2.right + Vector2.down;

        for (int i = 0; i < UnityEngine.Input.touchCount; i++)
        {
            Touch t = UnityEngine.Input.GetTouch(i);
            if (t.position.x <= 0.5f * Screen.width) continue;
            if (t.phase != TouchPhase.Began) continue;
            return Vector2.right + Vector2.down;
        }

        for (int i = 0; i < UnityEngine.Input.touchCount; i++)
        {
            Touch t = UnityEngine.Input.GetTouch(i);
            if (t.position.x > 0.5f * Screen.width) continue;
            if (t.phase == TouchPhase.Began) this.anchor = t.position;

            Vector2 delta = t.position - this.anchor;
            this.anchor += delta;
            if (delta.x > 100) return Vector2.right;
            if (delta.x < -100) return Vector2.left;
            if (delta.y > 100) return Vector2.up;
            if (delta.y < -100) return Vector2.down;
            this.anchor -= delta;
        }
        return Vector2.zero;
    }
    static public Vector2 Position()
    {
        if (UnityEngine.Input.touchCount > 0) return UnityEngine.Input.GetTouch(0).position;
        return UnityEngine.Input.mousePosition;
    }
}