using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play
{
    private RenderPlay renderPlay = new RenderPlay();
    public void Update()
    {
        this.renderPlay.Update();
    }
}

public class Factory
{
    public List<Puyo> list = new List<Puyo>();
}


public class Puyo
{
    int color;
    Vector2 position;
}

public class Gravity
{
    int color;
    Vector2 position;

}

public class Freeze
{
    Vector2 position;
    Vector2 Lastposition;

}
public class Move
{
    Vector2 position;
    Vector2 Lastposition;

}
