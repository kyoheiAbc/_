public class Count
{
    public int i = 0;
    public int I;
    public Count(int i)
    {
        this.I = i;
    }
    public void Update()
    {
        if (0 < this.i && this.i < this.I) this.i++;
    }
    public void Start()
    {
        if (this.i == 0) this.i++;
    }
    public bool Finish()
    {
        return this.i == this.I;
    }
}