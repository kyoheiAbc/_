using UnityEngine;
class Input
{
    private Vector2 position;
    public Vector2 Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Return)) return Vector2.right + Vector2.down;
        if (UnityEngine.Input.GetKeyDown(KeyCode.RightArrow)) return Vector2.right;
        if (UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow)) return Vector2.left;
        if (UnityEngine.Input.GetKeyDown(KeyCode.UpArrow)) return Vector2.up;
        if (UnityEngine.Input.GetKeyDown(KeyCode.DownArrow)) return Vector2.down;

        for (int i = 0; i < UnityEngine.Input.touchCount; i++)
        {
            Touch t = UnityEngine.Input.GetTouch(i);
            if (t.position.x < 0.5f * Screen.width) break;
            if (t.phase == TouchPhase.Began) return Vector2.right + Vector2.down;
        }
        for (int i = 0; i < UnityEngine.Input.touchCount; i++)
        {
            Touch t = UnityEngine.Input.GetTouch(i);
            if (t.position.x > 0.5f * Screen.width) break;
            if (t.phase == TouchPhase.Began)
            {
                this.position = t.position;
            }
            Vector2 v = (Vector2)t.position - this.position;
            this.position += v;
            if (v.x > 96) return Vector2.right;
            if (v.x < -96) return Vector2.left;
            if (v.y > 96) return Vector2.up;
            if (v.y < -96) return Vector2.down;
            this.position -= v;
        }
        return Vector2.zero;
    }
    public static Vector2 GetTouchPosition()
    {
        for (int i = 0; i < UnityEngine.Input.touchCount; i++)
        {
            if (UnityEngine.Input.GetTouch(i).position.x > 0.5f * Screen.width) continue;
            return UnityEngine.Input.GetTouch(i).position;
        }
        return Vector2.zero;
    }
}