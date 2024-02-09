using System.Collections.Generic;
using UnityEngine;
public class Main : MonoBehaviour
{
    private Color color;
    private Collision collision;
    private Factory factory;
    private Input input;
    private Render render;

    private Puyo puyo;

    void Start()
    {
        Application.targetFrameRate = 30;

        this.gameObject.transform.position = new Vector3(0, 0, 0);

        Camera c = this.gameObject.AddComponent<Camera>();
        c.backgroundColor = UnityEngine.Color.HSVToRGB(0, 0, 0.5f);
        c.clearFlags = CameraClearFlags.SolidColor;
        c.orthographic = true;
        c.orthographicSize = 16;
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

        // this.factory.NewPuyo(this.color.Get(), new Vector2(2.5f, 6.5f), true);
        // this.factory.NewPuyo(this.color.Get(), new Vector2(2.5f, 8.5f), true);

        // this.factory.NewPuyo(this.color.Get(), new Vector2(5.5f, 6.5f), true);
        // this.factory.NewPuyo(this.color.Get(), new Vector2(5.5f, 7.5f), true);

        this.factory.NewPuyo(this.color.Get(), new Vector2(2.5f, 12.5f), false);
        this.factory.NewPuyo(this.color.Get(), new Vector2(2.5f, 10.49f), false);

        this.factory.NewPuyo(this.color.Get(), new Vector2(5.5f, 12.5f), false);
        this.factory.NewPuyo(this.color.Get(), new Vector2(5.5f, 10.51f), false);



        this.puyo = this.factory.NewPuyo(this.color.Get(), new Vector2(3.5f, 12.5f), false);
        this.factory.ListSort();

    }

    void Update()
    {
        Vector2 v = this.input.Update();

        if (v != Vector2.zero)
        {
            this.puyo.Move(v, this.collision);
        }
        this.factory.ListSort();

        foreach (Puyo p in this.factory.GetList())
        {
            p.Update(this.collision);

            this.render.Puyo(p);
        }
    }
}
