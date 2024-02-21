using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class Render
{
    private GameObject puyo = Resources.Load<GameObject>("Puyo");
    private SpriteRenderer[] nextColor = new SpriteRenderer[4];
    private TextMeshPro combo;
    private Dictionary<Puyo, Transform> dictionary = new Dictionary<Puyo, Transform>();
    public Camera camera;

    public Render()
    {
        this.camera = new GameObject("").AddComponent<Camera>();
        this.camera.backgroundColor = UnityEngine.Color.HSVToRGB(0, 0, 0.5f);
        this.camera.clearFlags = CameraClearFlags.SolidColor;
        this.camera.orthographic = true;
        this.camera.orthographicSize = 12;
        this.camera.transform.position = new Vector3(4, 7, -1);

        SpriteRenderer s = new GameObject("").AddComponent<SpriteRenderer>();
        s.color = UnityEngine.Color.HSVToRGB(2 / 3f, 1f, 1f);
        s.sprite = Resources.Load<Sprite>("Square");
        s.transform.localScale = new Vector3(6, 12, 0);
        s.transform.position = new Vector3(4, 7, 0);


        this.combo = new GameObject("").AddComponent<TextMeshPro>();
        this.combo.fontSize = 16;
        this.combo.transform.position = new Vector3(4, 7, 0);
        this.combo.alignment = TextAlignmentOptions.Center;
        this.combo.sortingOrder = 256;

        this.nextColor[0] = Main.Instantiate(this.puyo, new Vector2(9f, 11.5f), Quaternion.identity).GetComponent<SpriteRenderer>();
        this.nextColor[1] = Main.Instantiate(this.puyo, new Vector2(9f, 12.5f), Quaternion.identity).GetComponent<SpriteRenderer>();
        this.nextColor[2] = Main.Instantiate(this.puyo, new Vector2(9f, 8.5f), Quaternion.identity).GetComponent<SpriteRenderer>();
        this.nextColor[3] = Main.Instantiate(this.puyo, new Vector2(9f, 9.5f), Quaternion.identity).GetComponent<SpriteRenderer>();
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
            _list.Remove(l);

            if (!this.dictionary.ContainsKey(l))
            {
                this.dictionary[l] = Main.Instantiate(this.puyo).transform;
                this.dictionary[l].GetComponent<SpriteRenderer>().color = UnityEngine.Color.HSVToRGB(l.color / 5f, 0.5f, 1.0f);
            }

            if (l.fire.i > 0)
            {
                this.dictionary[l].localScale = new Vector2(1, 1.5f);
            }
            else
            {
                float f = 1f - (float)l.freeze.i / l.freeze.I;
                this.dictionary[l].position = l.position + new Vector2(0, -0.25f * Mathf.Sin(Mathf.PI * f));
                this.dictionary[l].localScale = new Vector2(1 + 0.25f * Mathf.Sin(Mathf.PI * f), 1);
            }
        }

        foreach (Puyo l in _list)
        {
            Main.Destroy(this.dictionary[l].gameObject);
            this.dictionary.Remove(l);
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
}