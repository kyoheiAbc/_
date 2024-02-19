public class Count
{
    public int i = 0;
    public void Update()
    {
        if (0 < i && i < 256) i++;
    }
    public void Start()
    {
        if (i == 0) i++;
    }
}