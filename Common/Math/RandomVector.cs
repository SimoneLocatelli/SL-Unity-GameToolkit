using UnityEngine;

public static class RandomVector
{
    /// <summary>
    /// To test, not sure if it works.
    /// </summary>
    public static Vector2 GetVectorInBetween(Vector2 minDistance, Vector2 maxDistance, Vector2? center = null)
    {
        var center2 = center ?? Vector2.zero;
        var x = UnityEngine.Random.Range(minDistance.x, maxDistance.x);
        var y = UnityEngine.Random.Range(minDistance.y, maxDistance.y);

        var distanceVector = new Vector2(x, y);
        var resultVector = center2 + distanceVector;

        return resultVector;
    }

    public static Vector2 GetVectorInBetweenTwoRadiuses(int minRadius, int maxRadius, Vector2? origin = null)
    {
        var origin2 = origin ?? Vector2.zero;
        var randomDirection = (Random.insideUnitCircle * origin2).normalized;

        var randomDistance = Random.Range(minRadius, maxRadius + 1);

        var point = origin2 + randomDirection * randomDistance;

        return point;
    }
}