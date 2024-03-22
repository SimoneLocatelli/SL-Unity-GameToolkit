using UnityEngine;

public static class PreciseLerp
{
    public static float Lerp(float startValue, float endValue, float interpolation, float threshold = 0.95f)
    {
        var interpolatedValue = Mathf.Lerp(startValue, endValue, interpolation);
        var interpolatedPercentage = interpolatedValue / endValue;
        if (interpolatedPercentage >= threshold)
            return endValue;

        return interpolatedValue;
    }

    internal static Vector3 Lerp(Vector3 startValue, Vector3 endValue, float interpolation, float distanceThreshold = 0.5f)
    {
        var interpolatedValue = Vector3.Lerp(startValue, endValue, interpolation);
        if (endValue.IsInRange(interpolatedValue, distanceThreshold))
            return endValue;

        return interpolatedValue;
    }
}