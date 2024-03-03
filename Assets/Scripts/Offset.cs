
public class Offset
{
    public int[] temporary = new int[2];
    public int i = 0;
    public Offset()
    {
        this.Start();
    }
    public void Start()
    {
        this.temporary = new int[] { 0, 0 };
        this.i = 0;
    }
    public void Update(Factory factory, Combo combo, Bot bot)
    {
        if (combo.update)
        {
            this.temporary[0] += combo.i;
        }
        if (combo.end.GetProgress() == 1)
        {
            this.i += this.temporary[0];
            this.temporary[0] = 0;
        }

        if (bot.combo.update)
        {
            this.temporary[1] += bot.combo.i;
        }
        if (bot.combo.end.GetProgress() == 1)
        {
            this.i -= this.temporary[1];
            this.temporary[1] = 0;
        }

        if (this.i > 0)
        {
            if (bot.combo.i != 0) return;
            bot.health -= this.i;
            if (bot.health < 0) bot.health = 0;
            Render.Character(0);
            this.i = 0;
        }
        else if (this.i < 0)
        {
            if (combo.i != 0) return;

            factory.NewGarbagePuyo((-i) * (int)(Static.BOT_ATTACK / 100f));
            Render.Character(1);
            this.i = 0;
        }
    }
}