using UnityEngine;
public class ScenePlay : Scene
{
    public ScenePlay()
    {
        this.render = new RenderPlay();
    }


}
public class RenderPlay : Render
{

    public RenderPlay()
    {
        this.Destroy(this.enter.gameObject);
    }

    public override void Update()
    {
        if (Render.Contact(UnityEngine.Input.mousePosition, this.back, this.canvas.sizeDelta))
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Main.NewScene(typeof(SceneCharacter));
            }
        }
    }
}