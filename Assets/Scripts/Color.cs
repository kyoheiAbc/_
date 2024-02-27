public class Color
{
    private int[] array = Static.Shuffle(new int[] { 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3 });
    private int i = 0;
    public int Get()
    {
        if (this.i > this.array.Length - 1)
        {
            this.array = Static.Shuffle(this.array);
            this.i = 0;
        }
        this.i++;
        return this.array[i - 1];
    }
}
public class NextColor
{
    public int[] array;
    private Color color;
    public NextColor()
    {
        this.color = new Color();
        this.array = new int[] { this.color.Get(), this.color.Get(), this.color.Get(), this.color.Get() };
    }
    public int[] Get()
    {
        int[] a = new int[] { this.array[0], this.array[1] };
        this.array[0] = this.array[2];
        this.array[1] = this.array[3];
        this.array[2] = this.color.Get();
        this.array[3] = this.color.Get();
        return new int[] { a[0], a[1] };
    }
}