using UnityEngine;

public static class ColorExtensions
{
    public static Color Update(this Color color,
        float? r = null, float? g = null, float? b = null,
        float? a = null)
    {
        color.r = r ?? color.r;
        color.g = g ?? color.g;
        color.b = b ?? color.b;
        color.a = a ?? color.a;
        return color;
    }
}