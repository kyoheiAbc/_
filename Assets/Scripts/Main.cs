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


        this.render.Puyo(this.factory.GetList());
        this.render.NextColor(this.factory.nextColor.array);


    }

    private void Control(PuyoPuyo puyoPuyo, Vector2 v)
    {
        if (v == Vector2.zero) return;
        else if (v == Vector2.up) puyoPuyo.Drop(this.factory.GetList());
        else if (v == Vector2.right + Vector2.down) puyoPuyo.rotatePuyoPuyo.Execute(this.factory.GetList());
        else puyoPuyo.movePuyoPuyo.Execute(v, this.factory.GetList());
    }

    private void _Update(PuyoPuyo puyoPuyo, List<Puyo> list)
    {
        float y = puyoPuyo.GetPosition().y;
        bool b = false;
        foreach (Puyo p in list)
        {
            if (puyoPuyo.array[0] == p) continue;
            if (puyoPuyo.array[1] == p) continue;

            if (!b && p.position.y > y)
            {
                b = true;
                puyoPuyo.Update(list);
            }
            p.Update(list);
        }

    }

}