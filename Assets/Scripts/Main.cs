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
        Main.scene.Update(this.input.Update());
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
        }
    }
}
public class SceneMode : Scene
{
    public int i;
    public SceneMode()
    {
        this.render = new RenderMode(this);
    }

    public override void Update(Vector2 vector2)
    {
        this.i += (int)vector2.y;
        if (i < 0) i = 0;
        if (i > 5) i = 5;

        this.Render<RenderMode>().Update();

    }
}

public class RenderMode : Render
{
    RectTransform[] rectTransform = new RectTransform[6];
    RectTransform cursor;

    public RenderMode(SceneMode parent)
    {
        this.parent = parent;
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
            textMeshPro.color = UnityEngine.Color.black;
        }



        this.Destroy(this.rt.gameObject);

        this.cursor = this.NewSprite(new Vector2(0, 375), new Vector2(500, 100), new Vector2(0, 0), Resources.Load<Sprite>("Cursor"), UnityEngine.Color.HSVToRGB(UnityEngine.Random.Range(0, 1f), 0.5f, 1));

    }
    public void Update()
    {
        this.cursor.localPosition = new Vector2(0, this.Parent<SceneMode>().i) * 150 + new Vector2(-this.crt.sizeDelta.x * 0.5f, -this.crt.sizeDelta.y * 0.5f);

    }


}
public class ScenePlay : Scene
{
    public ScenePlay()
    {
        this.render = new RenderPlay();
    }
    public override void Update(Vector2 v)
    {
        base.Update(v);
        if (v == Vector2.right + Vector2.down)
        {
            Main.NewScene(typeof(SceneCharacter));
        }
    }
}
public class RenderPlay : Render
{

    public RenderPlay()
    {
    }

    public void Update(Vector2 vector2)
    {
        // if (Render.Contact(UnityEngine.Input.mousePosition, this.rt, this.crt.sizeDelta))
        // {
        //     if (UnityEngine.Input.GetMouseButtonDown(0))
        //     {
        //         Main.NewScene(typeof(SceneCharacter));

        //     }
        // }

    }
}

public class SceneCharacter : Scene
{
    private int[,] array = new int[2, 4];
    Vector2 vector2;
    public SceneCharacter()
    {
        this.render = new RenderCharacter();
        for (int y = 0; y < 2; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                this.array[y, x] = x + 4 * y;
            }
        }
    }
    public override void Update(Vector2 v)
    {
        this.vector2 += v;
        if (this.Error(this.vector2)) this.vector2 -= v;

        int i = this.array[-(int)this.vector2.y, (int)this.vector2.x];

        ((RenderCharacter)this.render).Update(this.vector2);
    }

    private bool Error(Vector2 v)
    {
        if (v.x > this.array.GetLength(1) - 1) return true;
        if (v.x < 0) return true;
        if (v.y < 1 - this.array.GetLength(0)) return true;
        if (v.y > 0) return true;
        return false;
    }
}
public class RenderCharacter : Render
{
    RectTransform[] rt = new RectTransform[2];

    RectTransform[] cursor = new RectTransform[2];

    public RenderCharacter()
    {
        for (int y = 0; y < 2; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                this.NewSprite(new Vector2(x * 400 + 400, -y * 400 + 700), new Vector2(300, 300), new Vector2(0f, 0f), Resources.Load<Sprite>("Square"), UnityEngine.Color.HSVToRGB(UnityEngine.Random.Range(0, 1f), 0.5f, 1));
            }
        }
        this.rt[0] = this.NewSprite(new Vector2(-50, -50), new Vector2(100, 100), new Vector2(1f, 1f), Resources.Load<Sprite>("Square"), UnityEngine.Color.HSVToRGB(0, 0, 1));
        this.rt[1] = this.NewSprite(new Vector2(50, -50), new Vector2(100, 100), new Vector2(0f, 1f), Resources.Load<Sprite>("Square"), UnityEngine.Color.HSVToRGB(0, 0, 1));
        this.cursor[0] = this.NewSprite(new Vector2(400, 700), new Vector2(330, 330), new Vector2(0, 0), Resources.Load<Sprite>("Cursor"), UnityEngine.Color.HSVToRGB(UnityEngine.Random.Range(0, 1f), 0.5f, 1));
        this.cursor[1] = this.NewSprite(new Vector2(-1000, -1000), new Vector2(330, 330), new Vector2(0, 0), Resources.Load<Sprite>("Cursor"), UnityEngine.Color.HSVToRGB(UnityEngine.Random.Range(0, 1f), 0.5f, 1));

    }
    public void Update(Vector2 vector2)
    {
        this.cursor[0].localPosition = new Vector2(vector2.x, vector2.y) * 400 + new Vector2(-this.crt.sizeDelta.x * 0.5f + 400, -this.crt.sizeDelta.y * 0.5f + 700);
        if (Render.Contact(UnityEngine.Input.mousePosition, this.rt[0], this.crt.sizeDelta))
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Main.NewScene(typeof(ScenePlay));
            }
        }
        if (Render.Contact(UnityEngine.Input.mousePosition, this.rt[1], this.crt.sizeDelta))
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Main.NewScene(typeof(SceneOption));

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
    public override void Update(Vector2 v)
    {

        ((RenderOption)this.render).Update();
    }
}


public class RenderOption : Render
{
    Slider slider;
    TextMeshPro textMeshPro;

    public RenderOption()
    {
        this.NewSlider(new Vector2(300, 300), new Vector2(300, 50), new Vector2(0, 0.5f), 25, 75);

        this.crt.AddComponent<GraphicRaycaster>();
        this.crt.AddComponent<EventSystem>();
        this.crt.AddComponent<StandaloneInputModule>();


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
    public void Update()
    {
        this.textMeshPro.text = slider.value.ToString();

        // if (Render.Contact(UnityEngine.Input.mousePosition, this.rt, this.crt.sizeDelta))
        // {
        //     if (UnityEngine.Input.GetMouseButtonDown(0))
        //     {
        //         Main.NewScene(typeof(SceneCharacter));
        //     }
        // }
    }
}


public class Scene
{
    protected Render render;
    virtual public void Update(Vector2 v) { }
    public void Destroy()
    {
        this.render.Destroy();
    }
    protected T Render<T>()
    {
        return (T)(object)this.render;
    }
}

public class Render
{
    private readonly GameObject gameObject;
    protected readonly RectTransform crt;
    protected RectTransform rt;
    protected Scene parent;

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
        this.crt = canvas.GetComponent<RectTransform>();

        this.rt = this.NewSprite(new Vector2(-50, -50), new Vector2(100, 100), new Vector2(1f, 1f), Resources.Load<Sprite>("Square"), UnityEngine.Color.HSVToRGB(0, 0, 1));

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
    public void Destroy(GameObject gameObject)
    {
        _monoBehaviour.Destroy(gameObject);
    }
    public void Destroy()
    {
        _monoBehaviour.Destroy(this.gameObject);
    }
    public void Update()
    {

    }

    protected T Parent<T>()
    {
        return (T)(object)this.parent;
    }
}