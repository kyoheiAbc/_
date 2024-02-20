using UnityEngine;
public class Combo
{
    private int combo;
    private Count end = new Count(60);


    public Combo()
    {
        this.Start();
    }

    public void Update(int i)
    {
        this.end.Update();

        if (this.end.i > 180)
        {
            this.Start();
            return;
        }

        if (this.combo > 0 && i == -1)
        {
            this.end.Start();
        }
        if (i != -1)
        {
            this.combo += i;
            if (i > 0)
            {
                this.end.i = 0;
            }
        }
    }
    public void Start()
    {
        this.end.i = 0;
        this.combo = 0;
    }

    public int GetCombo()
    {
        return this.combo;
    }



}