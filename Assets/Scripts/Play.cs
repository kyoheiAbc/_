using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Play
{
    private RenderPlay renderPlay = new RenderPlay();
    private List<Puyo> list = new List<Puyo>();

    public Play()
    {
        this.list.Add(new Puyo(new Vector2(3.5f, 1.5f)));


    }

    public void Update()
    {
        foreach (Puyo l in this.list)
        {
        }

        this.renderPlay.Update();
    }
}



