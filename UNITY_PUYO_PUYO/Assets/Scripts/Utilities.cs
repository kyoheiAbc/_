using UnityEngine;

public static class L
{
    public static float QuadraticF(float x, float max)
    {
        return -4f * max * (x - 0.5f) * (x - 0.5f) + max;
    }

    public static int COMBO_TO_OJAMA(int c)
    {
        switch (c)
        {
            case 0: return 0;
            case 1: return 1;
            case 2: return 2;
            case 3: return 3;
            case 4: return 3;
        }
        return (int)(9 * Mathf.Pow(2, c - 5));
    }
}

public class ColorBag
{
    int[] bag;
    int cnt;
    public ColorBag() { }
    public void init()
    {
        int[] colors = new int[5] { 0, 1, 2, 3, 4 };
        for (int i = colors.Length - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            int tmp = colors[i];
            colors[i] = colors[j];
            colors[j] = tmp;
        }

        bag = new int[D.I().COLOR_NUMBER * D.I().COLOR_ADJUST];
        for (int i = 0; i < bag.Length; i++)
        {
            bag[i] = colors[i % D.I().COLOR_NUMBER];
        }
        reset();
    }

    public void reset()
    {
        cnt = -1;
        for (int i = bag.Length - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            int tmp = bag[i];
            bag[i] = bag[j];
            bag[j] = tmp;
        }
    }

    public int getColor()
    {
        if (cnt == bag.Length - 1)
        {
            reset();
        }
        cnt++;
        return bag[cnt];
    }
}