using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Main
{
    // private Scene scene = new SceneCharacter();
    // private Input input = new Input();
    public Main()
    {
        Application.targetFrameRate = 60;
        new RenderOption();

    }
    public void Update()
    {
        // this.scene.Update(this.input.Update());
    }
}


public class SceneCharacter : Scene
{
    RenderCharacter renderCharacter;

    public SceneCharacter()
    {
        // this.renderCharacter = new RenderCharacter();
    }
    public override void Update(Vector2 v)
    {
        this.renderCharacter.Update(v);
    }
}
public class RenderCharacter : Render
{
    RectTransform rt;
    RectTransform[] cursor = new RectTransform[2];

    public RenderCharacter()
    {
        for (int y = 0; y < 2; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                this.NewSprite(new Vector2(x * 400 + 400, -y * 400 + 700), new Vector2(300, 300), new Vector2(0f, 0f), Resources.Load<Sprite>("Square"), UnityEngine.Color.HSVToRGB(Random.Range(0, 1f), 0.5f, 1));
            }
        }
        this.rt = this.NewSprite(new Vector2(-50, -50), new Vector2(100, 100), new Vector2(1f, 1f), Resources.Load<Sprite>("Square"), UnityEngine.Color.HSVToRGB(0, 0, 1));
        this.cursor[0] = this.NewSprite(new Vector2(400, 700), new Vector2(330, 330), new Vector2(0, 0), Resources.Load<Sprite>("Cursor"), UnityEngine.Color.HSVToRGB(Random.Range(0, 1f), 0.5f, 1));
        this.cursor[1] = this.NewSprite(new Vector2(400, 700), new Vector2(330, 330), new Vector2(0, 0), Resources.Load<Sprite>("Cursor"), UnityEngine.Color.HSVToRGB(Random.Range(0, 1f), 0.5f, 1));

    }
    public void Update(Vector2 v)
    {
        if (Render.Contact(UnityEngine.Input.mousePosition, this.rt, this.crt.sizeDelta))
        {
            Debug.Log("Contact");
        }
        this.cursor[0].localPosition += (Vector3)v * 400;
    }

}

public class RenderOption : Render
{
    public RenderOption()
    {
        this.NewSlider(new Vector2(300, 300), new Vector2(300, 50), new Vector2(0, 0.5f), 25, 75);

    }
    private Slider NewSlider(Vector2 position, Vector2 size, Vector2 anchor, int min, int max)
    {
        RectTransform rectTransform = this.NewSprite(position, size, anchor, Resources.Load<Sprite>("Square"), UnityEngine.Color.HSVToRGB(0, 0, 0.25f));
        this.NewSprite(Vector2.zero, Vector2.zero, Vector2.zero, Resources.Load<Sprite>("Square"), UnityEngine.Color.HSVToRGB(0, 0, 0.75f)).SetParent(rectTransform, false);
        Slider slider = rectTransform.AddComponent<Slider>();
        slider.fillRect = rectTransform.GetChild(0).GetComponent<RectTransform>();
        slider.minValue = min;
        slider.maxValue = max;
        slider.wholeNumbers = true;
        return slider;
    }
}


public class Scene
{
    virtual public void Update(Vector2 v) { }
}

public class Render
{
    private readonly GameObject gameObject;
    protected readonly RectTransform crt;
    public Render()
    {
        this.gameObject = new GameObject();

        Camera camera = NewGameObject().AddComponent<Camera>();
        camera.backgroundColor = UnityEngine.Color.HSVToRGB(0, 0, 0.5f);
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.orthographic = true;

        Canvas canvas = NewGameObject().AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = camera;
        canvas.AddComponent<CanvasScaler>().referenceResolution = new Vector2(2000, 1000);
        canvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        this.crt = canvas.GetComponent<RectTransform>();
    }

    protected GameObject NewGameObject()
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.SetParent(this.gameObject.transform, false);
        return gameObject;
    }
    protected RectTransform NewSprite(Vector2 position, Vector2 size, Vector2 anchor, Sprite sprite, Color color)
    {
        GameObject gameObject = NewGameObject();
        gameObject.transform.SetParent(this.crt.transform, false);
        RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
        rectTransform.anchorMax = anchor;
        rectTransform.anchorMin = anchor;
        rectTransform.anchoredPosition = position;
        rectTransform.sizeDelta = size;
        gameObject.AddComponent<Image>().sprite = sprite;
        gameObject.GetComponent<Image>().color = color;
        return rectTransform;
    }
    static protected bool Contact(Vector2 point, RectTransform rectTransform, Vector2 cs)
    {
        float f = Screen.width / cs.x;
        point = point / f - cs * 0.5f;
        if (point.x > rectTransform.localPosition.x + rectTransform.sizeDelta.x * 0.5f) return false;
        if (point.x < rectTransform.localPosition.x - rectTransform.sizeDelta.x * 0.5f) return false;
        if (point.y > rectTransform.localPosition.y + rectTransform.sizeDelta.y * 0.5f) return false;
        if (point.y < rectTransform.localPosition.y - rectTransform.sizeDelta.y * 0.5f) return false;
        return true;
    }
    static protected void Put(Vector2 point, RectTransform rectTransform, Vector2 cs)
    {
        float f = Screen.width / cs.x;
        rectTransform.localPosition = (Vector3)point / f - (Vector3)cs * 0.5f;
    }
}
