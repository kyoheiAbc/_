using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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
public class SceneMode : Scene
{
    public SceneMode()
    {
        this.render = new RenderMode();
    }

}

public class RenderMode : Render
{
    RectTransform[] rectTransform = new RectTransform[6];
    RectTransform cursor;
    public int i = 0;

    public RenderMode()
    {
        for (int i = 0; i < rectTransform.Length; i++)
        {
            this.rectTransform[i] = this.NewSprite(new Vector2(0, i * -150 + 375), new Vector2(500, 100), new Vector2(0.5f, 0.5f), Resources.Load<Sprite>("Square"), UnityEngine.Color.HSVToRGB(0, 0, 1));
            TextMeshPro textMeshPro;
            textMeshPro = this.NewGameObject().AddComponent<TextMeshPro>();
            textMeshPro.transform.SetParent(this.rectTransform[i], false);
            textMeshPro.GetComponent<RectTransform>().localScale = new Vector3(10, 10, 1);
            textMeshPro.enableWordWrapping = false;
            textMeshPro.alignment = TextAlignmentOptions.Center;
            textMeshPro.sortingOrder = 1;
            textMeshPro.text = "MODE " + i.ToString();
            if (i == 2) textMeshPro.text = "FREE BATTLE";
            if (i == 5) textMeshPro.text = "OPTION"; ;

            textMeshPro.color = UnityEngine.Color.black;
        }


        this.Destroy(this.back.gameObject);

        this.cursor = this.NewSprite(new Vector2(0, 375), new Vector2(500, 100), new Vector2(0, 0), Resources.Load<Sprite>("Cursor"), UnityEngine.Color.HSVToRGB(UnityEngine.Random.Range(0, 1f), 0.5f, 1));
        Render.Put((Vector2)this.rectTransform[0].localPosition + this.canvas.sizeDelta * 0.5f, this.cursor, this.canvas.sizeDelta);

    }
    override public void Update()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < rectTransform.Length; i++)
            {
                if (Render.Contact(UnityEngine.Input.mousePosition, this.rectTransform[i], this.canvas.sizeDelta))
                {
                    this.i = i;
                    Render.Put((Vector2)this.rectTransform[i].localPosition + this.canvas.sizeDelta * 0.5f, this.cursor, this.canvas.sizeDelta);
                    return;
                }
            }
            if (Render.Contact(UnityEngine.Input.mousePosition, this.enter, this.canvas.sizeDelta))
            {
                switch (this.i)
                {
                    case 2:
                        Main.NewScene(typeof(SceneCharacter));
                        break;
                    case 5:
                        Main.NewScene(typeof(SceneOption));
                        break;
                }
                return;
            }

        }
    }


}
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
public class SceneOption : Scene
{
    public SceneOption()
    {
        this.render = new RenderOption();
    }


}


public class RenderOption : Render
{
    Slider slider;
    TextMeshPro textMeshPro;

    public RenderOption()
    {
        this.NewSlider(new Vector2(300, 300), new Vector2(300, 50), new Vector2(0, 0.5f), 25, 75);

        this.canvas.AddComponent<GraphicRaycaster>();
        this.canvas.AddComponent<EventSystem>();
        this.canvas.AddComponent<StandaloneInputModule>();

        this.Destroy(this.enter.gameObject);
    }
    private void NewSlider(Vector2 position, Vector2 size, Vector2 anchor, int min, int max)
    {
        RectTransform rectTransform = this.NewSprite(position, size, anchor, Resources.Load<Sprite>("Square"), UnityEngine.Color.HSVToRGB(0, 0, 0.25f));
        this.NewSprite(Vector2.zero, Vector2.zero, Vector2.zero, Resources.Load<Sprite>("Square"), UnityEngine.Color.HSVToRGB(0, 0, 0.75f)).SetParent(rectTransform, false);
        this.slider = rectTransform.AddComponent<Slider>();
        this.slider.fillRect = rectTransform.GetChild(0).GetComponent<RectTransform>();
        this.slider.minValue = min;
        this.slider.maxValue = max;
        this.slider.wholeNumbers = true;

        TextMeshPro textMeshPro;
        textMeshPro = this.NewGameObject().AddComponent<TextMeshPro>();
        textMeshPro.transform.SetParent(rectTransform, false);
        textMeshPro.GetComponent<RectTransform>().localScale = new Vector3(10, 10, 1);
        textMeshPro.enableWordWrapping = false;
        textMeshPro.alignment = TextAlignmentOptions.Center;
        textMeshPro.sortingOrder = 1;
        this.textMeshPro = textMeshPro;

        textMeshPro = this.NewGameObject().AddComponent<TextMeshPro>();
        textMeshPro.transform.SetParent(rectTransform, false);
        textMeshPro.GetComponent<RectTransform>().localScale = new Vector3(10, 10, 1);
        textMeshPro.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
        textMeshPro.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
        textMeshPro.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 50, 1);
        textMeshPro.enableWordWrapping = false;
        textMeshPro.alignment = TextAlignmentOptions.Center;
        textMeshPro.text = "SLIDER SLIDER";
        textMeshPro.sortingOrder = 1;
    }
    override public void Update()
    {
        this.textMeshPro.text = slider.value.ToString();

        if (Render.Contact(UnityEngine.Input.mousePosition, this.back, this.canvas.sizeDelta))
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Main.NewScene(typeof(SceneMode));
            }
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