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
            if (this.factory.puyoPuyo == null)
            {
                if (!Factory.garbagePuyoMove(this.factory.list))
                {
                    this.factory.NewPuyoPuyo();
                    this.input.Start();
                    if (Collision.Get(this.factory.puyoPuyo.array[0], this.factory.list) != null ||
                        Collision.Get(this.factory.puyoPuyo.array[1], this.factory.list) != null)
                    {
                        this.Start();
                        return;
                    }
                }
            }
        }

        {
            Vector2 v = this.input.Update();
            if (v != Vector2.zero && this.factory.puyoPuyo != null)
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
            if (this.bot.health <= 0) this.Start();
        }

        {
            this.offset.Update(this.factory, this.combo, this.bot);
        }

        {
            this.render.Puyo(this.factory.list);
            this.render.NextColor(this.factory.nextColor.array);
            this.render.Combo(this.combo.i);
            this.render.Bot(this.bot.health);
            this.render.GarbagePuyo(this.offset);
            this.render.Attack(this.combo, this.bot);
        }

        {
            for (int i = Main.list.Count - 1; i >= 0; i--)
            {
                Main.list[i].Update();
            }
        }
    }
}