
using UnityEngine;
public class Renderer
{
    Ninja ninja;
    GameObject gameObject; public GameObject getGameObject() { return this.gameObject; }
    Transform[] transform;
    Quaternion[] attack;
    Quaternion walk;
    public Renderer(Ninja n)
    {
        this.ninja = n;
        this.transform = new Transform[7];
        this.gameObject = Renderer.newGameObject(this.ninja.getI());
        this.transform[0] = this.gameObject.transform;

        for (int i = 0; i < 6; i++)
        {
            this.transform[i + 1] = this.transform[0].GetChild(0).GetChild(i).gameObject.transform;
        }

        this.transform[0].position = this.ninja.getPos();
        this.transform[0].localRotation = this.ninja.getRot();

        this.walk = Quaternion.Euler(30, 0, 0);

        this.attack = new Quaternion[2] { Quaternion.Euler(90, 0, 90), Quaternion.Euler(180, 0, 0) };


    }


    public void update()
    {
        float frame = (Main.instance.getFrame() % 30) / 30f;
        Vector3 old = this.transform[0].position;

        this.gameObject.SetActive(true);
        for (int i = 0; i < 7; i++)
        {
            this.transform[i].localRotation = Quaternion.identity;
        }

        this.transform[0].position = this.ninja.getPos();
        this.transform[0].localRotation = this.ninja.getRot();

        walk();
        void walk()
        {
            if (this.ninja.getPos() == old) return;
            float sin = Mathf.Sin(2f * Mathf.PI * (frame + this.ninja.getRandom())) * 0.5f + 0.5f;
            this.transform[3].localRotation = Quaternion.Lerp(this.walk, Quaternion.Inverse(this.walk), sin);
            this.transform[4].localRotation = Quaternion.Lerp(Quaternion.Inverse(this.walk), this.walk, sin);
            this.transform[5].localRotation = Quaternion.Lerp(Quaternion.Inverse(this.walk), this.walk, sin);
            this.transform[6].localRotation = Quaternion.Lerp(this.walk, Quaternion.Inverse(this.walk), sin);
        }

        attack();
        void attack()
        {
            int i = this.ninja.attack.getI();
            if (i == 0) return;
            float f = (30 - i % 100) / 4f;
            f = Mathf.Clamp(f, 0, 1);
            float cos = 0.5f - Mathf.Cos(Mathf.PI * f) * 0.5f;
            int c = i / 100;
            this.transform[5].localRotation = this.attack[1 - c % 2] * Quaternion.AngleAxis(-180 * cos, new Vector3(1 - c % 2, 0, c % 2));
        }

        if (this.ninja.getStun() > 0 || this.ninja.getHp() < 0)
        {
            this.gameObject.SetActive(Mathf.Sin(16 * Mathf.PI * (frame + this.ninja.getRandom())) > 0);
        }
    }

    static private GameObject newGameObject(int i)
    {
        GameObject ret = new GameObject();

        GameObject g = (GameObject)Main.Instantiate(Resources.Load("human"), Vector3.zero, Quaternion.identity);
        g.transform.SetParent(ret.transform, false);
        g.transform.localRotation = Quaternion.Euler(0, 180, 0);

        Renderer.setTexture(g.transform, (Texture)Resources.Load("ninja"), i);

        GameObject f = new GameObject();
        f.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("face");
        f.transform.SetParent(g.transform.GetChild(1).transform, false);
        f.transform.localPosition = new Vector3(0, 0, -0.251f);
        f.transform.localScale = new Vector3(6.2f, 6.2f, 0);

        return ret;
    }

    static private void setTexture(Transform transform, Texture texture, int i)
    {
        foreach (Transform t in transform)
        {
            if (t.childCount > 0) Renderer.setTexture(t, texture, i);
            MeshRenderer r = t.GetComponent<MeshRenderer>();
            if (r == null) continue;
            r.material.SetTexture("_MainTex", texture);
            switch (i)
            {
                case 0: r.material.color = Color.HSVToRGB(0, 0.8f, 0.9f); break;
                case 1: r.material.color = Color.HSVToRGB(2 / 3f, 0.8f, 0.9f); break;
                case 2: r.material.color = Color.HSVToRGB(0, 0f, 0.15f); break;
                case 3: r.material.color = Color.HSVToRGB(0, 0f, 0.95f); break;
            }
        }
    }
}
