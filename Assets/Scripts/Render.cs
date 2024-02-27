using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;

public class Render
{
    private GameObject puyo = Resources.Load<GameObject>("Puyo");
    private SpriteRenderer[] nextColor = new SpriteRenderer[4];
    private TextMeshPro combo;
    private Dictionary<Puyo, Transform> dictionary = new Dictionary<Puyo, Transform>();
    public Camera camera;
    private Transform transform = new GameObject().transform;
    private GameObject[] garbagePuyo = new GameObject[36];
    private Transform[] character = new Transform[2];
    private GameObject gauge;

    public Render()
    {
        GameObject gameObject = new GameObject();
        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Circle");
        spriteRenderer.sortingOrder = 1;
        for (int i = 0; i < 4; i++)
        {
            Main.Instantiate(gameObject).transform.SetParent(this.transform);
        }
        Main.Destroy(gameObject);
        this.transform.position = new Vector2(256, 256);
        this.transform.localScale = new Vector3(0.8f, 0.8f, 1);

        this.camera = new GameObject("").AddComponent<Camera>();
        this.camera.backgroundColor = UnityEngine.Color.HSVToRGB(0, 0, 0.35f);
        this.camera.clearFlags = CameraClearFlags.SolidColor;
        this.camera.orthographic = true;
        this.camera.orthographicSize = 8;
        this.camera.transform.position = new Vector3(8.5f, 7, -1);

        SpriteRenderer s = new GameObject("").AddComponent<SpriteRenderer>();
        s.color = UnityEngine.Color.HSVToRGB(0, 0, 0.5f);
        s.sprite = Resources.Load<Sprite>("board");
        s.transform.position = new Vector3(4, 7, 0);


        this.combo = new GameObject("").AddComponent<TextMeshPro>();
        this.combo.fontSize = 16;
        this.combo.transform.position = new Vector3(4, 7, 0);
        this.combo.alignment = TextAlignmentOptions.Center;
        this.combo.sortingOrder = 256;

        SpriteRenderer s_ = new GameObject("").AddComponent<SpriteRenderer>();
        s_.color = UnityEngine.Color.HSVToRGB(0, 0, 0.5f);
        s_.sprite = Resources.Load<Sprite>("next");
        s_.transform.position = new Vector3(8.5f, 10, 0);
        this.nextColor[0] = Main.Instantiate(this.puyo, new Vector2(8.5f, 11f), Quaternion.identity).GetComponent<SpriteRenderer>();
        this.nextColor[1] = Main.Instantiate(this.puyo, new Vector2(8.5f, 12f), Quaternion.identity).GetComponent<SpriteRenderer>();
        this.nextColor[2] = Main.Instantiate(this.puyo, new Vector2(8.5f, 8f), Quaternion.identity).GetComponent<SpriteRenderer>();
        this.nextColor[3] = Main.Instantiate(this.puyo, new Vector2(8.5f, 9f), Quaternion.identity).GetComponent<SpriteRenderer>();

        for (int y = 0; y < 6; y++)
        {
            for (int x = 0; x < 6; x++)
            {
                this.garbagePuyo[x + y * 6] = Main.Instantiate(this.puyo, new Vector2(x + 1.5f, 14.5f + y), Quaternion.identity);
                this.garbagePuyo[x + y * 6].GetComponent<SpriteRenderer>().color = UnityEngine.Color.HSVToRGB(0, 0, 0.5f);
            }

        }

        this.gauge = new GameObject();
        new GameObject().transform.SetParent(this.gauge.transform);
        new GameObject().transform.SetParent(this.gauge.transform);
        this.gauge.transform.GetChild(0).AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Square");
        this.gauge.transform.GetChild(0).GetComponent<SpriteRenderer>().color = UnityEngine.Color.green;
        this.gauge.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 1;
        this.gauge.transform.GetChild(1).AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Square");
        this.gauge.transform.GetChild(1).GetComponent<SpriteRenderer>().color = UnityEngine.Color.black;
        this.gauge.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 1;
        this.gauge.transform.localPosition = new Vector2(13.25f, 12.75f);
        this.gauge.transform.localScale = new Vector2(5.5f, 0.25f);

        this.character[0] = new GameObject("").transform;
        this.character[0].gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ringo");
        this.character[0].localScale = new Vector3(5.5f, 5.5f, 0);
        this.character[0].position = new Vector3(13.25f, 3.75f, 0);

        this.character[1] = new GameObject("").transform;
        this.character[1].gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("maguro");
        this.character[1].localScale = new Vector3(5.5f, 5.5f, 0);
        this.character[1].position = new Vector3(13.25f, 10.25f, 0);

        this.Start();
    }
    public void Start()
    {
        foreach (Transform t in this.dictionary.Values)
        {
            Main.Destroy(t.gameObject);
        }
        this.dictionary.Clear();

    }
    public void Puyo(List<Puyo> list)
    {
        List<Puyo> _list = this.dictionary.Keys.ToList();
        foreach (Puyo l in list)
        {
            if (l.position.x == 0.5f || l.position.x == 7.5f || l.position.y == 0.5f || l.position.y == 15.5f) continue;

            _list.Remove(l);

            if (!this.dictionary.ContainsKey(l))
            {
                this.dictionary[l] = Main.Instantiate(this.puyo).transform;
                this.dictionary[l].GetComponent<SpriteRenderer>().color = UnityEngine.Color.HSVToRGB(l.color / 5f, 0.5f, 1.0f);
                if (l.color == 10) this.dictionary[l].GetComponent<SpriteRenderer>().color = UnityEngine.Color.HSVToRGB(0, 0, 0.5f);
            }

            if (l.fire.i > 0)
            {
                this.dictionary[l].localScale = new Vector2(1, 1.5f);
            }
            else
            {
                float f = 1f - (float)l.freeze.i / l.freeze.max;
                this.dictionary[l].position = l.position + new Vector2(0, -0.25f * Mathf.Sin(Mathf.PI * f));
                this.dictionary[l].localScale = new Vector2(1 + 0.25f * Mathf.Sin(Mathf.PI * f), 1);
            }
        }

        foreach (Puyo l in _list)
        {
            Main.Destroy(this.dictionary[l].gameObject);
            this.dictionary.Remove(l);
            new Effect(this.transform, l.position + new Vector2(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f)), l.color);
        }

    }

    public void Combo(int i)
    {
        if (i == 0) this.combo.text = "";
        else this.combo.text = i + " COMBO";
    }

    public void NextColor(int[] array)
    {
        this.nextColor[0].color = UnityEngine.Color.HSVToRGB(array[0] / 5f, 0.5f, 1.0f);
        this.nextColor[1].color = UnityEngine.Color.HSVToRGB(array[1] / 5f, 0.5f, 1.0f);
        this.nextColor[2].color = UnityEngine.Color.HSVToRGB(array[2] / 5f, 0.5f, 1.0f);
        this.nextColor[3].color = UnityEngine.Color.HSVToRGB(array[3] / 5f, 0.5f, 1.0f);
    }

    public void Bot(int i)
    {
        Gauge.Set(this.gauge.transform.GetChild(0).transform, i / 16f);
    }

    public void GarbagePuyo(Offset offset)
    {
        int I = offset._temporary - offset.i - offset.temporary;
        for (int i = 0; i < this.garbagePuyo.Length; i++)
        {
            this.garbagePuyo[i].SetActive(i < I);
        }
    }

    public void Attack(Combo combo, Bot bot)
    {
        if (combo.b && combo.i > 0)
        {
            new _Attack(this.character[0], false);
        }
        if (bot.combo.b && bot.combo.i > 0)
        {
            new _Attack(this.character[1], true);
        }

    }


    private class Effect : CustomGameObject
    {
        private Transform transform;
        public Effect(Transform transform, Vector2 position, int color) : base(30)
        {
            this.transform = Main.Instantiate(transform, position, Quaternion.identity);
            for (int i = 0; i < 4; i++)
            {
                this.transform.GetChild(i).GetComponent<SpriteRenderer>().color = UnityEngine.Color.HSVToRGB(color / 5f, 0.5f, 1.0f);
                if (color == 10) this.transform.GetChild(i).GetComponent<SpriteRenderer>().color = UnityEngine.Color.HSVToRGB(0, 0, 0.5f);
            }
            this.transform.Rotate(0, 0, UnityEngine.Random.Range(0, 7) * 15);

        }
        public override void Update()
        {
            base.Update();
            this.transform.GetChild(0).localPosition += new Vector3(1, 0, 0) * 0.15f;
            this.transform.GetChild(1).localPosition += new Vector3(-1, 0, 0) * 0.15f;
            this.transform.GetChild(2).localPosition += new Vector3(0, 1, 0) * 0.15f;
            this.transform.GetChild(3).localPosition += new Vector3(0, -1, 0) * 0.15f;
        }
        public override void End()
        {
            Main.Destroy(this.transform.gameObject);
        }
    }

    private class _Attack : CustomGameObject
    {
        Transform transform;
        Vector2 position;
        bool bot;
        public _Attack(Transform transform, bool bot) : base(30)
        {
            this.transform = transform;
            this.position = this.transform.position;
            this.bot = bot;

        }
        public override void Update()
        {
            float f = (30 - base.i) / 29f;
            if (!this.bot)
                this.transform.localPosition = this.position + new Vector2(0, Mathf.Sin(Mathf.PI * f)) * 3f;
            else
                this.transform.localPosition = this.position - new Vector2(0, Mathf.Sin(Mathf.PI * f)) * 3f;
            base.Update();
        }
    }

    private class Gauge
    {
        public static void Set(Transform transform, float f)
        {
            transform.localScale = new Vector2(f, 1);
            transform.localPosition = new Vector3(-(1 - transform.localScale.x) * 0.5f, 0, 0);
        }
    }


}