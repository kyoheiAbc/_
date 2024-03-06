using UnityEngine;
public class Main
{
    private Play play = new Play();
    public Main()
    {
        Application.targetFrameRate = 60;
    }
    public void Update()
    {
        this.play.Update();
    }
}