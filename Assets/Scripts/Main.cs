using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    static public Vector2 PUYO_PUYO_DOWN = Vector2.down * 0.075f * 0.5f;
    static public Vector2 PUYO_DOWN = Vector2.down * 0.075f * 0.5f * 4;
    static public int REMOVE = 20 * 2;
    static public int FREEZE = 10 * 2;
    static public int BREAK = 15 * 2;

    private Factory factory;
    private Input input;
    private Render render;
    private PuyoPuyo puyoPuyo;
    private Remove remove;
    private Combo combo;


    void Start()
    {
        Application.targetFrameRate = 60;

        this.gameObject.transform.position = Vector3.zero;

        this.input = new Input();
        this.render = new Render();
        this.factory = new Factory();
        this.combo = new Combo();
        this.remove = null;

        this.Reset();
    }
    private void Reset()
    {
        this.puyoPuyo = null;
        this.factory.Reset();
        this.input.Reset();
        this.combo.Reset();
        this.render.Reset();
    }

    void Update()
    {

        if (this.puyoPuyo == null)
        {
            this.puyoPuyo = this.factory.NewPuyoPuyo();
            foreach (Puyo p in this.puyoPuyo.GetArray())
            {
                if (Collision.Get(p, this.factory.GetList()) != null)
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
        Puyo[] a = this.puyoPuyo.GetArray();
        foreach (Puyo p in this.factory.GetList())
        {
            if (a[0] == p) continue;
            if (a[1] == p) continue;

            if (!b && p.GetPosition().y > y)
            {
                b = true;
                this.puyoPuyo.Update(this.factory.GetList());
            }
            p.Update(this.factory.GetList());
        }

        if (this.puyoPuyo.GetArray()[0] == null) this.puyoPuyo = null;
        else
        {
            Vector2 v = this.input.Update(this.render.camera);
            if (v == Vector2.up)
            {
                PuyoPuyo.Drop(puyoPuyo, this.factory.GetList());
            }
            else if (v == Vector2.right + Vector2.up)
            {
                Rotate.Execute(this.puyoPuyo, this.factory.GetList());
            }
            else if (v != Vector2.zero) Move.PuyoPuyo_(this.puyoPuyo, v, this.factory.GetList());
        }

        if (this.puyoPuyo == null && this.remove == null) this.remove = new Remove();
        if (this.remove != null)
        {
            List<Puyo> lst = new List<Puyo>(this.factory.GetList());

            if (this.puyoPuyo != null)
            {
                Puyo[] ary = this.puyoPuyo.GetArray();
                lst.Remove(ary[0]);
                lst.Remove(ary[1]);
            }
            if (this.remove.Ready(lst))
            {
                if (!this.remove.Execute(new Board(this.factory.GetList())))
                {
                    this.remove = null;
                }
            }
        }

        this.combo.Update(this.remove);


        foreach (Puyo p in this.factory.GetList()) this.render.Puyo(p);
        this.render.RenderNextPuyoPuyo(this.factory.GetNextColor());
        this.render.RenderCombo(this.combo);

    }
}

public class I
{
    public int i = 0;
    public void Update()
    {
        if (0 < i && i < 256) i++;
    }
    public void Start()
    {
        if (i == 0) i++;
    }

}