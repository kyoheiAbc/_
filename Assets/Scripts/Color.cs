public class Color
{
    private int[] array = Shuffle(new int[] { 0, 1, 2, 3 });
    private int i = 0;
    public int Get()
    {
        if (this.i > this.array.Length - 1)
        {
            this.array = Shuffle(this.array);
            this.i = 0;
        }
        this.i++;
        return this.array[i - 1];
    }
    static int[] Shuffle(int[] a)
    {
        int[] clone = (int[])a.Clone();
        for (int i = clone.Length - 1; i > 0; i--)
        {
            int r = UnityEngine.Random.Range(0, i + 1);
            int _ = clone[i];
            clone[i] = clone[r];
            clone[r] = _;
        }
        return clone;
    }
}