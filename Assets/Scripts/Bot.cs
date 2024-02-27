using UnityEngine;

public class Bot
{
    public int health;
    public Combo combo = new Combo();
    private Attack attack;
    private float random;


    public Bot()
    {
        this.Start();
    }

    public void Start()
    {
        this.health = 16;
        this.combo.Start();
        this.attack = null;
        this.random = Random.Range(0f, 1f);
    }
    public void Update()
    {
        this.combo.Update();

        if (this.attack != null) return;
        if (this.combo.i != 0) return;
        if (Time.frameCount < 180) return;

        if (Time.frameCount % 180 == (int)(180 * this.random) && Random.Range(0, 3) == 0)
        {
            this.attack = new Attack(this, Random.Range(3, 8));
        }
    }

    private class Attack : CustomGameObject
    {
        Bot parent;
        int i;
        public Attack(Bot bot, int i) : base(120)
        {
            this.parent = bot;
            this.i = i;
            this.parent.combo.Add(1);
        }

        public override void End()
        {
            this.i--;
            if (this.i == 0)
            {
                this.parent.attack = null;
                this.parent.combo.end.Start();
            }
            else
            {
                parent.attack = new Attack(parent, i);
            }
        }

    }
}