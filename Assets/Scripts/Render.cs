using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Render
{
    private GameObject gameObject = Resources.Load<GameObject>("Puyo");
    private SpriteRenderer[] nextColor = new SpriteRenderer[4];
    private TextMeshPro combo;
    private Count comboI = new Count();
    private Dictionary<Puyo, Transform> dictionary = new Dictionary<Puyo, Transform>();
    public Camera camera;

    public Render()
    {
        camera = new GameObject("").AddComponent<Camera>();
        camera.backgroundColor = UnityEngine.Color.HSVToRGB(0, 0, 0.5f);
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.orthographic = true;
        camera.orthographicSize = 12;
        camera.transform.position = new Vector3(4, 7, -1);

        SpriteRenderer s = new GameObject("").AddComponent<SpriteRenderer>();
        s.color = UnityEngine.Color.HSVToRGB(2 / 3f, 1f, 1f);
        s.sprite = Resources.Load<Sprite>("Square");
        s.transform.localScale = new Vector3(6, 12, 0);
        s.transform.position = new Vector3(4, 7, 0);


        combo = new GameObject("").AddComponent<TextMeshPro>();
        combo.fontSize = 16;
        combo.transform.position = new Vector3(4, 7, 0);
        combo.alignment = TextAlignmentOptions.Center;
        combo.sortingOrder = 256;

        this.nextColor[0] = Main.Instantiate(this.gameObject, new Vector2(9f, 11.5f), Quaternion.identity).GetComponent<SpriteRenderer>();
        this.nextColor[1] = Main.Instantiate(this.gameObject, new Vector2(9f, 12.5f), Quaternion.identity).GetComponent<SpriteRenderer>();
        this.nextColor[2] = Main.Instantiate(this.gameObject, new Vector2(9f, 8.5f), Quaternion.identity).GetComponent<SpriteRenderer>();
        this.nextColor[3] = Main.Instantiate(this.gameObject, new Vector2(9f, 9.5f), Quaternion.identity).GetComponent<SpriteRenderer>();
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
        foreach (Puyo p in list)
        {

            if (!this.dictionary.ContainsKey(p))
            {
                this.dictionary[p] = Main.Instantiate(this.gameObject).transform;
                this.dictionary[p].GetComponent<SpriteRenderer>().color = UnityEngine.Color.HSVToRGB(p.GetColor() / 5f, 0.5f, 1.0f);
            }

            if (p.fire.i != 0)
            {
                // if (p.fire.i >= Main.FIRE)
                // {
                //     Main.Destroy(this.dictionary[p].gameObject);
                //     this.dictionary.Remove(p);

                // }
                // else
                // {
                //     this.dictionary[p].localScale = new Vector2(1, 1.5f);
                // }
            }
            else
            {
                int i = p.freeze.i;
                // if (i >= Main.FREEZE) i = Main.FREEZE;
                // float f = 1f - i / (float)Main.FREEZE;
                // this.dictionary[p].position = p.GetPosition() + new Vector2(0, -0.25f * Mathf.Sin(Mathf.PI * f));
                // this.dictionary[p].localScale = new Vector2(1 + 0.25f * Mathf.Sin(Mathf.PI * f), 1);
                this.dictionary[p].position = p.GetPosition();

            }
        }
    }

    public void RenderCombo(Combo combo)
    {
        this.comboI.Update();

        if (combo.GetCombo() == 0)
        {
            this.comboI.i = 0;
            this.combo.text = "";
            return;
        }

        if (this.combo.text != combo.GetCombo() + " COMBO")
        {
            this.comboI.Start();
        }

        // if (this.comboI.i >= Main.FIRE)
        {
            this.comboI.i = 0;
            this.combo.text = combo.GetCombo() + " COMBO";
        }
    }

    public void NextColor(int[] array)
    {
        this.nextColor[0].color = UnityEngine.Color.HSVToRGB(array[0] / 5f, 0.5f, 1.0f);
        this.nextColor[1].color = UnityEngine.Color.HSVToRGB(array[1] / 5f, 0.5f, 1.0f);
        this.nextColor[2].color = UnityEngine.Color.HSVToRGB(array[2] / 5f, 0.5f, 1.0f);
        this.nextColor[3].color = UnityEngine.Color.HSVToRGB(array[3] / 5f, 0.5f, 1.0f);
    }
}