using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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


        if (Static.scene == Static.Scene.Character)
        {
            if (this.scene is not SceneCharacter)
            {
                this.scene.Destroy();
                this.scene = new SceneCharacter();
            }
        }
        if (Static.scene == Static.Scene.Option)
        {
            if (this.scene is not SceneOption)
            {
                this.scene.Destroy();
                this.scene = new SceneOption();
            }
        }
    }

}
public class SceneCharacter : Scene
{
    RenderCharacter renderCharacter = new RenderCharacter();
    private int[,] array = new int[2, 4];
    Vector2 vector2;
    public SceneCharacter()
    {
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

        this.renderCharacter.Update(this.vector2);
    }

    private bool Error(Vector2 v)
    {
        if (v.x > this.array.GetLength(1) - 1) return true;
        if (v.x < 0) return true;
        if (v.y < 1 - this.array.GetLength(0)) return true;
        if (v.y > 0) return true;
        return false;
    }
    public override void Destroy()
    {
        this.renderCharacter.Destroy();
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
        this.cursor[1] = this.NewSprite(new Vector2(-1000, -1000), new Vector2(330, 330), new Vector2(0, 0), Resources.Load<Sprite>("Cursor"), UnityEngine.Color.HSVToRGB(Random.Range(0, 1f), 0.5f, 1));

    }
    public void Update(Vector2 vector2)
    {
        this.cursor[0].localPosition = new Vector2(vector2.x, vector2.y) * 400 + new Vector2(-this.crt.sizeDelta.x * 0.5f + 400, -this.crt.sizeDelta.y * 0.5f + 700);
        if (Render.Contact(UnityEngine.Input.mousePosition, this.rt, this.crt.sizeDelta))
        {
            if (UnityEngine.Input.GetMouseButtonDown(0)) Static.scene = Static.Scene.Option;
        }

    }
}
public class SceneOption : Scene
{
    RenderOption renderOption = new RenderOption();
    public override void Update(Vector2 v)
    {

        this.renderOption.Update();
    }
    public override void Destroy()
    {
        this.renderOption.Destroy();
    }
}


public class RenderOption : Render
{
    Slider slider;
    TextMeshPro textMeshPro;
    RectTransform rt;

    public RenderOption()
    {
        this.NewSlider(new Vector2(300, 300), new Vector2(300, 50), new Vector2(0, 0.5f), 25, 75);

        this.crt.AddComponent<GraphicRaycaster>();
        this.crt.AddComponent<EventSystem>();
        this.crt.AddComponent<StandaloneInputModule>();

        this.rt = this.NewSprite(new Vector2(-50, -50), new Vector2(100, 100), new Vector2(1f, 1f), Resources.Load<Sprite>("Square"), UnityEngine.Color.HSVToRGB(0, 0, 1));

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
        // textMeshPro.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
        // textMeshPro.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
        // textMeshPro.GetComponent<RectTransform>().anchoredPosition = new Vector3(50, 0, 1);
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

        if (Render.Contact(UnityEngine.Input.mousePosition, this.rt, this.crt.sizeDelta))
        {
            if (UnityEngine.Input.GetMouseButtonDown(0)) Static.scene = Static.Scene.Character;
        }
    }
}


public class Scene
{
    virtual public void Update(Vector2 v) { }
    virtual public void Destroy() { }
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
    public void Destroy()
    {
        _monoBehaviour.Destroy(this.gameObject);
    }
}
public class Static
{
    public enum Scene
    {
        Main,
        Character,
        Option
    }
    public static Scene scene = 0;
}
