using System.Collections.Generic;
using UnityEngine;
public class Main : MonoBehaviour
{
    private Color color;
    private Collision collision;
    private Factory factory;
    private Input input;
    private Render render;
    private PuyoPuyo puyoPuyo;


    void Start()
    {
        Application.targetFrameRate = 30;

        this.gameObject.transform.position = new Vector3(0, 0, 0);

        Camera c = this.gameObject.AddComponent<Camera>();
        c.backgroundColor = UnityEngine.Color.HSVToRGB(0, 0, 0.5f);
        c.clearFlags = CameraClearFlags.SolidColor;
        c.orthographic = true;
        c.orthographicSize = 12;
        c.transform.position = new Vector3(4, 7, -1);

        SpriteRenderer s = new GameObject("").AddComponent<SpriteRenderer>();
        s.color = UnityEngine.Color.HSVToRGB(2 / 3f, 1f, 1f);
        s.sprite = Resources.Load<Sprite>("Square");
        s.transform.localScale = new Vector3(6, 12, 0);
        s.transform.position = new Vector3(4, 7, 0);

        this.input = new Input(this.gameObject.GetComponent<Camera>());
        this.color = new Color();
        this.render = new Render();
        this.factory = new Factory();

        this.collision = new Collision(this.factory.GetList());

    }

    void Update()
    {
        if (this.puyoPuyo == null) this.puyoPuyo = this.factory.NewPuyoPuyo(this.color);

        this.factory.ListSort();
        float y = this.puyoPuyo.GetPosition().y;
        bool b = false;
        Puyo[] a = this.puyoPuyo.GetArray();
        foreach (Puyo p in this.factory.GetList())
        {
            if (a[0] == p) continue;
            if (a[1] == p) continue;
            if (!b && p.GetPosition().y > y)
            {
                b = true;
                this.puyoPuyo.Update(this.collision);
            }
            p.Update(this.collision);
        }

        if (this.puyoPuyo.GetArray()[0] == null) this.puyoPuyo = null;
        else
        {
            Vector2 v = this.input.Update();
            if (v != Vector2.zero) this.puyoPuyo.Move(v, this.collision);
        }


        foreach (Puyo p in this.factory.GetList()) this.render.Puyo(p);
    }
}
