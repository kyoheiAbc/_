using UnityEngine;
public class SceneCharacter : Scene
{
    public SceneCharacter()
    {
        this.render = new RenderCharacter();
    }


}
public class RenderCharacter : Render
{
    RectTransform[] rt = new RectTransform[2];

    RectTransform[] cursor = new RectTransform[2];

    RectTransform[] character = new RectTransform[8];


    public RenderCharacter()
    {
        for (int y = 0; y < 2; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                this.character[x + y * 4] = this.NewSprite(new Vector2(x * 400 + 400, -y * 400 + 700), new Vector2(300, 300), new Vector2(0f, 0f), Resources.Load<Sprite>("Square"), UnityEngine.Color.HSVToRGB(UnityEngine.Random.Range(0, 1f), 0.5f, 1));
            }
        }
        this.rt[0] = this.NewSprite(new Vector2(-50, -50), new Vector2(100, 100), new Vector2(1f, 1f), Resources.Load<Sprite>("Square"), UnityEngine.Color.HSVToRGB(0, 0, 1));
        this.rt[1] = this.NewSprite(new Vector2(50, -50), new Vector2(100, 100), new Vector2(0f, 1f), Resources.Load<Sprite>("Square"), UnityEngine.Color.HSVToRGB(0, 0, 1));
        this.cursor[0] = this.NewSprite(new Vector2(400, 700), new Vector2(330, 330), new Vector2(0, 0), Resources.Load<Sprite>("Cursor"), UnityEngine.Color.HSVToRGB(UnityEngine.Random.Range(0, 1f), 0.5f, 1));
        this.cursor[1] = this.NewSprite(new Vector2(-1000, -1000), new Vector2(330, 330), new Vector2(0, 0), Resources.Load<Sprite>("Cursor"), UnityEngine.Color.HSVToRGB(UnityEngine.Random.Range(0, 1f), 0.5f, 1));

    }
    override public void Update()
    {
        if (Render.Contact(UnityEngine.Input.mousePosition, this.back, this.canvas.sizeDelta))
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Main.NewScene(typeof(SceneMode));
            }
        }
        if (Render.Contact(UnityEngine.Input.mousePosition, this.enter, this.canvas.sizeDelta))
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Main.NewScene(typeof(ScenePlay));
            }
        }
        for (int i = 0; i < this.character.Length; i++)
        {
            if (Render.Contact(UnityEngine.Input.mousePosition, this.character[i], this.canvas.sizeDelta))
            {
                if (UnityEngine.Input.GetMouseButtonDown(0))
                {
                    Render.Put((Vector2)this.character[i].localPosition + this.canvas.sizeDelta * 0.5f, this.cursor[0], this.canvas.sizeDelta);
                }
            }
        }

    }
}