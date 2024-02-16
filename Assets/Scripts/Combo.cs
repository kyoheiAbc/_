using UnityEngine;
public class Combo
{
    private int combo;
    private int i;
    private int j;


    public Combo()
    {
        this.Reset();
    }

    public void Update(Remove r)
    {
        if (-1 < this.i && this.i < 256)
        {

            this.i++;
        }

        if (this.j < 256)
        {

            this.j++;
        }



        if (this.i > 180)
        {

            this.Reset();
            return;
        }

        if (this.combo > 0 && r == null && this.i == -1)
        {
            this.i = 0;
        }

        if (r != null)
        {
            this.combo += r.GetI();
            if (r.GetI() > 0)
            {
                this.j = 0;
                this.i = -1;
            }
            r.SetI(0);
        }
    }
    public void Reset()
    {
        this.i = -1;
        this.j = 0;
        this.combo = 0;
    }

    public int GetCombo()
    {
        return this.combo;
    }



}