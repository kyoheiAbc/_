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
        this.renderCharacter.Update();
    }
}
public class RenderCharacter : Render
{
    RectTransform rt;
    public RenderCharacter(Vector2 position, float orthographicSize)
    {

        this.rt = this.NewSprite(new Vector2(500, 200), new Vector2(300, 400), new Vector2(0, 0.5f), Resources.Load<Sprite>("Square"));
    }
    public void Update()
    {
        if (Render.Contact(UnityEngine.Input.mousePosition, this.rt, this.crt.sizeDelta))
        {
            Debug.Log("Contact");
        }
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

    private GameObject NewGameObject()
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.SetParent(this.gameObject.transform, false);
        return gameObject;
    }
    protected RectTransform NewSprite(Vector2 position, Vector2 size, Vector2 anchor, Sprite sprite)
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
