using System.Collections.Generic;
using UnityEngine;
public class Main : MonoBehaviour
{
    private Factory factory;
    private Render render;
    private Input input;
    void Awake()
    {
        Application.targetFrameRate = 60;

        this.gameObject.transform.position = Vector3.zero;

        this.factory = new Factory();
        this.render = new Render();
        this.input = new Input(this.render.camera);
    }
    void Start()
    {
        this.factory.Start();
        this.render.Start();
        this.input.Start();
    }
    void Update()
    {
        {
            this.NextPuyoPuyo(this.factory, this.input);
        }

        {
            this.Control(this.factory.puyoPuyo, this.input.Update());
        }

        {
            this._Update(this.factory.puyoPuyo, this.factory.list);
        }

        {
            this.render.Puyo(this.factory.list);
            this.render.NextColor(this.factory.nextColor.array);
        }
    }

    private void NextPuyoPuyo(Factory factory, Input input)
    {
        if (factory.puyoPuyo.disconnect.Finish())
        {
            factory.NewPuyoPuyo();
            input.Start();
        }
    }

    private void Control(PuyoPuyo puyoPuyo, Vector2 v)
    {
        if (v == Vector2.zero) return;
        else if (v == Vector2.up) puyoPuyo.Drop(this.factory.list);
        else if (v == Vector2.right + Vector2.down) puyoPuyo.rotatePuyoPuyo.Execute(this.factory.list);
        else puyoPuyo.movePuyoPuyo.Execute(v, this.factory.list);
    }

    private void _Update(PuyoPuyo puyoPuyo, List<Puyo> list)
    {
        float y = puyoPuyo.GetPosition().y;
        bool b = false;
        foreach (Puyo l in list)
        {
            if (puyoPuyo.array[0] == l) continue;
            if (puyoPuyo.array[1] == l) continue;

            if (!b && l.position.y > y)
            {
                b = true;
                puyoPuyo.Update(list);
            }
            l.Update(list);
        }

    }

}