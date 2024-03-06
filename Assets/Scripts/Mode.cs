using TMPro;
using UnityEngine;
public class SceneMode : Scene
{
    public SceneMode()
    {
        this.render = new RenderMode();
    }

}

public class RenderMode : Render
{
    RectTransform[] rectTransform = new RectTransform[6];
    RectTransform cursor;
    public int i = 0;

    public RenderMode()
    {
        for (int i = 0; i < rectTransform.Length; i++)
        {
            this.rectTransform[i] = this.NewSprite(new Vector2(0, i * -150 + 375), new Vector2(500, 100), new Vector2(0.5f, 0.5f), Resources.Load<Sprite>("Square"), UnityEngine.Color.HSVToRGB(0, 0, 1));
            TextMeshPro textMeshPro;
            textMeshPro = this.NewGameObject().AddComponent<TextMeshPro>();
            textMeshPro.transform.SetParent(this.rectTransform[i], false);
            textMeshPro.GetComponent<RectTransform>().localScale = new Vector3(10, 10, 1);
            textMeshPro.enableWordWrapping = false;
            textMeshPro.alignment = TextAlignmentOptions.Center;
            textMeshPro.sortingOrder = 1;
            textMeshPro.text = "MODE " + i.ToString();
            if (i == 2) textMeshPro.text = "FREE BATTLE";
            if (i == 5) textMeshPro.text = "OPTION"; ;

            textMeshPro.color = UnityEngine.Color.black;
        }


        this.Destroy(this.back.gameObject);

        this.cursor = this.NewSprite(new Vector2(0, 375), new Vector2(500, 100), new Vector2(0, 0), Resources.Load<Sprite>("Cursor"), UnityEngine.Color.HSVToRGB(UnityEngine.Random.Range(0, 1f), 0.5f, 1));
        Render.Put((Vector2)this.rectTransform[0].localPosition + this.canvas.sizeDelta * 0.5f, this.cursor, this.canvas.sizeDelta);

    }
    override public void Update()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < rectTransform.Length; i++)
            {
                if (Render.Contact(UnityEngine.Input.mousePosition, this.rectTransform[i], this.canvas.sizeDelta))
                {
                    this.i = i;
                    Render.Put((Vector2)this.rectTransform[i].localPosition + this.canvas.sizeDelta * 0.5f, this.cursor, this.canvas.sizeDelta);
                    return;
                }
            }
            if (Render.Contact(UnityEngine.Input.mousePosition, this.enter, this.canvas.sizeDelta))
            {
                switch (this.i)
                {
                    case 2:
                        Main.NewScene(typeof(SceneCharacter));
                        break;
                    case 5:
                        Main.NewScene(typeof(SceneOption));
                        break;
                }
                return;
            }

        }
    }


}
