public class Color
{
    private int[] array = new int[]{
        0,1,2,3,
        0,1,2,3,
        0,1,2,3,
        0,1,2,3
    };
    private int i = 16;

    private void Reset()
    {
        this.i = 0;

        for (int I = this.array.Length - 1; I > 0; I--)
        {
            int r = UnityEngine.Random.Range(0, I + 1);
            int a = this.array[r];
            this.array[r] = this.array[I];
            this.array[I] = a;
        }
    }
    public int Get()
    {
        if (this.i > this.array.Length - 1) this.Reset();
        this.i++;
        return this.array[i - 1];
    }
}