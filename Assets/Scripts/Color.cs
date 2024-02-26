public class Color
{
    private int[] array = Utility.Shuffle(new int[] { 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3 });
    private int i = 0;
    public int Get()
    {
        if (this.i > this.array.Length - 1)
        {
            this.array = Utility.Shuffle(this.array);
            this.i = 0;
        }
        this.i++;
        return this.array[i - 1];
    }
}