using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Main
{
    static Scene scene = new SceneMode();
    private Input input = new Input();
    public Main()
    {
        Application.targetFrameRate = 60;
    }
    public void Update()
    {
        Main.scene.Update();
    }
    static public void NewScene(Type t)
    {
        if (t == Main.scene.GetType()) return;
        Main.scene.Destroy();
        switch (t.Name)
        {
            case nameof(ScenePlay):
                Main.scene = new ScenePlay();
                break;
            case nameof(SceneCharacter):
                Main.scene = new SceneCharacter();
                break;
            case nameof(SceneOption):
                Main.scene = new SceneOption();
                break;
            case nameof(SceneMode):
                Main.scene = new SceneMode();
                break;
        }
    }
}





public class Scene
{
    protected Render render;
    public void Destroy()
    {
        this.render.Destroy();
    }
    virtual public void Update()
    {
        this.render.Update();
    }
}

public class Render
{
    private readonly GameObject gameObject;
    protected readonly RectTransform canvas;
    protected RectTransform back;
    protected RectTransform enter;

    public Render()
    {
        this.gameObject = new GameObject();

        Camera camera = NewGameObject().AddComponent<Camera>();
        camera.backgroundColor = UnityEngine.Color.HSVToRGB(0, 0, 0.5f);
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.orthographic = true;

        Canvas canvas = NewGameObject().AddComponent<Canvas>();
        canvas.renderMode = UnityEngine.RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = camera;
        canvas.AddComponent<CanvasScaler>().referenceResolution = new Vector2(2000, 1000);
        canvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        this.canvas = canvas.GetComponent<RectTransform>();

        this.back = this.NewSprite(new Vector2(50, -50), new Vector2(100, 100), new Vector2(0f, 1f), Resources.Load<Sprite>("Square"), UnityEngine.Color.HSVToRGB(0, 0, 1));
        this.enter = this.NewSprite(new Vector2(-50, -50), new Vector2(100, 100), new Vector2(1f, 1f), Resources.Load<Sprite>("Square"), UnityEngine.Color.HSVToRGB(0, 0, 1));

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
        gameObject.transform.SetParent(this.canvas.transform, false);
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
    public void Destroy(GameObject gameObject) => _monoBehaviour.Destroy(gameObject);
    public void Destroy() => _monoBehaviour.Destroy(this.gameObject);
    virtual public void Update()
    {

    }
}