using UnityEngine;
public class Render
{
    private GameObject gameObject = Resources.Load<GameObject>("Puyo");
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
}