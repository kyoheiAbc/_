using UnityEngine;
public class Combo
{
    private int combo;
    private I end = new I();


    public Combo()
    {
        this.Reset();
    }

    public void Update(Remove r)
    {
        this.end.Update();

        if (this.end.i > 180)
        {
            this.Reset();
            return;
        }

        if (this.combo > 0 && r == null)
        {
            this.end.Start();
        }

        if (r != null)
        {
            this.combo += r.GetI();
            if (r.GetI() > 0)
            {
                this.end.i = 0;
            }
            r.SetI(0);
        }
    }
    public void Reset()
    {
        this.end.i = 0;
        this.combo = 0;
    }

    public int GetCombo()
    {
        return this.combo;
    }



}