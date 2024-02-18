using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    static public Vector2 DOWN = Vector2.down * 0.1f * 0.5f;
    static public Vector2 PUYO_DOWN = Vector2.down * 0.3f * 0.5f;
    static public int REMOVE = 20 * 2;
    static public int FREEZE = 10 * 2;
    static public int BREAK = 15 * 2;

    private Collision collision;
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

        this.collision = new Collision(this.factory.GetList());
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
            Vector2 v = this.input.Update(this.render.camera);
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

        if (this.puyoPuyo == null && this.remove == null) this.remove = new Remove();
        if (this.remove != null)
        {
            if (this.remove.Ready(this.factory.GetList()))
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
