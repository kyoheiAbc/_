using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
public class Render
{
    static readonly int EFFECT = 30;

    private GameObject puyo;
    private SpriteRenderer[] nextColor = new SpriteRenderer[4];
    private TextMeshPro combo;
    private Dictionary<Puyo, Transform> dictionary = new Dictionary<Puyo, Transform>();
    public Camera camera;
    private Transform effect;
    private GameObject[] garbagePuyo = new GameObject[32];
    private Transform[] character = new Transform[2];
    private Gauge gauge;
    public Render()
    {
        {
            this.puyo = new GameObject();
            this.puyo.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Circle");
            this.puyo.GetComponent<SpriteRenderer>().sortingOrder = 2;
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Puyo");
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
            gameObject.transform.SetParent(this.puyo.transform);
            this.puyo.transform.localPosition = new Vector3(255, 255, 255);
        }
        {
            this.camera = new GameObject().AddComponent<Camera>();
            this.camera.backgroundColor = UnityEngine.Color.HSVToRGB(0, 0, 0.35f);
            this.camera.clearFlags = CameraClearFlags.SolidColor;
            this.camera.orthographic = true;
            this.camera.orthographicSize = 7;
            this.camera.transform.position = new Vector3(7, 7, -1);
        }
        {
            SpriteRenderer s = new GameObject().AddComponent<SpriteRenderer>();
            s.color = UnityEngine.Color.HSVToRGB(0, 0, 0.5f);
            s.sprite = Resources.Load<Sprite>("board");
            s.sortingOrder = 1;
            s.transform.position = new Vector3(4, 7, 0);
        }
        {
            SpriteRenderer s = new GameObject().AddComponent<SpriteRenderer>();
            s.color = UnityEngine.Color.HSVToRGB(0, 0, 0.5f);
            s.sprite = Resources.Load<Sprite>("next");
            s.sortingOrder = 1;
            s.transform.position = new Vector3(8.5f, 10, 0);
            this.nextColor[0] = Main.Instantiate(this.puyo, new Vector2(8.5f, 11f), Quaternion.identity).GetComponent<SpriteRenderer>();
            this.nextColor[1] = Main.Instantiate(this.puyo, new Vector2(8.5f, 12f), Quaternion.identity).GetComponent<SpriteRenderer>();
            this.nextColor[2] = Main.Instantiate(this.puyo, new Vector2(8.5f, 8f), Quaternion.identity).GetComponent<SpriteRenderer>();
            this.nextColor[3] = Main.Instantiate(this.puyo, new Vector2(8.5f, 9f), Quaternion.identity).GetComponent<SpriteRenderer>();
        }
        {
            this.effect = new GameObject().transform;
            for (int i = 0; i < 4; i++)
            {
                new GameObject().transform.SetParent(this.effect);
                SpriteRenderer spriteRenderer = this.effect.GetChild(i).AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = Resources.Load<Sprite>("Circle");
                spriteRenderer.sortingOrder = 2;
            }
            this.effect.transform.localPosition = new Vector3(255, 255, 255);
        }
        {
            this.combo = new GameObject().AddComponent<TextMeshPro>();
            this.combo.fontSize = 14;
            this.combo.color = UnityEngine.Color.HSVToRGB(1 / 6f, 1, 1);
            this.combo.transform.position = new Vector3(10.25f, 3.75f, 0);
            this.combo.alignment = TextAlignmentOptions.Center;
            this.combo.sortingOrder = 4;
        }
        {
            GameObject gameObject = new GameObject();
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    this.garbagePuyo[x + y * 4] = Main.Instantiate(this.puyo, new Vector2(x + 14, 1.5f + y), Quaternion.identity);
                    this.garbagePuyo[x + y * 4].GetComponent<SpriteRenderer>().color = UnityEngine.Color.HSVToRGB(0, 0, 0.5f);
                    this.garbagePuyo[x + y * 4].transform.SetParent(gameObject.transform);
                }
            }
            gameObject.transform.localScale = new Vector3(0.6875f, 0.6875f, 1);
            gameObject.transform.localPosition = new Vector3(4, 0.325f, 0);
        }
        {
            this.gauge = new Gauge(new Vector2(13.25f, 7.375f), new Vector2(5.5f, 0.25f));
        }
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject gameObject = new GameObject();
                this.character[i] = new GameObject().transform;
                this.character[i].SetParent(gameObject.transform);
                this.character[i].gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ringo");
                this.character[i].gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                gameObject.transform.localScale = new Vector3(5.5f, 5.5f, 0);
                gameObject.transform.position = new Vector3(10.25f + 3 * i, 3.75f + 6.5f * i, 0);
            }
        }

        this.Start();
    }
    public void Start()
    {
        foreach (Transform t in this.dictionary.Values)
        {
            Main.Destroy(t.gameObject);
        }
        this.dictionary.Clear();
        this.combo.text = "";
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
                float f = 1f - (float)l.freeze.i / l.freeze.maximum;
                this.dictionary[l].position = l.position + new Vector2(0, -0.25f * Mathf.Sin(Mathf.PI * f));
                this.dictionary[l].localScale = new Vector2(1 + 0.25f * Mathf.Sin(Mathf.PI * f), 1);
            }
        }

        foreach (Puyo l in _list)
        {
            Main.Destroy(this.dictionary[l].gameObject);
            this.dictionary.Remove(l);
            new Effect(this.effect, l.position + new Vector2(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f)), l.color);
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

    public void Bot(Bot bot)
    {
        this.gauge.Set(bot.health);
    }

    public void GarbagePuyo(Offset offset)
    {
        int I = offset.temporary[1] - offset.i - offset.temporary[0];
        for (int i = 0; i < this.garbagePuyo.Length; i++)
        {
            this.garbagePuyo[i].SetActive(i < I);
        }
    }

    public void Character(Combo combo, Bot bot)
    {
        if (combo.update)
        {
            new Attack(this.character[0], 0.5f);
        }
        if (bot.combo.update)
        {
            new Attack(this.character[1], -0.5f);
        }
    }

    private class Effect : CustomGameObject
    {
        private Transform transform;
        public Effect(Transform transform, Vector2 position, int color) : base(Render.EFFECT)
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
        public override void Clear()
        {
            base.Clear();
            this.End();
        }
    }

    private class Attack : CustomGameObject
    {
        Transform transform;
        float f;
        public Attack(Transform transform, float f) : base(Render.EFFECT)
        {
            this.transform = transform;
            this.f = f;
        }
        public override void Update()
        {
            float f = (Render.EFFECT - base.i) / (float)(Render.EFFECT - 1);
            this.transform.localPosition = new Vector3(0, Mathf.Sin(Mathf.PI * f), 0) * this.f;
            base.Update();
        }
        public override void End()
        {
            this.transform.localPosition = Vector2.zero;
        }
        public override void Clear()
        {
            base.Clear();
            this.transform.localPosition = Vector2.zero;
        }
    }

    private class Gauge
    {
        Transform transform;
        public Gauge(Vector2 position, Vector2 scale)
        {
            GameObject gameObject = new GameObject();
            new GameObject().transform.SetParent(gameObject.transform);
            new GameObject().transform.SetParent(gameObject.transform);
            gameObject.transform.GetChild(0).AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Square");
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = UnityEngine.Color.green;
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 3;
            gameObject.transform.GetChild(1).AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Square");
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = UnityEngine.Color.black;
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 2;
            this.transform = gameObject.transform.GetChild(0).transform;
            gameObject.transform.localPosition = position;
            gameObject.transform.localScale = scale;
        }
        public void Set(float f)
        {
            this.transform.localScale = new Vector2(f, 1);
            this.transform.localPosition = new Vector3(-(1 - transform.localScale.x) * 0.5f, 0, 0);
        }
    }
}