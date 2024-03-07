using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Factory
{
    private List<Puyo> puyoList;

    public List<Puyo> PuyoList() => puyoList;

    public Factory()
    {
        puyoList = new List<Puyo>();
    }

    public void NewPuyo()
    {
        puyoList.Add(new Puyo(new Vector2(UnityEngine.Random.Range(0, 6) + 0.5f, 12.5f)));
    }
}

