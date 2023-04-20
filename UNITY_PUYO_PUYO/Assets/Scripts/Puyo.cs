using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Puyo
{
    private Vector2 pos;
    GameObject gO;
    Transform t;
    int color;
    int cnt;

    public Puyo(GameObject g)
    {
        gO = g;
        t = gO.transform;
        pos = t.position;
        switch (gO.name)
        {
            case "puyoA(Clone)": color = 0; break;
            case "puyoB(Clone)": color = 1; break;
            case "puyoC(Clone)": color = 2; break;
            case "puyoD(Clone)": color = 3; break;
            case "puyoE(Clone)": color = 4; break;
            case "puyoZ(Clone)": color = 9; break;
            default: color = 255; break;
        }
        cnt = D.I().EFFECT_FIX_CNT;
    }

    public void setPos(Vector2 p)
    {
        pos = p;
    }

    public Vector2 move(Vector2 vec, List<Puyo> pList, bool parent)
    {
        Vector2 initPos = pos;

        pos += vec;

        for (int i = 0; i < pList.Count; i++)
        {
            Puyo p = pList[i];

            if (p == this) continue;
            if (Vector2.SqrMagnitude(pos - p.getPos()) >= 1) continue;


            if (vec.x != 0)
            {
                if (Mathf.Abs(pos.y - p.getPos().y) < 0.25)
                {
                    pos = initPos;
                    break;
                }
                pos.y = p.getPos().y + D.I().VEC_Y.y * Mathf.Sign(pos.y - p.getPos().y);
                if (!canPut(pList))
                {
                    pos = initPos;
                    break;
                }
            }
            else
            {
                pos.y = p.getPos().y - D.I().VEC_Y.y * Mathf.Sign(vec.y);
            }
        }

        if (!parent && pos.y < initPos.y) cnt = 0;
        return pos - initPos;
    }

    public bool canPut(List<Puyo> pList)
    {
        for (int i = 0; i < pList.Count; i++)
        {
            if (pList[i] == this) continue;
            if (Vector2.SqrMagnitude(pos - pList[i].getPos()) < 1) return false;
        }
        return true;
    }

    public bool canDrop(List<Puyo> pList)
    {
        for (int i = 0; i < pList.Count; i++)
        {
            if (pList[i] == this) continue;
            if (pos.x != pList[i].getPos().x) continue;
            if (pos.y - pList[i].getPos().y != 1) continue;
            return false;
        }
        return true;
    }

    public int getColor()
    {
        return color;
    }
    public void setColor(int c)
    {
        color = c;
    }

    public Vector2 getPos()
    {
        return pos;
    }

    public int getCnt()
    {
        return (int)cnt;
    }
    public void setCnt(int c)
    {
        cnt = c;
    }
    public void rm()
    {
        gO.tag = "REMOVE";
    }

    public void effect()
    {
        if (cnt == D.I().EFFECT_FIX_CNT)
        {
            if (color == 255) cnt = 100 - 1;
            // if (color == 255 && gO.name != "puyoZ(Clone)") cnt = 100 - 1;
            else return;
        }
        if (cnt == D.I().EFFECT_REMOVE_CNT + 100) return;
        cnt++;
    }

    public void render()
    {
        if (cnt < 100)
        {
            t.position = new Vector2(pos.x, pos.y - L.QuadraticF((float)cnt / (float)D.I().EFFECT_FIX_CNT, 0.1f));
            t.localScale = new Vector2(1 + L.QuadraticF((float)cnt / (float)D.I().EFFECT_FIX_CNT, 0.1f), 1);
        }
        else if (cnt == 100)
        {
            t.localScale = new Vector2(0.9f, 1.1f);
        }
        else if (cnt == (int)(D.I().EFFECT_REMOVE_CNT / 3) + 100)
        {
            t.localScale = D.I().VEC_0;
        }
        else if (cnt == (int)(D.I().EFFECT_REMOVE_CNT * 2 / 3) + 100)
        {
            t.localScale = new Vector2(0.9f, 1.1f);
        }
    }

}
