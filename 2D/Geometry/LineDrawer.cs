using System;
using UnityEngine;

public static class LineDrawer
{
    //public static void DrawLine(Vector3 start, Vector3 end, Color color, float thickness = 0.2f, string sortingLayer = null)
    //{
    //    //var distance = start.Distance(end);
    //    //var angle = start.AngleBetweenPoints(end);
    //    var lr = CustomResources.InstantiatePrefab<LineRenderer>("Engine/2D/Geometry/Line_2", /*start, angle*/);
    //    lr.Initialise(color, thickness, sortingLayer);

    //    lr.SetPosition(0, start);
    //    lr.SetPosition(1, end);
    //}

    public static void AddPoint(this LineRenderer lr, Vector2 point)
    {
        var position = lr.positionCount;
        lr.positionCount = position + 1;
        lr.SetPosition(position, point);
    }

    public static void AddPoints(this LineRenderer lr, params Vector2[] points)
    {
        var index = lr.positionCount;
        var position = lr.positionCount;
        lr.positionCount = position + points.Length;
        foreach (var p in points)
        {
            lr.SetPosition(index, p);
            index++;
        }
    }

    public static void DrawLine(this LineRenderer lr, Vector3 start, Vector3 end, Color color, float thickness = 0.2f, string sortingLayer = null, int startIndex = 0)
    {
        //line.transform.localScale = new Vector3(distance, thickness, 0);
        //line.transform.position = start;
    }

    public static void Initialise(this LineRenderer lr, Color color, float thickness = 0.2f, string sortingLayer = null)
    {
        color = color.Update(a: 1);
        lr.startWidth = thickness;
        lr.endWidth = thickness;
        lr.startColor = color;
        lr.endColor = color;
        if (sortingLayer != null)
            lr.sortingLayerName = sortingLayer;
    }

    public static Tuple<Vector2, Vector2> GetLastTwoPoints(this LineRenderer lr)
    {
        var count = lr.positionCount;
        if (count < 2)
            return null;

        return new Tuple<Vector2, Vector2>(lr.GetPosition(count - 1), lr.GetPosition(count - 2));
    }

    public static void ResetPoints(this LineRenderer lr, Vector2[] points)
    {
        lr.positionCount = 0;
        lr.AddPoints(points);
    }

    public static void Sort(this LineRenderer lr)
    {
        var points = new Vector3[lr.positionCount];
        lr.GetPositions(points);
        var sortedPoints = PointsSorter.SortPoints(points).ToArray();
        lr.ResetPoints(sortedPoints);
    }

    public static Vector2? GetClosestPoint(this LineRenderer lr, Vector2 position)
    {
        var points = new Vector3[lr.positionCount];
        lr.GetPositions(points);
        return position.GetClosestVector(points.ToVector2Array()); ;
    }

    //public static void Sort(this LineRenderer lr, float maxRange)
    //{
    //    var lineRendererPosition = lr.transform.position;
    //    var points = new Vector3[lr.positionCount];
    //    lr.GetPositions(points);
    //    points = points.Where(p => lineRendererPosition.IsInRange(p, maxRange)).ToArray();
    //    var sortedPoints = PointsSorter.SortPoints(points).ToArray();
    //    lr.ResetPoints(sortedPoints);
    //}
}