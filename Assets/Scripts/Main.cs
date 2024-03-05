using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Main
{
    private Scene scene = new SceneCharacter();
    private Input input = new Input();
    public Main()
    {
        Application.targetFrameRate = 60;
    }
    public void Update()
    {
        this.scene.Update(this.input.Update());
    }
}


public class SceneCharacter : Scene
{
    RenderCharacter renderCharacter;
    public SceneCharacter()
    {
        this.renderCharacter = new RenderCharacter(Vector2.zero, 2);
    }
    public override void Update(Vector2 v)
    {
        this.renderCharacter.Update(this);
    }
}
public class RenderCharacter : Render
{
    RectTransform rt;
    public RenderCharacter(Vector2 position, float orthographicSize)
    {

        this.rt = this.NewSprite(new Vector2(500, 200), new Vector2(300, 400), new Vector2(0, 0.5f), Resources.Load<Sprite>("Square"));
    }
    public void Update(Scene scene)
    {
        // if (Render.Contact(UnityEngine.Input.mousePosition, this.rt, this.crt.sizeDelta))
        // {

        // }
        // Render.Put(UnityEngine.Input.mousePosition, this.rt, this.crt.sizeDelta);
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

    public GameObject NewGameObject()
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.SetParent(this.gameObject.transform, false);
        return gameObject;
    }
    public RectTransform NewSprite(Vector2 position, Vector2 size, Vector2 anchor, Sprite sprite)
    {
        GameObject gameObject = NewGameObject();
        gameObject.transform.SetParent(this.crt.transform, false);
        RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
        rectTransform.anchorMax = anchor;
        rectTransform.anchorMin = anchor;
        rectTransform.anchoredPosition = position;
        rectTransform.sizeDelta = size;
        gameObject.AddComponent<Image>().sprite = sprite;
        return rectTransform;
    }
    static public bool Contact(Vector2 point, RectTransform rectTransform, Vector2 cs)
    {
        float f = Screen.width / cs.x;
        point /= f;
        if (point.x > rectTransform.localPosition.x + cs.x * 0.5f + rectTransform.sizeDelta.x * 0.5f) return false;
        if (point.x < rectTransform.localPosition.x + cs.x * 0.5f - rectTransform.sizeDelta.x * 0.5f) return false;
        if (point.y > rectTransform.localPosition.y + cs.y * 0.5f + rectTransform.sizeDelta.y * 0.5f) return false;
        if (point.y < rectTransform.localPosition.y + cs.y * 0.5f - rectTransform.sizeDelta.y * 0.5f) return false;
        return true;
    }
    static public void Put(Vector2 point, RectTransform rectTransform, Vector2 cs)
    {
        float f = Screen.width / cs.x;

        rectTransform.localPosition = point / f;
        rectTransform.localPosition -= (Vector3)cs * 0.5f;
    }
}
