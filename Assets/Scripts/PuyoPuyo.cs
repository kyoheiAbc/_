using System.Collections.Generic;
using UnityEngine;

public class PuyoPuyo
{
    // private Puyo[] array;
    // public PuyoPuyo(Puyo p0, Puyo p1)
    // {
    //     this.puyo = new Puyo[] { p0, p1 };
    //     this.puyo[0].SetPuyoPuyo(this);
    //     this.puyo[1].SetPuyoPuyo(this);
    // }

    // public bool Update(PuyoManager puyoManager)
    // {
    //     if (this.Move(new Vector2(0, -0.05f), puyoManager) != Vector2.zero) return true;

    //     this.puyo[0].SetPuyoPuyo(null);
    //     this.puyo[1].SetPuyoPuyo(null);
    //     return false;
    // }


    // public Vector2 Move(Vector2 v, PuyoManager puyoManager)
    // {
    //     Vector2 p = this.puyo[0].GetPosition();
    //     for (int i = 0; i < 2; i++)
    //     {
    //         // if (this.puyo[i].Move(v, puyoManager) != v)
    //         // {
    //         //     this.sync(i);
    //         //     // if (puyoManager.isColliding(this.puyo[1 - i])) continue;
    //         //     // {
    //         //     //     this.puyo[0].SetPosition(p);
    //         //     //     this.sync(0);
    //         //     // }
    //         //     break;
    //         // }
    //     }
    //     return this.puyo[0].GetPosition() - p;
    // }


    // private void sync(int i)
    // {
    //     this.puyo[1 - i].SetPosition(
    //         this.puyo[i].GetPosition() + Vector2.right * (1 - 2 * i)
    //     );
    // }
}
