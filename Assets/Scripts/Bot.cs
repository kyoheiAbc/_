using UnityEngine;

public class Bot
{
    private static readonly int COMBO = 90;
    public int health;

    public Count energy = null;
    public Combo combo = new Combo();
    private Attack attack;
    public int attackIteration;
    public Bot()
    {
        this.Start();
    }
    public void Start()
    {
        this.health = Static.BOT_HEALTH;
        this.combo.Start();
        this.attack = null;
        this.energy = null;
        this.attackIteration = Static.Shuffle(new int[] { 3, 3, 3, 5, 5, 7 })[0];
    }
    public void Update()
    {
        this.combo.Update();
        if (this.energy != null) this.energy.Update();

        if (this.attack != null) return;
        if (this.combo.i != 0) return;

        if (this.energy == null) this.energy = new Count(Static.BOT_SPEED * this.attackIteration);
        this.energy.Launch();
        if (this.energy.Finish())
        {
            this.attack = new Attack(this, this.attackIteration);
            this.energy = null;
            this.attackIteration = Static.Shuffle(new int[] { 3, 3, 3, 5, 5, 7 })[0];
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