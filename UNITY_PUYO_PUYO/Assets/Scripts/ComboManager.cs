using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboManager
{

    int combo;
    int plus;
    int cnt;
    TextMeshPro text;
    Transform t;
    Gauge gauge;

    public ComboManager(GameObject gO)
    {
        t = GameObject.Find("ComboUI").transform;
        text = GameObject.Find("ComboUI").GetComponent<TextMeshPro>();
        t.gameObject.GetComponent<MeshRenderer>().sortingOrder = 99;

        gO.transform.Rotate(0, 0, 90);
        gauge = new Gauge(1000, new Vector2(5f, 2f), gO, UnityEngine.Color.HSVToRGB(0.9f, 0.35f, 1));

        init();
    }

    public void init()
    {
        combo = 0;
        cnt = 0;
        gauge.setPoint(cnt);
        text.text = "";
    }

    public int update()
    {
        if (cnt < 0)
        {
            combo = 0;
            text.text = "";
        }
        else if (cnt < 1000)
        {
            float a = D.I().COMBO_CNT * ((combo + 8) / 9f);
            cnt = cnt - (int)(1000f / a);
        }
        gauge.setPoint(cnt);
        return cnt;
    }

    public void setCombo(Vector2 pos)
    {
        combo = combo + plus;
        if (combo > D.I().COMBO_MAX) combo = D.I().COMBO_MAX;
        plus = 0;
        cnt = 1000;
        gauge.setPoint(cnt);

        text.text = combo + " COMBO";
        if (pos.x < 3.5f) pos.x = 3.5f;
        t.position = pos;
    }

    public int getCombo()
    {
        return combo;
    }

    public void setPlus(int p)
    {
        plus = p;
    }
    public void setCnt(int c)
    {
        cnt = c;
    }
    public int getCnt()
    {
        return cnt;
    }
}
