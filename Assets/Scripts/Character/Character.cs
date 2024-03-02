using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    // private InputAndroid input;
    private Input input;

    private int[,] array = new int[2, 6];
    private Vector2[] v = new Vector2[2] { Vector2.zero, new Vector2(255, 255) };
    private CharacterRender render;

    void Start()
    {
        Application.targetFrameRate = 60;


        Camera camera = new GameObject().AddComponent<Camera>();
        camera.backgroundColor = UnityEngine.Color.HSVToRGB(1 / 6f, 0.4f, 0.8f);
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.orthographic = true;
        camera.orthographicSize = 1.5f;
        camera.transform.position = new Vector3(2.5f, -0.5f, -1f);

        GameObject gameObject = new GameObject();
        gameObject.transform.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Character");

        gameObject.transform.GetComponent<SpriteRenderer>().sortingOrder = -2;

        UnityEngine.Color color = gameObject.transform.GetComponent<SpriteRenderer>().color;
        color.a = 0.4f;
        gameObject.transform.GetComponent<SpriteRenderer>().color = color;

        gameObject.transform.localScale = new Vector2(1f, 1f);
        gameObject.transform.localPosition = new Vector3(2.5f, -0.5f, 0);

        // this.input = new InputAndroid();
        this.input = new Input();


        for (int y = 0; y < 2; y++)
        {
            for (int x = 0; x < 6; x++)
            {
                this.array[y, x] = x + 6 * y;
            }
        }
        this.render = new CharacterRender();

    }


    void Update()
    {
        Vector2 v = this.input.Update();
        if (v == Vector2.right + Vector2.down)
        {
            if (this.v[1] == new Vector2(255, 255))
            {
                this.v[1] = Vector2.zero;
                if (this.v[0] == this.v[1]) this.v[1] += Vector2.right;

            }
            else
            {
                Main.character[0] = this.array[-(int)this.v[0].y, (int)this.v[0].x];
                Main.character[1] = this.array[-(int)this.v[1].y, (int)this.v[1].x];
                SceneManager.LoadScene("Configuration");
            }
        }
        else
        {
            if (this.v[1] == new Vector2(255, 255))
            {
                this.v[0] += v;
                if (this.Error(this.v[0])) this.v[0] -= v;
            }
            else
            {
                Vector2 p = this.v[1];
                this.v[1] += v;
                if (this.v[0] == this.v[1]) this.v[1] += v;
                if (this.Error(this.v[1])) this.v[1] = p;
            }

        }


        this.render.Update(this.v);
    }


    private bool Error(Vector2 v)
    {
        if (v.x > this.array.GetLength(1) - 1) return true;
        if (v.x < 0) return true;
        if (v.y < 1 - this.array.GetLength(0)) return true;
        if (v.y > 0) return true;
        return false;
    }

    private class CharacterRender
    {
        private Transform[] transform = new Transform[2];


        public CharacterRender()
        {
            this.transform[0] = new GameObject().transform;
            this.transform[0].AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cursor");
            this.transform[0].GetComponent<SpriteRenderer>().sortingOrder = -1;
            this.transform[0].localScale = new Vector2(1f, 1f);
            this.transform[0].GetComponent<SpriteRenderer>().color = UnityEngine.Color.HSVToRGB(2 / 3f, 0.5f, 1);

            this.transform[1] = new GameObject().transform;
            this.transform[1].AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cursor");
            this.transform[1].GetComponent<SpriteRenderer>().sortingOrder = -1;
            this.transform[1].localScale = new Vector2(1f, 1f);
            this.transform[1].GetComponent<SpriteRenderer>().color = UnityEngine.Color.HSVToRGB(0, 0.5f, 1);

            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 6; x++)
                {
                    Transform transform = new GameObject().transform;
                    transform.AddComponent<SpriteRenderer>().sprite = Render.sprite[x + y * 6];
                    transform.localPosition = new Vector2(x, -y);
                    transform.localScale = new Vector2(0.9f, 0.9f);
                }
            }
        }
        public void Update(Vector2[] v)
        {
            this.transform[0].localPosition = v[0];
            this.transform[1].localPosition = v[1];
        }

    }

}
