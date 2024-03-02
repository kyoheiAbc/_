using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class Static
{
    public static int ACTIVE = 1;
    public static int THRESHOLD = 3;
    public static int COLOR = 4;
    public static int FIRE = 4;
    public static int DOWN = 3;
    public static int COMBO = 90;
    public static int BOT_HEALTH = 64;
    public static int BOT_ATTACK = 100;
    public static int BOT_SPEED = 300;
    public static int BOT_COMBO_3 = 3;
    public static int BOT_COMBO_5 = 2;
    public static int BOT_COMBO_7 = 1;

    public Static()
    {
        if (File.Exists(Application.persistentDataPath + "/json.json"))
        {
            Json json = JsonUtility.FromJson<Json>(File.ReadAllText(Application.persistentDataPath + "/json.json"));
            Static.ACTIVE = json.active;
            Static.THRESHOLD = json.threshold;
            Static.COLOR = json.color;
            Static.FIRE = json.fire;
            Static.DOWN = json.down;
            Static.COMBO = json.combo;
            Static.BOT_HEALTH = json.botHealth;
            Static.BOT_ATTACK = json.botAttack;
            Static.BOT_SPEED = json.botSpeed;
            Static.BOT_COMBO_3 = json.bot_combo_3;
            Static.BOT_COMBO_5 = json.bot_combo_5;
            Static.BOT_COMBO_7 = json.bot_combo_7;
        }
        else
        {
            Json json = new Json();
            json.active = Static.ACTIVE;
            json.threshold = Static.THRESHOLD;
            json.color = Static.COLOR;
            json.fire = Static.FIRE;
            json.down = Static.DOWN;
            json.combo = Static.COMBO;
            json.botHealth = Static.BOT_HEALTH;
            json.botAttack = Static.BOT_ATTACK;
            json.botSpeed = Static.BOT_SPEED;
            json.bot_combo_3 = Static.BOT_COMBO_3;
            json.bot_combo_5 = Static.BOT_COMBO_5;
            json.bot_combo_7 = Static.BOT_COMBO_7;
            File.WriteAllText(Application.persistentDataPath + "/json.json", JsonUtility.ToJson(json));
        }
    }

    public class Json
    {
        public int active;
        public int threshold;
        public int color;
        public int fire;
        public int down;
        public int combo;
        public int botHealth;
        public int botAttack;
        public int botSpeed;
        public int bot_combo_3;
        public int bot_combo_5;
        public int bot_combo_7;
        public static void Save()
        {
            Json json = new Json();
            json.active = Static.ACTIVE;
            json.threshold = Static.THRESHOLD;
            json.color = Static.COLOR;
            json.fire = Static.FIRE;
            json.down = Static.DOWN;
            json.combo = Static.COMBO;
            json.botHealth = Static.BOT_HEALTH;
            json.botAttack = Static.BOT_ATTACK;
            json.botSpeed = Static.BOT_SPEED;
            json.bot_combo_3 = Static.BOT_COMBO_3;
            json.bot_combo_5 = Static.BOT_COMBO_5;
            json.bot_combo_7 = Static.BOT_COMBO_7;

            File.WriteAllText(Application.persistentDataPath + "/json.json", JsonUtility.ToJson(json));
        }

    }



    public static int[] Shuffle(int[] array)
    {
        int[] a = (int[])array.Clone();
        for (int i = a.Length - 1; i > 0; i--)
        {
            int r = UnityEngine.Random.Range(0, i + 1);
            (a[r], a[i]) = (a[i], a[r]);
        }
        return a;
    }
}
public class CustomGameObject
{
    protected int i;
    public CustomGameObject(int i)
    {
        Main.list.Add(this);
        this.i = i;
    }
    virtual public void Update()
    {
        this.i--;
        if (this.i == 0)
        {
            this.End();
            Main.list.Remove(this);
        }
    }
    virtual public void End() { }
    virtual public void Clear()
    {
        Main.list.Remove(this);
    }
}
public class Count
{
    public int i;
    public readonly int maximum;
    public Count(int i)
    {
        this.i = 0;
        this.maximum = i;
    }
    public void Update()
    {
        if (0 < this.i && this.i < this.maximum) this.i++;
    }
    public void Launch()
    {
        if (this.i == 0) this.i++;
    }
    public bool Finish()
    {
        return this.i == this.maximum;
    }
}
public class Collision
{
    static public Puyo Get(Puyo puyo, List<Puyo> list)
    {
        foreach (Puyo l in list)
        {
            if (puyo == l) continue;
            if (Vector2.SqrMagnitude(puyo.position - l.position) < 1) return l;
        }
        return null;
    }
}