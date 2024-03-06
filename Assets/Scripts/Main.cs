using System;
using Unity.VisualScripting;
using UE = UnityEngine;
using UI = UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Main
{
    Play play = new Play();
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
    Render render = new Render();
    public RenderPlay()
    {
        this.render.canvas.component.AddComponent<UI.GraphicRaycaster>();
        this.render.canvas.component.AddComponent<EventSystem>();
        this.render.canvas.component.AddComponent<StandaloneInputModule>();

        new Slider(this.render, new Vector2(0, 0), new Vector2(300, 50), new Vector2(0.5f, 0.5f), 25, 75);

    }
    public void Update()
    {
        this.render.Update();

    }
}

public class Slider
{
    UI.Slider component;
    public Slider(Render render, Vector2 position, Vector2 size, Vector2 anchor, int min, int max)
    {
        RectTransform rectTransform = render.NewRectTransform(position, size, anchor);
        rectTransform.AddComponent<UI.Image>().sprite = Resources.Load<Sprite>("Square");
        this.component = rectTransform.AddComponent<UI.Slider>();

        RectTransform rT = render.NewRectTransform(Vector2.zero, Vector2.zero, Vector2.zero);
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



public class Render
{
    public GameObject gameObject;
    public Camera camera;
    public Canvas canvas;
    public Button back, enter;
    public Render()
    {
        this.gameObject = new GameObject();
        this.camera = new Camera(this);
        this.canvas = new Canvas(this);

        this.back = new Button(this, new Vector2(50, -50), new Vector2(100, 100), new Vector2(0f, 1f), Resources.Load<Sprite>("Square"));
        this.enter = new Button(this, new Vector2(-50, -50), new Vector2(100, 100), new Vector2(1f, 1f), Resources.Load<Sprite>("Square"));
    }


    public RectTransform NewRectTransform(Vector2 position, Vector2 size, Vector2 anchor)
    {
        RectTransform rectTransform = new GameObject().AddComponent<RectTransform>();
        rectTransform.SetParent(this.canvas.component.transform, false);
        rectTransform.anchorMax = anchor;
        rectTransform.anchorMin = anchor;
        rectTransform.anchoredPosition = position;
        rectTransform.sizeDelta = size;
        return rectTransform;
    }
    public void Update()
    {
        if (this.back.Hit(UE.Input.mousePosition, this.canvas.size)) Debug.Log("a");
    }
}
public class Button
{
    RectTransform rectTransform;
    public Button(Render render, Vector2 position, Vector2 size, Vector2 anchor, Sprite sprite)
    {
        this.rectTransform = render.NewRectTransform(position, size, anchor);
        rectTransform.AddComponent<UI.Image>().sprite = sprite;
    }
    public bool Hit(Vector2 point, Vector2 cS)
    {
        float f = Screen.width / cS.x;
        point = point / f - cS * 0.5f;
        if (point.x > rectTransform.localPosition.x + rectTransform.sizeDelta.x * 0.5f) return false;
        if (point.x < rectTransform.localPosition.x - rectTransform.sizeDelta.x * 0.5f) return false;
        if (point.y > rectTransform.localPosition.y + rectTransform.sizeDelta.y * 0.5f) return false;
        if (point.y < rectTransform.localPosition.y - rectTransform.sizeDelta.y * 0.5f) return false;
        return true;
    }

}


public class Camera
{
    public UE.Camera component;
    public Camera(Render render)
    {
        this.component = new GameObject().AddComponent<UE.Camera>();
        this.component.backgroundColor = UE.Color.HSVToRGB(0, 0, 0.5f);
        this.component.clearFlags = CameraClearFlags.SolidColor;
        this.component.orthographic = true;
        this.component.transform.SetParent(render.gameObject.transform);
    }
}

public class Canvas
{
    public UE.Canvas component;
    public Vector2 size;
    public Canvas(Render render)
    {
        this.component = new GameObject().AddComponent<UE.Canvas>();
        this.component.renderMode = UnityEngine.RenderMode.ScreenSpaceCamera;
        this.component.worldCamera = render.camera.component;
        this.component.AddComponent<UI.CanvasScaler>().referenceResolution = new Vector2(2000, 1000);
        this.component.GetComponent<UI.CanvasScaler>().uiScaleMode = UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
        this.component.transform.SetParent(render.gameObject.transform);

        this.size = this.component.GetComponent<RectTransform>().sizeDelta;
    }
}

