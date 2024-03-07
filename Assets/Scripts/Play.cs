using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Play
{
    private RenderPlay renderPlay = new RenderPlay();

    public Play()
    {


    }

    public void Update()
    {


        this.renderPlay.Update();
    }
}



