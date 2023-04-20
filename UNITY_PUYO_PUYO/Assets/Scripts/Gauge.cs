using UnityEngine;

public class Gauge
{
    Transform[] children;
    float max;
    float point;
    public Gauge(float m, Vector2 s, GameObject gO, Color c)
    {
        Transform t = gO.transform;
        t.localScale = s;
        children = new Transform[4];
        children[0] = t.GetChild(0).gameObject.transform;
        children[1] = t.GetChild(1).gameObject.transform;
        children[2] = t.GetChild(2).gameObject.transform;
        children[2].GetComponent<SpriteRenderer>().color = c;
        children[3] = t.GetChild(3).gameObject.transform;
        children[3].transform.localScale = new Vector2(0, 0);

        max = m;
        init();
    }

    public void init()
    {
        point = 0;
        setUi();
        setUiTmp(0);
    }

    public void cover(float c)
    {
        children[3].transform.localScale = new Vector2(c / max, 1);
        children[3].transform.localPosition = new Vector2((max - c) / (2 * max), 0);

    }

    public void setPoint(float p)
    {
        point = p;
        if (point < 0) point = 0;
        if (max < point) point = max;

        setUi();

    }
    public float getPoint()
    {
        return point;
    }


    public void setUiTmp(float p)
    {
        children[2].transform.localScale = new Vector2(p / max, 1);
        children[2].transform.localPosition = new Vector2(-(max - p) / (2 * max), 0);
    }

    public void setUi()
    {
        if (children[1].transform.localScale == children[2].transform.localScale)
        {
            children[1].transform.localScale = new Vector2(point / max, 1);
            children[1].transform.localPosition = new Vector2(-(max - point) / (2 * max), 0);
            children[2].transform.localScale = new Vector2(point / max, 1);
            children[2].transform.localPosition = new Vector2(-(max - point) / (2 * max), 0);
        }
        else
        {
            children[1].transform.localScale = new Vector2(point / max, 1);
            children[1].transform.localPosition = new Vector2(-(max - point) / (2 * max), 0);
        }

    }

}
