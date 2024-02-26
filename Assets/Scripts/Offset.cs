using UnityEditor.Rendering;
using UnityEngine;

public class Offset
{
    public int temporary;
    private int i;

    public Offset()
    {
        this.Start();

    }
    public void Start()
    {
        this.temporary = 0;
        this.i = 0;
    }
    public void Update()
    {

    }

    public void Push()
    {
        this.i += this.temporary;
        this.temporary = 0;
    }


}