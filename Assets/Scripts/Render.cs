using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine.TextCore.Text;
public class Render
{
    static readonly int EFFECT = 30;
    public static Sprite[] sprite = new Sprite[] {
        Resources.Load<Sprite>("chara-img-aruru"),
        Resources.Load<Sprite>("chara-img-witch"),
        Resources.Load<Sprite>("chara-img-schezo"),
        Resources.Load<Sprite>("chara-img-satan"),
        Resources.Load<Sprite>("chara-img-amity"),
        Resources.Load<Sprite>("chara-img-sig"),
        Resources.Load<Sprite>("chara-img-rafina"),
        Resources.Load<Sprite>("chara-img-lemres"),
        Resources.Load<Sprite>("chara-img-ringo"),
        Resources.Load<Sprite>("chara-img-maguro"),
        Resources.Load<Sprite>("chara-img-risukuma"),
        Resources.Load<Sprite>("chara-img-ecolo"),
    };
    private GameObject puyo;
    private SpriteRenderer[] nextColor = new SpriteRenderer[4];
    private TextMeshPro combo;
    private Dictionary<Puyo, Transform> dictionary = new Dictionary<Puyo, Transform>();
    public Camera camera;
    private Transform effect;
    private GameObject[] garbagePuyo = new GameObject[33];
    static private Transform[] character = new Transform[2];
    private Gauge[] gauge;
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
            this.camera.transform.position = new Vector3(6.875f, 7, -1);
        }
        {
            SpriteRenderer s = new GameObject().AddComponent<SpriteRenderer>();
            s.sprite = Resources.Load<Sprite>("Main");
            s.color = UnityEngine.Color.HSVToRGB(0, 0, 0.5f);
            s.sortingOrder = 0;
            s.transform.position = new Vector3(6.875f, 7, 0);
        }
        {
            this.nextColor[0] = Main.Instantiate(this.puyo, new Vector2(0, 4.0f), Quaternion.identity).GetComponent<SpriteRenderer>();
            this.nextColor[1] = Main.Instantiate(this.puyo, new Vector2(0, 5.0f), Quaternion.identity).GetComponent<SpriteRenderer>();
            this.nextColor[2] = Main.Instantiate(this.puyo, new Vector2(0, 1.75f), Quaternion.identity).GetComponent<SpriteRenderer>();
            this.nextColor[3] = Main.Instantiate(this.puyo, new Vector2(0, 2.75f), Quaternion.identity).GetComponent<SpriteRenderer>();
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
            this.combo.fontSize = 10;
            this.combo.fontStyle = TMPro.FontStyles.Bold;
            this.combo.color = UnityEngine.Color.HSVToRGB(1 / 6f, 1, 1);
            this.combo.transform.position = new Vector3(10f, 3.75f, 0);
            this.combo.alignment = TextAlignmentOptions.Center;
            this.combo.sortingOrder = 4;
        }
        {
            GameObject gameObject = new GameObject();
            for (int y = 0; y < 11; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    this.garbagePuyo[x + y * 3] = Main.Instantiate(this.puyo, new Vector2(x * 0.5f, y * 0.5f), Quaternion.identity);
                    this.garbagePuyo[x + y * 3].GetComponent<SpriteRenderer>().color = UnityEngine.Color.HSVToRGB(0, 0, 0.5f);
                    this.garbagePuyo[x + y * 3].transform.SetParent(gameObject.transform);
                    this.garbagePuyo[x + y * 3].transform.localScale = new Vector3(0.5f, 0.5f, 1);
                }
            }
            gameObject.transform.localPosition = new Vector3(13.25f, 1.25f, 0);
        }
        {

            this.gauge = new Gauge[2];
            this.gauge[0] = new Gauge(new Vector2(14.375f, 10.25f), new Vector2(0.25f, 5.5f), UnityEngine.Color.green);
            this.gauge[1] = new Gauge(new Vector2(13.875f, 10.25f), new Vector2(0.25f, 5.5f), UnityEngine.Color.yellow);

        }
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject gameObject = new GameObject();
                Render.character[i] = new GameObject().transform;
                Render.character[i].SetParent(gameObject.transform);
                if (i == 0) Render.character[i].gameObject.AddComponent<SpriteRenderer>().sprite = Render.sprite[Main.character[0]];
                if (i == 1) Render.character[i].gameObject.AddComponent<SpriteRenderer>().sprite = Render.sprite[Main.character[1]];
                new GameObject().transform.SetParent(gameObject.transform);
                gameObject.transform.GetChild(1).AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Square");
                if (i == 0) gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = UnityEngine.Color.HSVToRGB(2 / 3f, 0.3f, 1);
                if (i == 1) gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = UnityEngine.Color.HSVToRGB(0, 0.3f, 1);
                Render.character[i].gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                gameObject.transform.localScale = new Vector3(5.5f, 5.5f, 0);
                gameObject.transform.position = new Vector3(10f + 0.75f * i, 3.75f + 6.5f * i, 0);
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

    public void BotGauge(Bot bot)
    {
        this.gauge[0].Set(bot.health / (float)Static.BOT_HEALTH);
        if (bot.energy == null) this.gauge[1].Set(0);
        else this.gauge[1].Set(bot.energy.i / (float)bot.energy.maximum);
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
            if (combo.i > 0) new Attack(Render.character[0], 0.3f);
        }
        if (bot.combo.update)
        {
            if (bot.combo.i > 0) new Attack(Render.character[1], -0.3f);
        }
    }
    static public void Character(int i)
    {
        new Attack(Render.character[i], 0.6f * (1 - 2 * i));
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
        public Gauge(Vector2 position, Vector2 scale, UnityEngine.Color color)
        {
            GameObject gameObject = new GameObject();
            new GameObject().transform.SetParent(gameObject.transform);
            new GameObject().transform.SetParent(gameObject.transform);
            gameObject.transform.GetChild(0).AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Square");
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
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
            this.transform.localScale = new Vector2(1, f);
            this.transform.localPosition = new Vector3(0, -(1 - transform.localScale.y) * 0.5f, 0);
        }
    }
}