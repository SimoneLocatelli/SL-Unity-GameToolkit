using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PointsSorter
{
    public static List<Vector2> SortPoints(IEnumerable<Vector2> points, Vector2 centre)
    {
        return points.OrderBy(x => Mathf.Atan2(x.x - centre.x, x.y - centre.y)).ToList();
    }

    public static List<Vector2> SortPoints(IEnumerable<Vector2> points)
    {
        return points.OrderBy(x => Mathf.Atan2(x.x, x.y)).ToList();
    }

    public static List<Vector2> SortPoints(IEnumerable<Vector3> points, Vector2 centre)
    {
        return SortPoints(points.Select(v => v.ToVector2()), centre);
    }

    public static List<Vector2> SortPoints(IEnumerable<Vector3> points)
    {
        return SortPoints(points.Select(v => v.ToVector2()));
    }
}