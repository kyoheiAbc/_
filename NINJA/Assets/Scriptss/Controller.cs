using UnityEngine;
public class Controller
{
    int button; public int getButton() { return this.button; }
    Vector2 deltaPosition; public Vector3 getDeltaPosition() { return new Vector3(this.deltaPosition.x, 0, this.deltaPosition.y); }
    Vector2[] stick; public Vector3 getStick() { return new Vector3(this.stick[1].x, 0, this.stick[1].y); }
    Vector3 touchPhaseBegan; public Vector3 getTouchPhaseBegan() { return this.touchPhaseBegan; }

    public Controller()
    {
        this.stick = new Vector2[2];
    }

    public void update()
    {
        this.button = 0;
        this.deltaPosition = Vector2.zero;
        this.stick[1] = Vector2.zero;
        this.touchPhaseBegan = Vector3.zero;

        int c = 0;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            if (t.phase == TouchPhase.Began)
            {
                if (t.position.x > 0.75f * Screen.width && t.position.y < 0.5f * Screen.height)
                {
                    this.button |= 1;
                }

                if (t.position.x < 0.5f * Screen.width)
                {
                    this.stick[0] = t.position;
                }

                this.touchPhaseBegan = t.position;
            }

            if (t.phase == TouchPhase.Ended)
            {
                if (t.position.x > 0.5f * Screen.width)
                {
                    if (t.deltaPosition.sqrMagnitude > Mathf.Pow((Screen.width * 0.01f), 2))
                    {
                        this.button |= 4;
                        this.deltaPosition = t.deltaPosition;
                    }
                }
            }

            if (t.position.x > 0.5f * Screen.width)
            {
                c++;
                if (c > 1) this.button |= 2;
            }

            if (t.position.x < 0.5f * Screen.width)
            {
                this.stick[1] = t.position - this.stick[0];
            }
        }

        if (Input.GetKeyDown(KeyCode.Z)) this.button |= 1;
        if (Input.GetKeyDown(KeyCode.X)) this.button |= 2;
        if (Input.GetKeyDown(KeyCode.C)) this.button |= 4;
    }
}