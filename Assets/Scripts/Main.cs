using System;
using Unity.VisualScripting;
using UE = UnityEngine;
using UI = UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Main
{
    private Play play = new Play();
    public Main()
    {
        UE.Application.targetFrameRate = 60;
    }
    public void Update()
    {
        this.play.Update();
    }
}


public class Play
{
    private RenderPlay renderPlay = new RenderPlay();
    public void Update()
    {
        this.renderPlay.Update();
    }
}

public class RenderPlay
{
    private Render render = new Render();
    public RenderPlay()
    {



    }
    public void Update()
    {
        this.render.Update();

    }
}


public class Render
{
    private GameObject gameObject;
    private Canvas canvas;
    private Button back, enter;
    public Render()
    {
        this.gameObject = new GameObject();
        new Camera(this.gameObject);
        this.canvas = new Canvas(this.gameObject, this.gameObject.GetComponent<UE.Camera>());

        this.back = new Button(this.canvas, new Vector2(50, -50), new Vector2(100, 100), new Vector2(0f, 1f), Resources.Load<Sprite>("Square"));
        this.enter = new Button(this.canvas, new Vector2(-50, -50), new Vector2(100, 100), new Vector2(1f, 1f), Resources.Load<Sprite>("Square"));
    }

    public void Update()
    {
        if (this.back.Hit(this.canvas.ScreenPosition(UE.Input.mousePosition))) Debug.Log("a");
    }

    public Slider NewSlider()
    {
        return new Slider(this.canvas, new Vector2(0, 0), new Vector2(300, 50), new Vector2(0.5f, 0.5f), 25, 75);
    }
}


public class Slider
{
    private UI.Slider component;
    public Slider(Canvas canvas, Vector2 position, Vector2 size, Vector2 anchor, int min, int max)
    {
        RectTransform rectTransform = canvas.NewRectTransform(position, size, anchor);
        rectTransform.AddComponent<UI.Image>().sprite = Resources.Load<Sprite>("Square");
        this.component = rectTransform.AddComponent<UI.Slider>();

        RectTransform rT = canvas.NewRectTransform(Vector2.zero, Vector2.zero, Vector2.zero);
        rT.AddComponent<UI.Image>().sprite = Resources.Load<Sprite>("Square");
        rT.GetComponent<UI.Image>().color = UE.Color.green;
        rT.SetParent(rectTransform, false);

        this.component.fillRect = rectTransform.GetChild(0).GetComponent<RectTransform>();
        this.component.minValue = min;
        this.component.maxValue = max;
        this.component.wholeNumbers = true;

        TextMeshPro textMeshPro;
        textMeshPro = new GameObject().AddComponent<TextMeshPro>();
        textMeshPro.transform.SetParent(rectTransform, false);
        textMeshPro.GetComponent<RectTransform>().localScale = new Vector3(10, 10, 1);
        textMeshPro.enableWordWrapping = false;
        textMeshPro.alignment = TextAlignmentOptions.Center;
        textMeshPro.sortingOrder = 1;

        textMeshPro = new GameObject().AddComponent<TextMeshPro>();
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
}


public class Button
{
    private RectTransform rectTransform;
    public Button(Canvas canvas, Vector2 position, Vector2 size, Vector2 anchor, Sprite sprite)
    {
        this.rectTransform = canvas.NewRectTransform(position, size, anchor);
        rectTransform.AddComponent<UI.Image>().sprite = sprite;
    }
    public bool Hit(Vector2 point)
    {
        if (point.x > this.rectTransform.localPosition.x + this.rectTransform.sizeDelta.x * 0.5f) return false;
        if (point.x < this.rectTransform.localPosition.x - this.rectTransform.sizeDelta.x * 0.5f) return false;
        if (point.y > this.rectTransform.localPosition.y + this.rectTransform.sizeDelta.y * 0.5f) return false;
        if (point.y < this.rectTransform.localPosition.y - this.rectTransform.sizeDelta.y * 0.5f) return false;
        return true;
    }
}


public class Camera
{
    public Camera(GameObject Parent)
    {
        UE.Camera component = new GameObject().AddComponent<UE.Camera>();
        component.backgroundColor = UE.Color.HSVToRGB(0, 0, 0.5f);
        component.clearFlags = CameraClearFlags.SolidColor;
        component.orthographic = true;
        component.transform.SetParent(Parent.transform);
    }
}

public class Canvas
{
    private UE.Canvas component;
    private Vector2 size;
    public Canvas(GameObject parent, UE.Camera camera)
    {
        this.component = new GameObject().AddComponent<UE.Canvas>();
        this.component.renderMode = UnityEngine.RenderMode.ScreenSpaceCamera;
        this.component.worldCamera = camera;
        this.component.AddComponent<UI.CanvasScaler>().referenceResolution = new Vector2(2000, 1000);
        this.component.GetComponent<UI.CanvasScaler>().uiScaleMode = UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
        this.component.transform.SetParent(parent.transform);
        {
            this.component.AddComponent<UI.GraphicRaycaster>();
            this.component.AddComponent<EventSystem>();
            this.component.AddComponent<StandaloneInputModule>();
        }
        this.size = this.component.GetComponent<RectTransform>().sizeDelta;
    }
    public RectTransform NewRectTransform(Vector2 position, Vector2 size, Vector2 anchor)
    {
        RectTransform rectTransform = new GameObject().AddComponent<RectTransform>();
        rectTransform.SetParent(this.component.transform, false);
        rectTransform.anchorMax = anchor;
        rectTransform.anchorMin = anchor;
        rectTransform.anchoredPosition = position;
        rectTransform.sizeDelta = size;
        return rectTransform;
    }

    public Vector2 ScreenPosition(Vector2 position)
    {
        return position / (Screen.width / this.size.x) - this.size * 0.5f;
    }
    public bool Hit(Vector2 point, RectTransform rectTransform)
    {
        point = this.ScreenPosition(point);
        if (point.x > rectTransform.localPosition.x + rectTransform.sizeDelta.x * 0.5f) return false;
        if (point.x < rectTransform.localPosition.x - rectTransform.sizeDelta.x * 0.5f) return false;
        if (point.y > rectTransform.localPosition.y + rectTransform.sizeDelta.y * 0.5f) return false;
        if (point.y < rectTransform.localPosition.y - rectTransform.sizeDelta.y * 0.5f) return false;
        return true;
    }
}

