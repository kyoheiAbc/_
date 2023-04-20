using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoPuyo
{
    List<Puyo> puyo;
    int rot;
    int cnt;

    public PuyoPuyo(Puyo p0, Puyo p1)
    {
        puyo = new List<Puyo> { p0, p1 };
        rot = 3;
        cnt = 0;
    }

    public Vector2 move(Vector2 vec, List<Puyo> pList)
    {
        Vector2 initPos = puyo[0].getPos();
        for (int i = 0; i < 2; i++)
        {
            if (puyo[i].move(vec, pList, true) != vec)
            {
                sync(i);
                if (!puyo[1 - i].canPut(pList))
                {
                    puyo[0].setPos(initPos);
                    sync(0);
                }
                break;
            }
        }
        if (puyo[0].getPos() != initPos) cnt = 0;
        return puyo[0].getPos() - initPos;
    }

    public bool setPos(Vector2 pos)
    {
        puyo[0].setPos(pos);
        sync(0);
        return true;
    }

    public void rotate(int r, List<Puyo> pList)
    {
        rot = (rot + r) % 4;
        if (rot < 0) rot = 3;

        Vector2 pos = puyo[0].getPos();
        puyo[1].setPos(pos);
        puyo[1].move(new Vector2(1 - rot, rot - 2) * new Vector2((rot + 1) % 2, rot % 2), pList, true);
        sync(1);

        if (!puyo[0].canPut(pList))
        {
            rot = (rot - r) % 4;
            if (rot < 0) rot = 3;

            rot = (rot + 2) % 4;
            puyo[1].setPos(pos);
            sync(1);
        }
        cnt = 0;
    }

    public List<Puyo> getPuyo()
    {
        if (puyo[0].getPos().y <= puyo[1].getPos().y) return puyo;
        else return new List<Puyo>() { puyo[1], puyo[0] };
    }

    private void sync(int i)
    {
        puyo[1 - i].setPos(
            puyo[i].getPos()
            + new Vector2(1 - rot, rot - 2) * new Vector2((rot + 1) % 2, rot % 2) * (1 - 2 * i)
        );
    }
    public void setCnt(int c)
    {
        cnt = c;
    }
    public int getCnt()
    {
        return cnt;
    }

    public void render()
    {
        for (int i = 0; i < 2; i++) puyo[i].render();
    }
}
