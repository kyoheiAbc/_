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
            if (p.GetJ() == 30)
            {
                Main.Destroy(p.GetTransform().gameObject);
                return;
            }
        }
        else
        {
            int i = p.GetI();
            if (i >= 10) i = 10;
            float f = 1f - i / 10f;
            p.GetTransform().position = p.GetPosition() + new Vector2(0, -0.25f * Mathf.Sin(Mathf.PI * f));
            p.GetTransform().localScale = new Vector2(1 + 0.25f * Mathf.Sin(Mathf.PI * f), 1);
        }
    }
}