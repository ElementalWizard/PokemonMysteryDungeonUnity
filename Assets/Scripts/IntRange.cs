using System;

[Serializable]
public class IntRange
{
    public int min;
    public int max;

    public IntRange(int v1, int v2)
    {
        this.min = v1;
        this.max = v2;
    }

    public int Random {
        get { return UnityEngine.Random.Range(min, max); }
    }
}
