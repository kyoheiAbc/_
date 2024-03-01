using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigurationMonoBehaviour : MonoBehaviour
{
    List<Slider> list = new List<Slider>();
    private new Camera camera;
    static int i = 0;
    void Start()
    {
        Application.targetFrameRate = 60;
        ConfigurationMonoBehaviour.i = 0;

        this.camera = new GameObject().AddComponent<Camera>();
        this.camera.clearFlags = CameraClearFlags.SolidColor;
        this.camera.orthographic = true;
        this.camera.orthographicSize = 8f;
        this.camera.transform.position = new Vector3(0, -3.5f, -1f);

        this.list.Add(new Slider(new Vector2(0, 0), new Vector2(8, 0.1f), 1 / 1000000f, this.camera));
        this.list.Add(new Slider(new Vector2(0, -1), new Vector2(8, 0.1f), 1 / 3f, this.camera));
        this.list.Add(new Slider(new Vector2(0, -2), new Vector2(8, 0.1f), 1 / 6f, this.camera));
        this.list.Add(new Slider(new Vector2(0, -3), new Vector2(8, 0.1f), 1 / 1000000f, this.camera));
        this.list.Add(new Slider(new Vector2(0, -4), new Vector2(8, 0.1f), 1 / 1000000f, this.camera));
        this.list.Add(new Slider(new Vector2(0, -5), new Vector2(8, 0.1f), 1 / 1000000f, this.camera));
        this.list.Add(new Slider(new Vector2(0, -6), new Vector2(8, 0.1f), 1 / 1000000f, this.camera));
        this.list.Add(new Slider(new Vector2(0, -7), new Vector2(8, 0.1f), 1 / 1000000f, this.camera));


        string[] s = new string[] { "THRESHOLD", "COLOR", "FIRE", "DOWN", "COMBO", "BOT_HEALTH", "BOT_ATTACK", "BOT_SPEED" };
        for (int i = 0; i < 8; i++)
        {
            Transform transform = new GameObject().transform;
            transform.localPosition = new Vector2(-8, -i);
            TextMeshPro text = transform.AddComponent<TextMeshPro>();
            text.fontSize = 6;
            text.alignment = TextAlignmentOptions.Center;
            text.text = s[i];
        }

    }
    void Update()
    {

        foreach (Slider l in this.list)
        {
            l.Update();
        }
        this.list[0].text.text = this.list[0].Get(1, 30).ToString();
        this.list[1].text.text = this.list[1].Get(2, 5).ToString();
        this.list[2].text.text = this.list[2].Get(2, 8).ToString();
        this.list[3].text.text = this.list[3].Get(1, 30).ToString();
        this.list[4].text.text = this.list[4].Get(30, 300).ToString();
        this.list[5].text.text = this.list[5].Get(16, 512).ToString();
        this.list[6].text.text = this.list[6].Get(0, 1000).ToString();
        this.list[7].text.text = this.list[7].Get(30, 60 * 15).ToString();

        Static.THRESHOLD = this.list[0].Get(1, 30);
        Static.COLOR = this.list[1].Get(2, 5);
        Static.FIRE = this.list[2].Get(2, 8);
        Static.DOWN = this.list[3].Get(1, 30);
        Static.COMBO = this.list[4].Get(30, 300);
        Static.BOT_HEALTH = this.list[5].Get(16, 512);
        Static.BOT_ATTACK = this.list[6].Get(0, 1000);
        Static.BOT_SPEED = this.list[7].Get(30, 60 * 15);

        ConfigurationMonoBehaviour.i++;
        if (ConfigurationMonoBehaviour.i > 300) SceneManager.LoadScene("Main");
        if (UnityEngine.Input.GetMouseButton(0))
        {
            ConfigurationMonoBehaviour.i = 0;
        }




    }

    private class Slider
    {
        float f;
        private bool b;
        Transform[] transform = new Transform[2];
        Camera camera;
        public TextMeshPro text;

        public Slider(Vector2 position, Vector2 scale, float f, Camera camera)
        {
            GameObject gameObject = new GameObject();

            this.transform[0] = new GameObject().transform;
            this.transform[0].SetParent(gameObject.transform);
            this.transform[0].AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Square");
            this.transform[0].localScale = scale;

            this.transform[1] = new GameObject().transform;
            this.transform[1].SetParent(gameObject.transform);
            this.transform[1].AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Circle");
            this.transform[1].transform.localScale = new Vector2(scale.y * 4, scale.y * 4);

            gameObject.transform.localPosition = position;

            Transform transform = new GameObject().transform;
            transform.SetParent(this.transform[0]);
            transform.localPosition = Vector2.right * 0.75f;
            this.text = transform.AddComponent<TextMeshPro>();
            this.text.fontSize = 6;
            // this.text.color = UnityEngine.Color.HSVToRGB(1 / 6f, 1, 1);
            this.text.alignment = TextAlignmentOptions.Center;

            this.f = f;

            this.camera = camera;
            this.Set(0);
        }
        public void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                if (Vector2.Distance(this.transform[1].position, this.camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition)) <= this.transform[1].localScale.x * 0.5f)
                {
                    this.b = true;
                }
            }
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                this.b = false;
            }

            if (!this.b) return;

            ConfigurationMonoBehaviour.i = 0;
            this.transform[1].position = new Vector2(this.camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition).x, this.transform[1].position.y);

            if (this.transform[1].position.x < this.transform[0].position.x - this.transform[0].localScale.x * 0.5f)
            {
                this.transform[1].position = new Vector2(this.transform[0].position.x - this.transform[0].localScale.x * 0.5f, this.transform[1].position.y);

            }
            if (this.transform[1].position.x > this.transform[0].position.x + this.transform[0].localScale.x * 0.5f)
            {
                this.transform[1].position = new Vector2(this.transform[0].position.x + this.transform[0].localScale.x * 0.5f, this.transform[1].position.y);
            }

            float f = this.transform[1].position.x - (this.transform[0].position.x - this.transform[0].localScale.x * 0.5f);
            f /= this.transform[0].localScale.x;
            f = (int)((f + this.f * 0.5f) / this.f) * this.f;

            this.transform[1].position = new Vector2(this.transform[0].position.x - this.transform[0].localScale.x * 0.5f + this.transform[0].localScale.x * f, this.transform[1].position.y);

        }
        public void Set(float f)
        {
            this.transform[1].position = new Vector2(this.transform[0].position.x - this.transform[0].localScale.x * 0.5f + this.transform[0].localScale.x * f, this.transform[1].position.y);
        }
        public int Get(int min, int max)
        {
            float f = this.transform[1].position.x - (this.transform[0].position.x - this.transform[0].localScale.x * 0.5f);
            f /= this.transform[0].localScale.x;

            f = f * (max - min) + min;
            return (int)f;
        }
    }
}
