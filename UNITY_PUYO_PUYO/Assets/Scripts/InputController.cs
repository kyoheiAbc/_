using UnityEngine;

public class InputController
{
    Camera cam;
    Vector2 pos;
    int ctrl;

    public InputController()
    {
        cam = Camera.main;
    }

    public void init()
    {
        pos = D.I().VEC_0;
        ctrl = 0;
    }

    public int update()
    {
        if (Input.GetMouseButtonDown(0) && ctrl == 0)
        {
            pos = cam.ScreenToWorldPoint((Vector2)Input.mousePosition);
            ctrl = 1;
        }
        else if (Input.GetMouseButton(0) && ctrl > 0)
        {
            Vector2 p = (Vector2)cam.ScreenToWorldPoint((Vector2)Input.mousePosition);
            Vector2 d = p - pos;

            if (d.x > 1)
            {
                ctrl = 2;
                pos = p;
                return 6;
            }
            if (d.x < -1)
            {
                ctrl = 2;
                pos = p;
                return 4;
            }
            if (d.y > 1f)
            {
                ctrl = 2;
                pos = p;
                return 8;
            }
            if (d.y < -0.5)
            {
                ctrl = 2;
                pos = p;
                return 2;
            }

            // if (Mathf.Abs(d.x) > Mathf.Abs(d.y))
            // {
            //     return 5 + (int)Mathf.Sign(d.x);
            // }
            // else
            // {
            //     return 5 + (int)Mathf.Sign(d.y) * 3;
            // }
        }
        else if (Input.GetMouseButtonUp(0) && ctrl > 0)
        {
            if (ctrl == 2)
            {
                init();
                return 0;
            }
            init();
            return 15 + (int)Mathf.Sign(cam.ScreenToWorldPoint((Vector2)Input.mousePosition).x - 4);
        }

        return 0;
    }
}
