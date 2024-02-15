using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Main : MonoBehaviour
{
    static public Vector2 DOWN = Vector2.down * 0.1f * 0.5f;
    static public Vector2 PUYO_DOWN = Vector2.down * 0.3f * 0.5f;
    static public int REMOVE = 20 * 2;
    static public int FREEZE = 10 * 2;
    static public int BREAK = 15 * 2;

    private Color color;
    private Collision collision;
    private Factory factory;
    private Input input;
    private Render render;
    private PuyoPuyo puyoPuyo;
    private Remove remove;


    void Start()
    {
        Application.targetFrameRate = 60;

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
        this.Reset();
    }
    private void Reset()
    {
        this.puyoPuyo = null;
        this.color.Reset();
        this.factory.Reset();
        this.input.Reset();
    }

    void Update()
    {
        // Stopwatch stopwatch = new Stopwatch();

        if (this.puyoPuyo == null)
        {
            this.puyoPuyo = this.factory.NewPuyoPuyo(this.color);
            foreach (Puyo p in this.puyoPuyo.GetArray())
            {
                if (this.collision.Get(p) != null)
                {
                    this.Reset();
                    return;
                }
            }
            this.input.SetDown(false);
        }

        this.factory.Remove();
        this.factory.Sort();

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
            if (v == Vector2.up)
            {
                this.puyoPuyo.Drop(this.collision);
            }
            else if (v == Vector2.right + Vector2.up)
            {
                this.puyoPuyo.Rotate(this.collision);
            }
            else if (v != Vector2.zero) this.puyoPuyo.Move(v, this.collision);
        }

        if (this.puyoPuyo == null) this.remove = new Remove();
        if (this.remove != null)
        {
            if (this.remove.Ready(this.factory.GetList()))
            {
                if (!this.remove.Execute(new Board(this.factory.GetList()))) this.remove = null;
            }
        }

        foreach (Puyo p in this.factory.GetList()) this.render.Puyo(p);

        // stopwatch.Stop();
        // long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        // UnityEngine.Debug.Log("処理時間: " + elapsedMilliseconds + "ミリ秒");
    }
}
