using System.Collections.Generic;
using UnityEngine;
public class Main : MonoBehaviour
{
    private Factory factory;
    private Render render;
    private Input input;
    private Combo combo;
    private Bot bot;
    private Offset offset;
    public static List<CustomGameObject> list = new List<CustomGameObject>();
    void Awake()
    {
        Application.targetFrameRate = 60;

        this.gameObject.transform.position = Vector3.zero;

        this.factory = new Factory();
        this.render = new Render();
        this.input = new Input(this.render.camera);
        this.combo = new Combo();
        this.bot = new Bot();
        this.offset = new Offset();
    }
    void Start()
    {
        this.factory.Start();
        this.render.Start();
        this.input.Start();
        this.combo.Start();
        this.bot.Start();
        this.offset.Start();
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

        Fire fire = null;
        {
            if (Fire.Ready(this.factory.puyoPuyo, this.factory.list))
            {
                fire = new Fire(new Board(this.factory.list));
                fire.Execute();
            }
        }


        {
            if (fire != null)
            {
                this.combo.Add(fire.i);
                if (fire.i == 0) this.combo.end.Start();
            }
            this.combo.Update();
        }

        {
            this.bot.Update();
        }

        {
            this.offset.Update();
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