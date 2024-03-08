// using System.Collections.Generic;
// using UnityEngine;

// public class Special
// {
//     int i; public int getI() { return this.i; }
//     Ninja ninja;
//     GameObject cube;

//     public Special(Ninja n)
//     {
//         this.i = 0;
//         this.ninja = n;
//     }

//     public void exe()
//     {
//         if (this.i != 0) return;
//         if (this.ninja.getPos().y != 0) return;
//         this.ninja.attack.setI(0);
//         this.i = 60;
//     }

//     public void update()
//     {

//         if (this.ninja.getStun() > 0) this.i = 0;
//         if (this.i == 0) return;
//         if (this.i == 1) this.ninja.setStun(30);

//         this.i -= (int)Mathf.Sign(this.i);
//         if (this.ninja.getPos().y != 0) this.i += (int)Mathf.Sign(this.i);



//         if (this.i < 0) return;
//         if (this.i > 30) return;


//         this.ninja.mv(Main.forward(this.ninja.getRot()).normalized * 0.5f);
//         List<Ninja> l = Main.instance.getList(this.ninja, 1.5f * 1.5f, 180);
//         for (int i = 0; i < l.Count; i++)
//         {
//             if (l[i].getStun() != 0) continue;
//             l[i].addHp(-8);
//             l[i].addVec(Vector3.up * 0.5f);
//             l[i].setStun(5);
//         }

//     }
// }