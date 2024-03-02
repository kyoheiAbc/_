using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfigurationMonoBehaviour : MonoBehaviour
{
    public static List<UI> list = new List<UI>();
    public static new Camera camera;
    public static Canvas canvas;

    void Start()
    {

        new Static();

        Application.targetFrameRate = 60;

        camera = new GameObject().AddComponent<Camera>();
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = UnityEngine.Color.HSVToRGB(0, 0, 0.25f);
        camera.orthographic = true;
        camera.orthographicSize = 4;
        camera.transform.position = new Vector3(0, 0, -1);


        canvas = new GameObject().AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = camera;
        canvas.transform.AddComponent<GraphicRaycaster>();
        canvas.transform.AddComponent<EventSystem>();
        canvas.transform.AddComponent<StandaloneInputModule>();

        canvas.transform.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvas.transform.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);


        GameObject gameObject = new GameObject();
        gameObject.transform.SetParent(canvas.transform, false);
        RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.anchorMin = new Vector2(1, 1);
        rectTransform.anchoredPosition = new Vector2(-64, -64);
        rectTransform.sizeDelta = new Vector2(64, 64);
        gameObject.AddComponent<Image>().sprite = Resources.Load<Sprite>("x");



        Vector2 v = new Vector2(512, 96);
        list.Add(new CustomSlider("ACTIVE", Static.ACTIVE, new Vector2(v.x * -1, v.y * 3), new Vector2(320, 32), 0, 1));
        list.Add(new CustomSlider("THRESHOLD", Static.THRESHOLD, new Vector2(v.x * -1, v.y * 1), new Vector2(320, 32), 1, 25));
        list.Add(new CustomSlider("COLOR", Static.COLOR, new Vector2(v.x * -1, v.y * -1), new Vector2(320, 32), 2, 5));
        list.Add(new CustomSlider("FIRE", Static.FIRE, new Vector2(v.x * -1, v.y * -3), new Vector2(320, 32), 2, 8));

        list.Add(new CustomSlider("DOWN", Static.DOWN, new Vector2(v.x * 0, v.y * 3), new Vector2(320, 32), 1, 30));
        list.Add(new CustomSlider("COMBO", Static.COMBO, new Vector2(v.x * 0, v.y * 1), new Vector2(320, 32), 30, 300));
        list.Add(new CustomSlider("BOT_HEALTH", Static.BOT_HEALTH, new Vector2(v.x * 0, v.y * -1), new Vector2(320, 32), 1, 512));
        list.Add(new CustomSlider("BOT_SPEED", Static.BOT_SPEED, new Vector2(v.x * 0, v.y * -3), new Vector2(320, 32), 30, 540));

        list.Add(new CustomSlider("BOT_ATTACK", Static.BOT_ATTACK, new Vector2(v.x * 1, v.y * 3), new Vector2(320, 32), 0, 1000));
        list.Add(new CustomSlider("BOT_COMBO_3", Static.BOT_COMBO_3, new Vector2(v.x * 1, v.y * 1), new Vector2(320, 32), 0, 10));
        list.Add(new CustomSlider("BOT_COMBO_5", Static.BOT_COMBO_5, new Vector2(v.x * 1, v.y * -1), new Vector2(320, 32), 0, 10));
        list.Add(new CustomSlider("BOT_COMBO_7", Static.BOT_COMBO_7, new Vector2(v.x * 1, v.y * -3), new Vector2(320, 32), 0, 10));


        return;
    }

    void Update()
    {
        foreach (UI l in ConfigurationMonoBehaviour.list)
        {
            l.Update();
        }
        Static.ACTIVE = (int)ConfigurationMonoBehaviour.list[0].slider.value;
        Static.THRESHOLD = (int)ConfigurationMonoBehaviour.list[1].slider.value;
        Static.COLOR = (int)ConfigurationMonoBehaviour.list[2].slider.value;
        Static.FIRE = (int)ConfigurationMonoBehaviour.list[3].slider.value;

        Static.DOWN = (int)ConfigurationMonoBehaviour.list[4].slider.value;
        Static.COMBO = (int)ConfigurationMonoBehaviour.list[5].slider.value;
        Static.BOT_HEALTH = (int)ConfigurationMonoBehaviour.list[6].slider.value;
        Static.BOT_SPEED = (int)ConfigurationMonoBehaviour.list[7].slider.value;

        Static.BOT_ATTACK = (int)ConfigurationMonoBehaviour.list[8].slider.value;
        Static.BOT_COMBO_3 = (int)ConfigurationMonoBehaviour.list[9].slider.value;
        Static.BOT_COMBO_5 = (int)ConfigurationMonoBehaviour.list[10].slider.value;
        Static.BOT_COMBO_7 = (int)ConfigurationMonoBehaviour.list[11].slider.value;



        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            Vector2 position = UnityEngine.Input.mousePosition;
            if (position.x > Screen.width - 96)
            {
                if (position.y > Screen.height - 96)
                {
                    Static.Json.Save();
                    SceneManager.LoadScene("Character");
                }
            }
        }

    }
    public class UI
    {
        public Slider slider;

        public UI()
        {
        }

        virtual public void Update()
        {
        }
    }

    public class CustomSlider : UI
    {
        private TextMeshPro textMeshPro;
        public CustomSlider(String str, int i, Vector2 position, Vector2 size, int min, int max)
        {
            GameObject gameObject = new GameObject();
            RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
            gameObject.AddComponent<Image>().sprite = Resources.Load<Sprite>("Square");
            gameObject.transform.GetComponent<Image>().color = UnityEngine.Color.HSVToRGB(0, 0, 0.5f);


            this.slider = gameObject.AddComponent<Slider>();

            rectTransform.localPosition = position;
            rectTransform.sizeDelta = size;
            rectTransform.localScale = new Vector3(1, 1, 1);


            new GameObject().AddComponent<RectTransform>().SetParent(gameObject.transform, false);

            gameObject.transform.GetChild(0).AddComponent<Image>().sprite = Resources.Load<Sprite>("Square");
            gameObject.transform.GetChild(0).GetComponent<Image>().color = UnityEngine.Color.HSVToRGB(0, 0, 0.75f);

            gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            gameObject.transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            slider.fillRect = gameObject.transform.GetChild(0).GetComponent<RectTransform>();
            slider.minValue = min;
            slider.maxValue = max;
            slider.wholeNumbers = true;

            this.slider.value = i;

            new GameObject().AddComponent<TextMeshPro>().transform.SetParent(gameObject.transform, false);
            // gameObject.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            gameObject.transform.GetChild(1).GetComponent<RectTransform>().localScale = new Vector3(8, 8, 1);
            gameObject.transform.GetChild(1).GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
            gameObject.transform.GetChild(1).GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
            gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().sortingOrder = 256;
            gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().enableWordWrapping = false;
            gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;
            gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = slider.value.ToString();
            textMeshPro = gameObject.transform.GetChild(1).GetComponent<TextMeshPro>();
            gameObject.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector3(48, 0, 1);

            gameObject.transform.SetParent(canvas.transform, false);

            new GameObject().AddComponent<TextMeshPro>().transform.SetParent(gameObject.transform, false);
            gameObject.transform.GetChild(2).GetComponent<RectTransform>().localScale = new Vector3(8, 8, 1);
            gameObject.transform.GetChild(2).GetComponent<TextMeshPro>().sortingOrder = 256;
            gameObject.transform.GetChild(2).GetComponent<TextMeshPro>().enableWordWrapping = false;
            gameObject.transform.GetChild(2).GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;
            gameObject.transform.GetChild(2).GetComponent<TextMeshPro>().text = str;
            gameObject.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 32, 1);
            gameObject.transform.GetChild(2).GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
            gameObject.transform.GetChild(2).GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);

        }
        override public void Update()
        {
            textMeshPro.text = this.slider.value.ToString();
        }



    }
}
