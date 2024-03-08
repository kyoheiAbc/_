using UnityEngine;
public class Ai
{
    bool atk;
    int walk;
    Ninja ninja;
    Ninja target;
    public Ai(Ninja n)
    {
        this.atk = false;
        this.walk = 0;
        this.ninja = n;
        this.target = Main.instance.nearestNinja(this.ninja);
    }
    public void update()
    {
        Vector3 old = this.ninja.getPos();

        if (this.ninja.getStun() > 0) this.atk = false;

        if (this.ninja.getHp() < 0 || this.ninja.getStun() > 0) return;


        if (Main.instance.getFrame() % 30 == (int)(30 * this.ninja.getRandom()))
        {
            this.target = Main.instance.getPlayer();
        }

        Vector3 v = this.target.getPos() - this.ninja.getPos();
        v.y = 0;



        walk();
        void walk()
        {
            if (this.walk == 0)
            {
                if (Random.Range(0, 60) == 0) this.walk = Random.Range(30, 60);
                return;
            }
            if (v.sqrMagnitude < Mathf.Pow(2f + 2 * this.ninja.getRandom(), 2))
            {
                this.walk = 0;
                return;
            }
            this.ninja.mv(v.normalized * 0.1f);
            this.walk--;
        }

        atk();
        void atk()
        {
            if (this.atk) return;
            if (this.ninja.attack.getI() != 0) return;
            if (Random.Range(0, 90) != 0) return;
            if (!this.ninja.jump(Main.forward(this.ninja.getRot() * Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up)))) return;
            this.atk = true;
        }
        if (this.atk && this.ninja.attack.getI() == 0 && this.ninja.getVec().y == 0) this.ninja.attack.exe();
        if (this.atk && this.ninja.attack.getI() % 100 == 15) this.atk = this.ninja.attack.exe();
        if (this.atk && this.ninja.getPos() == old) this.ninja.mv(v.normalized * 0.1f);
    }
}
