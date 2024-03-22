using UnityEngine;

public class MinMaxRangeAttribute : PropertyAttribute
{
    public readonly float min, max;

    public MinMaxRangeAttribute(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}