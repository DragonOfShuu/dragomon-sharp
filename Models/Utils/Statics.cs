namespace DragoSharp.Models.Utils;

/// <summary>
/// General-purpose static utility helpers used throughout the model layer.
/// Contains no I/O — pure logic.
/// </summary>
public static class Statics
{
    private static readonly Random Rng = new();

    /// <summary>
    /// Generate a random integer in [min, max).
    /// </summary>
    public static int GenRNum(int min, int max)
    {
        if (max < min)
            throw new ArgumentOutOfRangeException(nameof(max), "Minimum cannot be larger than maximum.");
        if (max - min == 0)
            return min;
        return Rng.Next(min, max);
    }

    /// <summary>
    /// Pick a uniformly random element from an array.
    /// </summary>
    public static T? PickRItem<T>(T[] items)
    {
        if (items.Length == 0) return default;
        if (items.Length == 1) return items[0];
        return items[Rng.Next(0, items.Length)];
    }

    /// <summary>
    /// Invert a value within a [min, max] range.
    /// e.g. InvertNum(0.3, 0, 1) → 0.7
    /// </summary>
    public static float InvertNum(float num, float min, float max)
    {
        float diff = max - min;
        float norm = num - min;
        double pct = (double)norm / (double)diff;
        return (float)((1.0 - pct) * (double)diff) + min;
    }
}
