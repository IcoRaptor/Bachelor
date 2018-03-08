using System;

/// <summary>
/// Seeded random number generator
/// </summary>
public sealed class RNG
{
    #region Variables

    private Random _rand;

    #endregion

    #region Constructors

    public RNG(int seed, bool seedInBuild)
    {
#if !UNITY_EDITOR
        if (!seedInBuild)
            seed = (int)(DateTime.Now.Ticks & 0x0000FFFF);
#endif
        _rand = new Random(seed);
    }

    #endregion

    /// <summary>
    /// Generates a random positive value
    /// </summary>
    public int Generate()
    {
        return _rand.Next();
    }

    /// <summary>
    /// Generates a value betwenn 0 and max (exclusive)
    /// </summary>
    /// <param name="max">Maximum value</param>
    public int Generate(int max)
    {
        if (max < 0)
            max = 0;

        return _rand.Next(max);
    }

    /// <summary>
    /// Generates a value between min (inclusive) and max (exclusive)
    /// </summary>
    /// <param name="min">Minimum value</param>
    /// <param name="max">Maximum value</param>
    public int Generate(int min, int max)
    {
        if (min > max)
            min = max;

        return _rand.Next(min, max);
    }

    /// <summary>
    /// Generates a value between 0 and 1
    /// </summary>
    public float Generate01()
    {
        return (float)_rand.NextDouble();
    }
}