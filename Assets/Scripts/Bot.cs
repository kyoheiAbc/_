using UnityEngine;

public class Bot
{
    private static readonly int COMBO = 90;
    public float health;

    public Combo combo = new Combo();
    private Attack attack;
    public Bot()
    {
        this.Start();
    }
    public void Start()
    {
        this.health = 1;
        this.combo.Start();
        this.attack = null;
    }
    public void Update()
    {
        this.combo.Update();

        if (this.attack != null) return;
        if (this.combo.i != 0) return;

        if (Random.Range(0, 16 * 60) == 0)
        {
            this.attack = new Attack(this, Random.Range(3, 8));
        }
    }

    private class Attack : CustomGameObject
    {
        private Bot parent;
        private int repeat;
        public Attack(Bot bot, int i) : base(Bot.COMBO)
        {
            this.parent = bot;
            this.repeat = i;
            this.parent.combo.Add(1);
        }
        public override void End()
        {
            this.repeat--;
            if (this.repeat == 0)
            {
                this.parent.attack = null;
                this.parent.combo.end.Launch();
            }
            else
            {
                parent.attack = new Attack(parent, this.repeat);
            }
        }
    }
}