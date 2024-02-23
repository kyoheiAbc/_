using System.Collections.Generic;
using UnityEngine;
public class Main : MonoBehaviour
{
    private Factory factory;
    private Render render;
    private Input input;
    private Combo combo;
    public static List<CustomGameObject> list = new List<CustomGameObject>();
    void Awake()
    {
        Application.targetFrameRate = 60;

        this.gameObject.transform.position = Vector3.zero;

        this.factory = new Factory();
        this.render = new Render();
        this.input = new Input(this.render.camera);
        this.combo = new Combo();
    }
    void Start()
    {
        this.factory.Start();
        this.render.Start();
        this.input.Start();
        this.combo.Start();
        Main.list.Clear();
    }
    void Update()
    {
        {
            if (this.factory.puyoPuyo == null && Collision.Get(new Vector2(3.5f, 12.5f), this.factory.list) != null)
            {
                this.Start();
                return;
            }
        }

        {
            if (this.factory.puyoPuyo == null)
            {
                this.factory.NewPuyoPuyo();
                this.input.Start();
            }
        }

        {
            Vector2 v = this.input.Update();
            if (v != Vector2.zero)
            {
                if (v == Vector2.right + Vector2.down) this.factory.puyoPuyo.rotatePuyoPuyo.Execute(this.factory.list);
                else if (v == Vector2.up) this.factory.puyoPuyo.Drop(this.factory.list);
                else this.factory.puyoPuyo.movePuyoPuyo.Execute(v, this.factory.list);

                if (this.factory.puyoPuyo.disconnect.Finish()) this.factory.puyoPuyo = null;
            }
        }

        {
            this.factory.Update();
        }

        {
            if (Fire.Ready(this.factory.puyoPuyo, this.factory.list))
            {
                int i = new Fire(new Board(this.factory.list)).Execute();
                if (i > 0)
                {
                    this.combo.Add(i);
                    this.combo.end.i = 0;
                }
                else
                {
                    this.combo.end.Start();
                }
            }
        }

        {
            this.combo.Update();
        }

        {
            this.render.Puyo(this.factory.list);
            this.render.NextColor(this.factory.nextColor.array);
            this.render.Combo(this.combo.i);
        }

        {
            for (int i = Main.list.Count - 1; i >= 0; i--)
            {
                Main.list[i].Update();
            }
        }
    }
}