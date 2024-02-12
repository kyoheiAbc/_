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
    private Remove remove;


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
        this.remove = null;

        this.collision = new Collision(this.factory.GetList());

    }

    void Update()
    {
        if (this.puyoPuyo == null) this.puyoPuyo = this.factory.NewPuyoPuyo(this.color);

        this.factory.ListSort();
        float y = this.puyoPuyo.GetPosition().y;
        bool b = false;
        foreach (Puyo p in this.factory.GetList())
        {
            if (p.GetPuyoPuyo() != null) continue;
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

        if (this.puyoPuyo == null) this.remove = new Remove();
        if (this.remove != null)
        {
            if (this.remove.Ready(this.factory.GetList()))
            {
                this.remove.Execute(new Board(this.factory.GetList()));
                this.remove = null;
            }
        }

        foreach (Puyo p in this.factory.GetList()) this.render.Puyo(p);

        List<Puyo> l = this.factory.GetList();
        for (int i = l.Count - 1; i >= 0; i--)
        {
            if (l[i].GetRemove() && l[i].GetJ() == 30)
            {
                l.RemoveAt(i);
            }
        }
    }
}
