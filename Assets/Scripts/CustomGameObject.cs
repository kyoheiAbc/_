public class CustomGameObject
{
    int i;
    public int GetI()
    {
        return this.i;
    }
    public CustomGameObject(int i)
    {
        Main.list.Add(this);
        this.i = i;
    }
    virtual public void Update()
    {
        this.i--;
        if (this.i > 0) return;
        Main.list.Remove(this);
        this.End();
    }
    virtual public void End()
    {
    }
}