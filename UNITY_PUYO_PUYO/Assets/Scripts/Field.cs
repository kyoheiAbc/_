using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field
{
    Puyo[,] ary;

    public Field()
    {
        ary = new Puyo[D.I().FIELD_SIZE_Y, D.I().FIELD_SIZE_X];
    }

    public bool setPuyo(Puyo puyo)
    {
        Vector2 pos = puyo.getPos();
        if ((int)pos.y >= ary.GetLength(0))
        {
            return false;
        }

        ary[(int)pos.y, (int)pos.x] = puyo;

        return true;
    }

    public Puyo getPuyo(Vector2 pos)
    {
        if ((int)pos.y >= ary.GetLength(0)) return null;

        return ary[(int)pos.y, (int)pos.x];
    }

    public int rmCheck()
    {
        int removeCnt = 0;
        for (int y = 1; y < D.I().FIELD_SIZE_Y - 1; y++)
        {
            for (int x = 1; x < D.I().FIELD_SIZE_X - 1; x++)
            {
                int cnt = 0;
                if (cntSameColor(ary[y, x], cnt) >= D.I().REMOVE_NUMBER)
                {
                    removeCnt++;
                    rmSameColor(ary[y, x]);
                }
            }
        }

        for (int y = 1; y < D.I().FIELD_SIZE_Y - 1; y++)
        {
            for (int x = 1; x < D.I().FIELD_SIZE_X - 1; x++)
            {
                if (ary[y, x] == null) continue;
                int color = ary[y, x].getColor();
                if (color != 255 && color >= 100)
                {
                    ary[y, x].setColor(color - 100);
                }
            }
        }
        return removeCnt;
    }


    public void rm()
    {
        for (int y = 1; y < D.I().FIELD_SIZE_Y - 1; y++)
        {
            for (int x = 1; x < D.I().FIELD_SIZE_X - 1; x++)
            {
                if (ary[y, x] == null) continue;

                if (ary[y, x].getColor() == 255)
                {
                    ary[y, x] = null;
                    continue;
                }
                if (getPuyo(ary[y, x].getPos() + D.I().UNDER) == null)
                {
                    Vector2 pos = ary[y, x].getPos();
                    ary[(int)pos.y, (int)pos.x] = null;
                }

            }
        }
    }

    private int cntSameColor(Puyo p, int cnt)
    {
        if (p == null) return cnt;
        int color = p.getColor();
        if (color >= 100) return cnt;
        if (color == 9) return cnt;

        cnt++;

        Puyo[] rtlb = getRtlb(p);
        p.setColor(color + 100);

        for (int i = 0; i < 4; i++)
        {
            if (rtlb[i] == null) continue;
            if (color == rtlb[i].getColor())
            {
                cnt = cntSameColor(rtlb[i], cnt);
            }
        }
        return cnt;
    }

    private void rmSameColor(Puyo p)
    {
        if (p == null) return;
        int color = p.getColor();
        if (color == 255) return;
        if (color == 9) return;
        p.setColor(255);

        Puyo[] rtlb = getRtlb(p);
        for (int i = 0; i < 4; i++)
        {
            if (rtlb[i] == null) continue;
            if (color == rtlb[i].getColor()) rmSameColor(rtlb[i]);
            if (9 == rtlb[i].getColor()) rtlb[i].setColor(255);
        }
    }

    private Puyo[] getRtlb(Puyo puyo)
    {
        Puyo[] rtlb = new Puyo[4];
        for (int i = 0; i < 4; i++)
        {
            rtlb[i] = getPuyo(
                puyo.getPos() + D.I().VEC_X * (1 - i) * ((i + 1) % 2) + D.I().VEC_Y * (2 - i) * (i % 2)
            );
        }
        return rtlb;
    }
    public void effect()
    {
        for (int y = 1; y < D.I().FIELD_SIZE_Y - 1; y++)
        {
            for (int x = 1; x < D.I().FIELD_SIZE_X - 1; x++)
            {
                if (ary[y, x] == null) continue;
                ary[y, x].effect();
            }
        }
    }

    public bool effectIng()
    {
        for (int y = 1; y < D.I().FIELD_SIZE_Y - 1; y++)
        {
            for (int x = 1; x < D.I().FIELD_SIZE_X - 1; x++)
            {
                if (ary[y, x] == null) continue;
                int cnt = ary[y, x].getCnt();
                if (cnt == D.I().EFFECT_FIX_CNT) continue;
                if (cnt == D.I().EFFECT_REMOVE_CNT + 100) continue;
                return true;
            }
        }
        return false;
    }

    public void init()
    {
        for (int y = 0; y < D.I().FIELD_SIZE_Y; y++)
        {
            for (int x = 0; x < D.I().FIELD_SIZE_X; x++)
            {
                ary[y, x] = null;
            }
        }
    }
}
