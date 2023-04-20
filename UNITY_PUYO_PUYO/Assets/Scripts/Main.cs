using System.Collections.Generic;
using UnityEngine;
using System;

public class Main : MonoBehaviour
{
    InputController inputController;
    Field field;
    PuyoManager puyoManager;
    PuyoPuyo puyoPuyo;
    PuyoPuyo[] puyoPuyoNext;
    ColorBag colorBag;
    ComboManager comboManager;
    EffectManager effectManager;
    Boss boss;
    OjamaManager ojamaManager;

    int cnt;

    private void Awake()
    {
        new Options();

        Application.targetFrameRate = D.I().FPS;

        // Camera
        if ((float)Screen.height / (float)Screen.width >= (2050f / 950f))
        {
            Camera.main.orthographicSize =
                Camera.main.orthographicSize * (float)Screen.height / (float)Screen.width / (2050f / 950f);
        }
    }

    void Start()
    {
        inputController = new InputController();
        field = new Field();
        puyoManager = new PuyoManager();
        puyoPuyoNext = new PuyoPuyo[2];
        colorBag = new ColorBag();
        comboManager = new ComboManager(Instantiate(D.I().GAUGE, new Vector2(8.5f, 3.5f), Quaternion.identity));
        effectManager = new EffectManager();

        GameObject[,] gOary = new GameObject[12, 6];
        // GameObject gO = Resources.Load<GameObject>("puyoZ_");
        // for (int y = 0; y < 12; y++)
        // {
        //     for (int x = 0; x < 6; x++)
        //     {
        //         gOary[y, x] = Instantiate(gO, new Vector2(x + 1.5f, y + 1.5f), Quaternion.identity);
        //     }
        // }
        ojamaManager =
            new OjamaManager(
                gOary,
                Instantiate(D.I().GAUGE, new Vector2(5.25f, 13.75f), Quaternion.identity)
            );

        boss = new Boss(
            new GameObject[3] {
                Instantiate(D.I().GAUGE, new Vector2(7.5f, 19.35f), Quaternion.identity),
                Instantiate(D.I().GAUGE, new Vector2(7.5f, 18.85f), Quaternion.identity),
                Instantiate(D.I().GAUGE, new Vector2(7.5f, 19f), Quaternion.identity)
            }
        );

        reset();
    }

    public void reset()
    {
        GameObject[] gOary;
        gOary = GameObject.FindGameObjectsWithTag("PUYO");
        for (int i = 0; i < gOary.Length; i++) Destroy(gOary[i]);

        gOary = GameObject.FindGameObjectsWithTag("REMOVE");
        for (int i = 0; i < gOary.Length; i++) Destroy(gOary[i]);

        gOary = GameObject.FindGameObjectsWithTag("EFFECT");
        for (int i = 0; i < gOary.Length; i++) Destroy(gOary[i]);

        inputController.init();
        field.init();
        puyoManager.init();
        colorBag.init();
        effectManager.init();
        comboManager.init();
        boss.init();
        ojamaManager.init();

        GameObject gO = Resources.Load<GameObject>("puyo");
        for (int y = 0; y < D.I().FIELD_SIZE_Y; y++)
        {
            for (int x = 0; x < D.I().FIELD_SIZE_X; x++)
            {
                if (y == 0 || // y == D.I().FIELD_SIZE_Y - 1 ||
                    x == 0 || x == D.I().FIELD_SIZE_X - 1)
                {
                    gO.transform.position = new Vector2(x + 0.5f, y + 0.5f);
                    Puyo puyo = new Puyo(gO);
                    field.setPuyo(puyo);
                    puyoManager.addPuyo(puyo);
                }
            }
        }

        puyoPuyo = newPuyoPuyo(new Vector2(3.5f, 12.5f));
        puyoPuyoNext[0] = newPuyoPuyo(new Vector2(8.5f, 11f));
        puyoPuyoNext[1] = newPuyoPuyo(new Vector2(8.5f, 8.5f));

        cnt = 0;
    }

    void Update()
    {


        // debug
        int oldTime = DateTime.Now.Millisecond;

        // game over
        if (cnt >= 900)
        {
            cnt++;
            if (cnt - 900 != (int)D.I().NEXT_GAME_CNT) return;
            reset();
        }

        if (field.getPuyo(new Vector2(3.5f, 12.5f)) != null)
        {
            cnt = 900;
            return;
        }

        if (boss.getHp() == 0)
        {
            cnt = 900;
            return;
        }

        // generate
        if (puyoPuyo == null)
        {
            nextPuyo();
            inputController.init();
        }

        // move puyoPuyo
        int input = inputController.update();
        switch (input)
        {
            case 4:
            case 6:
                puyoPuyo.move(D.I().VEC_X * Mathf.Sign(input - 5), puyoManager.getList());
                break;
            case 2:
                if (puyoPuyo.move(-D.I().VEC_Y / 2, puyoManager.getList()) == D.I().VEC_0)
                {
                    // ready fix
                    puyoPuyo.setCnt(D.I().FIX_CNT);
                    puyoPuyo.getPuyo()[0].setCnt(0);
                    puyoPuyo.getPuyo()[1].setCnt(0);
                }
                break;
            case 8:
                for (int n = 0; n < D.I().FIELD_SIZE_Y - 1; n++)
                {
                    puyoPuyo.move(-D.I().VEC_Y, puyoManager.getList());
                }
                // ready fix
                puyoPuyo.setCnt(D.I().FIX_CNT);
                puyoPuyo.getPuyo()[0].setCnt(0);
                puyoPuyo.getPuyo()[1].setCnt(0);

                break;
            case 14:
            case 16:
                puyoPuyo.rotate((int)Mathf.Sign(input - 15), puyoManager.getList());
                break;
        }


        // drop
        puyoPuyo.move(D.I().VEC_DROP, puyoManager.getList());
        puyoManager.move(D.I().VEC_DROP_QUICK, puyoPuyo.getPuyo());



        // field set
        puyoPuyo.setCnt(puyoPuyo.getCnt() + 1);
        if (puyoPuyo.getCnt() > D.I().FIX_CNT)
        {
            List<Puyo> puyo = puyoPuyo.getPuyo();
            if (field.getPuyo(puyo[0].getPos() + D.I().UNDER) == null && field.getPuyo(puyo[1].getPos() + D.I().UNDER) == null)
            {
                // ready fix cancel
                puyoPuyo.setCnt(0);
                puyoPuyo.getPuyo()[0].setCnt(D.I().EFFECT_FIX_CNT);
                puyoPuyo.getPuyo()[1].setCnt(D.I().EFFECT_FIX_CNT);
            }
            else
            {
                puyoPuyo = null;
                for (int i = 0; i < puyo.Count; i++) puyoManager.addPuyo(puyo[i]);
                if (cnt < 200) cnt = 100;
            }
        }
        puyoManager.setPuyo(field);


        // fix effect
        field.effect();


        // rm check
        List<Puyo> puyoPuyoList;
        if (puyoPuyo != null) puyoPuyoList = puyoPuyo.getPuyo();
        else puyoPuyoList = new List<Puyo>();

        if (cnt == 100 && !field.effectIng() && !puyoManager.canDrop(puyoPuyoList))
        {
            int rmCnt = field.rmCheck();
            if (rmCnt > 0)
            {
                cnt = 200;
                comboManager.setPlus(rmCnt);
            }
            else
            {
                cnt = 0;
                if (comboManager.getCnt() == 1000)
                {
                    comboManager.setCnt(1000 - 1);
                }

                if (comboManager.getCombo() == 0 && ojamaManager.getAtk() - ojamaManager.getDmg() < 0)
                {
                    ojamaManager.setDmg(
                        ojamaManager.getDmg() - ojamaManager.getAtk() -
                        ojameGen(
                            ojamaManager.getDmg() - ojamaManager.getAtk()
                        )
                    );
                    ojamaManager.setAtk(0);
                }
            }
        }


        // rm
        if (cnt >= 200)
        {
            cnt++;
            if (cnt == 200 + D.I().EFFECT_REMOVE_CNT)
            {
                cnt = 100;

                field.rm();
                puyoManager.rm();
                GameObject[] gO = GameObject.FindGameObjectsWithTag("REMOVE");

                Vector2 p = D.I().VEC_0;
                for (int i = 0; i < gO.Length; i++)
                {
                    p = p + (Vector2)gO[i].transform.position / gO.Length;
                    effectManager.add(gO[i], Instantiate(D.I().EFFECT_EXPLOSION));
                    Destroy(gO[i]);
                }
                comboManager.setCombo(p);
                ojamaManager.addAtkTmp(L.COMBO_TO_OJAMA(comboManager.getCombo()));
            }
        }

        // combo
        comboManager.update();

        if (comboManager.getCombo() == 0)
        {
            ojamaManager.fixAtk();
        }

        // ojama
        ojamaManager.update();

        // boss
        ojamaManager.addDmgTmp(L.COMBO_TO_OJAMA(boss.update()));

        if (boss.getCombo() == 0)
        {
            ojamaManager.fixDmg();
            if (ojamaManager.getAtk() - ojamaManager.getDmg() > 0)
            {
                boss.setHp(boss.getHp() - (ojamaManager.getAtk() - ojamaManager.getDmg()));
                ojamaManager.setAtk(0);
                ojamaManager.setDmg(0);
            }
        }


        // render
        if (puyoPuyo != null) puyoPuyo.render();
        puyoManager.render();

        effectManager.render();
        GameObject[] gOe = GameObject.FindGameObjectsWithTag("REMOVE");
        for (int i = 0; i < gOe.Length; i++) Destroy(gOe[i]);


        // debug
        int nowTime = DateTime.Now.Millisecond;
        // if (nowTime - oldTime >= 0) Debug.Log("- " + (nowTime - oldTime) + " -");

    }

    private void nextPuyo()
    {
        puyoPuyo = puyoPuyoNext[0];
        puyoPuyo.setPos(new Vector2(3.5f, 12.5f));

        puyoPuyoNext[0] = puyoPuyoNext[1];
        puyoPuyoNext[0].setPos(new Vector2(8.5f, 11));

        puyoPuyoNext[1] = newPuyoPuyo(new Vector2(8.5f, 8.5f));

        puyoPuyo.render();
        puyoPuyoNext[0].render();
        puyoPuyoNext[1].render();
    }

    private PuyoPuyo newPuyoPuyo(Vector2 pos)
    {
        return new PuyoPuyo(
            newPuyo(colorBag.getColor(), pos),
            newPuyo(colorBag.getColor(), pos + D.I().VEC_Y)
        );
    }

    private Puyo newPuyo(int color, Vector2 pos)
    {
        return new Puyo(Instantiate(D.I().PUYO[color], pos, Quaternion.identity));
    }

    private int ojameGen(int num)
    {
        if (num > D.I().OJAMA_MAX_ONE_TIME) num = D.I().OJAMA_MAX_ONE_TIME;
        int[] xAry = new int[6];
        for (int i = 0; i < xAry.Length; i++)
        {
            xAry[i] = i;
        }
        for (int i = xAry.Length - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            int tmp = xAry[i];
            xAry[i] = xAry[j];
            xAry[j] = tmp;
        }

        int n = 0;
        for (int y = 0; y < 12; y++)
        {
            for (int x = 0; x < 6; x++)
            {
                n++;
                Vector2 pos = new Vector2(xAry[x] + 1.5f, 14.5f + y);
                if (field.getPuyo(new Vector2(pos.x, 13.5f - y)) == null)
                {
                    puyoManager.addPuyo(newPuyo(9, pos));
                }
                if (n >= num) return n;
            }
        }
        return num;
    }

}
