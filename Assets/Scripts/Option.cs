using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
