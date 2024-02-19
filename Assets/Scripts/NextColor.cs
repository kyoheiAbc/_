public class NextColor
{
    private int[] array;
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