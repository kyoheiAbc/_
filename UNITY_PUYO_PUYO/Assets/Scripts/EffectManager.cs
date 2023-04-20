using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager
{
    List<EffectExplosion> eElist;

    public EffectManager()
    {
        init();
    }

    public void init()
    {
        eElist = new List<EffectExplosion>();
    }

    public void add(GameObject origin, GameObject gO)
    {
        eElist.Add(new EffectExplosion(origin, gO));
    }

    public void render()
    {
        for (int i = 0; i < eElist.Count; i++)
        {
            if (!eElist[i].update())
            {
                eElist[i].rm();
                eElist.Remove(eElist[i]);
                i--;
            }
        }
    }
}

public class EffectExplosion
{
    GameObject gameObject;
    Transform[] children;
    int cnt;

    public EffectExplosion(GameObject origin, GameObject gO)
    {
        gameObject = gO;
        gameObject.transform.position = (Vector2)origin.transform.position - new Vector2(0.5f, 0.5f)
                            + new Vector2(0.5f, 0) * UnityEngine.Random.Range(0, 3)
                            + new Vector2(0, 0.5f) * UnityEngine.Random.Range(0, 3);

        gameObject.transform.Rotate(0, 0, UnityEngine.Random.Range(0, 7) * 15);

        Color color = origin.GetComponent<SpriteRenderer>().color;

        children = new Transform[4];
        for (int i = 0; i < 4; i++)
        {
            children[i] = gameObject.transform.GetChild(i).gameObject.transform;
            children[i].GetComponent<SpriteRenderer>().color = color;

            children[i].localPosition = (Vector2)children[i].localPosition - new Vector2(0.5f, 0.5f)
                                        + new Vector2(0.5f, 0) * UnityEngine.Random.Range(0, 3)
                                        + new Vector2(0, 0.5f) * UnityEngine.Random.Range(0, 3);
        }

        cnt = 0;
    }

    public bool update()
    {
        cnt++;
        for (int i = 0; i < 4; i++)
        {
            children[i].localPosition = (Vector2)children[i].localPosition
                                        + new Vector2(0.2f, 0) * (1 - i) * ((i + 1) % 2)
                                        + new Vector2(0, 0.2f) * (2 - i) * (i % 2);
        }

        if (cnt == 15) return false;
        return true;
    }
    public void rm()
    {
        gameObject.tag = "REMOVE";
    }
}