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
            Vector2 v = this.input.Update();
            PuyoPuyo puyoPuyo = this.factory.GetPuyoPuyo();


        }


        // {
        //     foreach (Puyo p in this.factory.GetList())
        //     {
        //         p.Update(this.factory.GetList());
        //     }
        // }


        {
            this.render.Puyo(this.factory.GetList());
            this.render.NextColor(this.factory.nextColor.array);
        }

    }
}