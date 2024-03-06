using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play
{
    private RenderPlay renderPlay = new RenderPlay();

    private Puyo puyo = new Puyo();
    public void Update()
    {


        this.renderPlay.Update();
        this.renderPlay.Puyo(this.puyo);
    }
}


public class Puyo
{
    int color = 0;
    Vector2 position = new Vector2(3.5f, 12.5f);
    public Vector2 Position() { return this.position; }
}
