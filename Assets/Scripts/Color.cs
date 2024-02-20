public class Color
{
    private int[] array = Shuffle(new int[] { 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3 });
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
    static int[] Shuffle(int[] array)
    {
        int[] a = (int[])array.Clone();
        for (int i = a.Length - 1; i > 0; i--)
        {
            int r = UnityEngine.Random.Range(0, i + 1);
            (a[r], a[i]) = (a[i], a[r]);
        }
        return a;
    }
}