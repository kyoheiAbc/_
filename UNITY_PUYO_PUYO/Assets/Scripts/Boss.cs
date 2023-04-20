using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss
{
    int cnt;
    int combo;
    int[] atk;
    Gauge[] gauge;

    public Boss(GameObject[] gO)
    {
        gauge = new Gauge[3]{
            new Gauge(D.I().BOSS_HP, new Vector2(4f, 0.3f), gO[0], UnityEngine.Color.HSVToRGB(0.4f, 0.6f, 1)),
            new Gauge(D.I().BOSS_SPEED*D.I().BOSS_ATTACK_GAUGE_MAX, new Vector2(4f, 0.3f), gO[1], UnityEngine.Color.HSVToRGB(0.12f, 0.6f, 1)),
            new Gauge(D.I().BOSS_SPEED, new Vector2(0, 0), gO[2], UnityEngine.Color.HSVToRGB(1, 0.5f, 1)),
        };

        atk = new int[2];
    }

    public void init()
    {
        cnt = 0;
        combo = 0;
        for (int i = 0; i < 3; i++)
        {
            gauge[i].init();
        }
        gauge[0].setPoint(D.I().BOSS_HP);
        gauge[1].cover((D.I().BOSS_ATTACK_GAUGE_MAX - (D.I().BOSS_ATTACK) - 1) * D.I().BOSS_SPEED);

        atk[0] = UnityEngine.Random.Range((int)D.I().BOSS_ATTACK - 1, (int)D.I().BOSS_ATTACK + 1 + 1);
        atk[1] = UnityEngine.Random.Range((int)D.I().BOSS_ATTACK - 1, (int)D.I().BOSS_ATTACK + 1 + 1);
        atk[1] = (int)((float)atk[1] / D.I().BOSS_SMALL_ATTACK_COEF);

        if (atk[0] < 1) atk[0] = 1;
        if (atk[1] < 1) atk[1] = 1;
        if (atk[0] <= atk[1]) atk[1] = atk[0] - 1;


    }

    public int update()
    {
        cnt++;
        if (cnt > D.I().BOSS_SPEED * atk[0])
        {
            if (gauge[1].getPoint() == D.I().BOSS_SPEED * atk[0])
            {
                gauge[1].setUiTmp(0);
                atk[1] = atk[0];
            }
            if (cnt > D.I().BOSS_SPEED * atk[0] + D.I().EFFECT_FIX_CNT + D.I().EFFECT_REMOVE_CNT + 3 / -D.I().VEC_DROP_QUICK.y)
            {
                if (gauge[1].getPoint() < 1 || atk[1] == 0)
                {
                    combo = 0;
                    cnt = (int)gauge[1].getPoint();
                    atk[1] = UnityEngine.Random.Range((int)D.I().BOSS_ATTACK - 1, (int)D.I().BOSS_ATTACK + 1 + 1);
                    atk[1] = (int)((float)atk[1] / D.I().BOSS_SMALL_ATTACK_COEF);
                }
                else
                {
                    atk[1]--;
                    combo++;
                    gauge[1].setPoint(gauge[1].getPoint() - D.I().BOSS_SPEED);
                    cnt = (int)D.I().BOSS_SPEED * atk[0];
                    return combo;
                }
            }
            return 0;
        }

        if (UnityEngine.Random.Range(0, (atk[0] - atk[1]) * (int)D.I().BOSS_SPEED * D.I().BOSS_SMALL_ATTACK_PROB) == 7)
        {
            if (cnt > atk[1] * (int)D.I().BOSS_SPEED)
            {
                gauge[1].setUiTmp(cnt - D.I().BOSS_SPEED * atk[1]);
                cnt = (int)D.I().BOSS_SPEED * atk[0];
                return 0;
            }
        }


        gauge[1].setPoint(cnt);
        return 0;
    }

    public int getCombo()
    {
        return combo;
    }

    public void setHp(float h)
    {
        gauge[0].setPoint(h > 0 ? h : 0);
    }
    public float getHp()
    {
        return gauge[0].getPoint();
    }

}
