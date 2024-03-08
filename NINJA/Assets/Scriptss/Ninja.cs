using System.Collections.Generic;
using UnityEngine;
public class Ninja
{
    Ai ai;
    public void setAi(Ai s) { this.ai = s; }
    public Attack attack;
    int i; public int getI() { return this.i; }
    int hp; public int getHp() { return this.hp; }
    public void setHp(int s) { this.hp = s; }
    Vector3 pos; public Vector3 getPos() { return this.pos; }
    public void setPos(Vector3 s) { this.pos = s; }
    float random; public float getRandom() { return this.random; }
    public Renderer renderer;
    Quaternion rot; public Quaternion getRot() { return this.rot; }
    public void setRot(Quaternion s) { this.rot = s; }
    int stun; public int getStun() { return this.stun; }
    public void setStun(int s) { this.stun = s; }

    Vector3 vec; public Vector3 getVec() { return this.vec; }
    public void setVec(Vector3 s) { this.vec = s; }

    public void addHp(int a) { this.hp += a; }
    public void addVec(Vector3 a) { this.vec += a; }

    public Ninja(int i)
    {
        this.ai = new Ai(this);
        this.attack = new Attack(this);
        this.hp = 4;
        this.i = i;
        this.pos = Vector3.zero;
        this.random = Random.Range(0, 1f);
        this.rot = Quaternion.identity;
        this.stun = 0;
        this.vec = Vector3.zero;

        this.renderer = new Renderer(this);
    }

    public void update()
    {
        if (this.hp < 0) this.hp--;
        if (this.stun > 0) this.stun--;

        physics();
        void physics()
        {
            this.pos += this.vec;
            this.vec.x *= 0.75f;
            this.vec.z *= 0.75f;
            if (this.pos.y > 0.001f) this.vec.y -= 0.05f;
            else
            {
                this.pos.y = 0;
                this.vec.y = 0;
            }
        }

        this.attack.update();


        if (this.ai != null) this.ai.update();
    }

    public bool jump(Vector3 v)
    {
        if (pos.y != 0) return false;
        this.attack.setI(0);
        this.addVec(Vector3.up * 0.4f + v.normalized * 0.6f);
        return true;
    }
    public void mv(Vector3 v)
    {
        if (v == Vector3.zero) return;
        this.setRot(Quaternion.LookRotation(v));
        this.pos += v;
    }
}

public class Attack
{
    Ninja ninja;
    int i; public int getI() { return this.i; }
    public void setI(int s) { this.i = s; }

    public Attack(Ninja n)
    {
        this.ninja = n;
        this.i = 0;
    }
    public bool exe()
    {
        if (this.ninja.getPos().y != 0) return false;
        if (this.i % 100 >= 25) return false;
        if (this.i > 300) return false;
        this.i = 100 * (this.i / 100) + 130;
        return true;
    }
    public void update()
    {
        if (this.i > 0) this.i--;

        if (this.i % 100 == 0) this.i = 0;

        if (this.i % 100 != 25) return;

        List<Ninja> l = Main.instance.getList(this.ninja, 2.5f * 2.5f, 90);

        for (int i = 0; i < l.Count; i++)
        {
            int c = this.i / 100;
            l[i].addHp(-c);
            l[i].addVec(Main.forward(this.ninja.getRot()) * 0.2f * c + 0.1f * c * Vector3.up);
            l[i].setStun(5);
            l[i].attack.setI(0);
        }
    }
}
