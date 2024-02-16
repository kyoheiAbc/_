using UnityEngine;
using TMPro;

public class Render
{
    private GameObject gameObject = Resources.Load<GameObject>("Puyo");
    private SpriteRenderer[] Next = new SpriteRenderer[4];
    private TextMeshPro combo;
    private int i;


    public Render()
    {
        combo = new GameObject("").AddComponent<TextMeshPro>();
        combo.fontSize = 16;
        combo.transform.position = new Vector3(4, 7, 0);
        combo.alignment = TextAlignmentOptions.Center;
        combo.sortingOrder = 256;

        this.Next[0] = Main.Instantiate(this.gameObject, new Vector2(9f, 11.5f), Quaternion.identity).GetComponent<SpriteRenderer>();
        this.Next[1] = Main.Instantiate(this.gameObject, new Vector2(9f, 12.5f), Quaternion.identity).GetComponent<SpriteRenderer>();
        this.Next[2] = Main.Instantiate(this.gameObject, new Vector2(9f, 8.5f), Quaternion.identity).GetComponent<SpriteRenderer>();
        this.Next[3] = Main.Instantiate(this.gameObject, new Vector2(9f, 9.5f), Quaternion.identity).GetComponent<SpriteRenderer>();
    }
    public void Puyo(Puyo p)
    {
        if (p.GetTransform() == null)
        {
            Transform t = Main.Instantiate(this.gameObject).transform;
            p.SetTransform(t);
            t.gameObject.GetComponent<SpriteRenderer>().color = UnityEngine.Color.HSVToRGB(p.GetColor() / 5f, 0.5f, 1.0f);
        }
        if (p.GetRemove())
        {
            p.GetTransform().localScale = new Vector2(1, 1.5f);
        }
        else
        {
            int i = p.GetI();
            if (i >= Main.FREEZE) i = Main.FREEZE;
            float f = 1f - i / (float)Main.FREEZE;
            p.GetTransform().position = p.GetPosition() + new Vector2(0, -0.25f * Mathf.Sin(Mathf.PI * f));
            p.GetTransform().localScale = new Vector2(1 + 0.25f * Mathf.Sin(Mathf.PI * f), 1);
        }
    }

    public void RenderCombo(Combo combo)
    {
        if (combo.GetCombo() == 0)
        {
            this.i = -1;
            this.combo.text = "";
            return;
        }

        if (0 <= this.i && this.i < 256) this.i++;


        if (this.combo.text != combo.GetCombo() + " COMBO")
        {
            if (this.i == -1) this.i = 0;
        }

        if (this.i >= Main.REMOVE)
        {
            this.i = -1;
            this.combo.text = combo.GetCombo() + " COMBO";
        }
    }

    public void RenderNextPuyoPuyo(int[] a)
    {
        this.Next[0].color = UnityEngine.Color.HSVToRGB(a[0] / 5f, 0.5f, 1.0f);
        this.Next[1].color = UnityEngine.Color.HSVToRGB(a[1] / 5f, 0.5f, 1.0f);
        this.Next[2].color = UnityEngine.Color.HSVToRGB(a[2] / 5f, 0.5f, 1.0f);
        this.Next[3].color = UnityEngine.Color.HSVToRGB(a[3] / 5f, 0.5f, 1.0f);
    }
}