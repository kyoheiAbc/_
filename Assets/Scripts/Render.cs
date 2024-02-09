using System.Collections.Generic;
using UnityEngine;

public class Render
{
    private Dictionary<Puyo, Transform> dictionary = new Dictionary<Puyo, Transform>();
    private GameObject gameObject = Resources.Load<GameObject>("Puyo");
    public void Puyo(Puyo p)
    {
        if (!this.dictionary.ContainsKey(p))
        {
            this.dictionary[p] = Main.Instantiate(this.gameObject).transform;
            this.dictionary[p].GetComponent<SpriteRenderer>().color = UnityEngine.Color.HSVToRGB(p.GetColor() / 5f, 0.5f, 1.0f);
        }
        Transform t = this.dictionary[p];
        t.position = p.GetPosition();

        // float f = 1f - puyo.GetI() / 10f;
        // t.position = puyo.GetPosition() + new Vector2(0, -0.25f * Mathf.Sin(Mathf.PI * f));
        // t.localScale = new Vector2(1 + 0.25f * Mathf.Sin(Mathf.PI * f), 1);
    }
}
