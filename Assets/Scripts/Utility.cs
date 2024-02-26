
public class Utility
{
    public static int[] Shuffle(int[] array)
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