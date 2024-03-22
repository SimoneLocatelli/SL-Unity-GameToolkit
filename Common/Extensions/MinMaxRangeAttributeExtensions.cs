using UnityEngine;

public static class MinMaxRangeAttributeExtensions
{
    public static Vector2 ToVector2(this MinMaxRangeAttribute minMaxRange)
        => new Vector2(minMaxRange.min, minMaxRange.max);
}