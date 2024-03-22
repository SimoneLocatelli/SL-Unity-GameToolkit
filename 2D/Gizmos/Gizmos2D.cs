using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Gizmos2D
{
    public static void DrawClosedPolygon(params Vector3[] points)
    {
        var lastPointIndex = points.Length - 1;

        for (int i = 0; i < lastPointIndex; i++)
        {
            Gizmos.DrawLine(points[i], points[i + 1]);
        }

        Gizmos.DrawLine(points[lastPointIndex], points[0]);
    }

    public static void DrawOpenPolygon(params Vector3[] points)
    {
        var lastPointIndex = points.Length - 1;

        for (int i = 0; i < lastPointIndex; i++)
        {
            Gizmos.DrawLine(points[i], points[i + 1]);
        }
    }

    public static void DrawClosedPolygon(List<Vector3> points)
    {
        var lastPointIndex = points.Count - 1;

        for (int i = 0; i < lastPointIndex; i++)
        {
            Gizmos.DrawLine(points[i], points[i + 1]);
        }

        Gizmos.DrawLine(points[lastPointIndex], points[0]);
    }

    public static void DrawOpenPolygon(List<Vector3> points)
    {
        var lastPointIndex = points.Count - 1;

        for (int i = 0; i < lastPointIndex; i++)
        {
            Gizmos.DrawLine(points[i], points[i + 1]);
        }
    }


    public static void DrawRect(Vector3 topLeft, Vector3 topRight, Vector3 bottomRight, Vector3 bottomLeft)
    {
        DrawClosedPolygon(topLeft, topRight, bottomRight, bottomLeft);
    }

    internal static void DrawRect(Bounds bounds)
    {
        var min = bounds.min;
        var max = bounds.max;
        var topLeft = new Vector3(min.x, max.y);
        var topRight = new Vector3(max.x, max.y);
        var bottomRight = new Vector3(max.x, min.y);
        var bottomLeft = new Vector3(min.x, min.y);

        DrawClosedPolygon(topLeft, topRight, bottomRight, bottomLeft);
    }
}