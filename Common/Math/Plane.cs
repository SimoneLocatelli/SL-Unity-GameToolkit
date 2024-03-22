using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class Plane<T>
{
    //public T[,] BottomLeftQuadrant = new T[,] { };
    //public T[,] BottomRightQuadrant = new T[,] { };
    //public T[,] TopLeftQuadrant = new T[,] { };
    //public T[,] TopRightQuadrant = new T[,] { };

    public PointValueList<T> BottomLeftQuadrant = new PointValueList<T>(QuadrantLocations.BottomLeft);
    public PointValueList<T> BottomRightQuadrant = new PointValueList<T>(QuadrantLocations.BottomRight);
    public PointValueList<T> TopLeftQuadrant = new PointValueList<T>(QuadrantLocations.TopLeft);
    public PointValueList<T> TopRightQuadrant = new PointValueList<T>(QuadrantLocations.TopRight);
    public int MaxX { get => Mathf.Max(TopRightQuadrant.MaxX, BottomRightQuadrant.MaxX); }
    public int MinX { get => Mathf.Min(TopLeftQuadrant.MaxX, BottomLeftQuadrant.MaxX); }
    public int MaxY { get => Mathf.Max(TopRightQuadrant.MaxY, TopLeftQuadrant.MaxY); }
    public int MinY { get => Mathf.Min(BottomRightQuadrant.MinY, BottomLeftQuadrant.MinY); }

    public void PrintDebugLog()
    {
        if (!UnityEngine.Debug.isDebugBuild)
            return;
        PrintQudrantDebug(TopRightQuadrant);
        PrintQudrantDebug(BottomRightQuadrant);
        PrintQudrantDebug(BottomLeftQuadrant);
        PrintQudrantDebug(TopLeftQuadrant);
    }

    private void PrintQudrantDebug(PointValueList<T> quadrant)
    {
        UnityEngine.Debug.Log($" -- {quadrant.location} --");
        foreach (var c in quadrant)
            UnityEngine.Debug.Log($"{c.X} . {c.Y} . {c.Value}");
    }

    public readonly uint Size;

    internal void Clear()
    {
        BottomLeftQuadrant.Clear();
        BottomRightQuadrant.Clear();
        TopLeftQuadrant.Clear();
        TopRightQuadrant.Clear();
    }

    //public Dictionary<string, PointValue<T>> TopRightQuadrant;
    //public Dictionary<string, PointValue<T>> BottomRightQuadrant;
    //public Dictionary<string, PointValue<T>> TopLeftQuadrant;
    //public Dictionary<string, PointValue<T>> BottomLeftQuadrant;

    public Plane(uint size)
    {
        Size = size;
    }

    public Vector2Int Normalise(int x, int y)
    {
        //    x = (int)Mathf.Abs(x / Size) + 1;
        //    y = (int)Mathf.Abs(y / Size) + 1;
        x = (int)((x - Size) / Size + 1);
        y = (int)((y - Size) / Size + 1);
        return new Vector2Int(x, y);
    }

    public T this[int x, int y]
    {
        get
        {
            //var quadrantList = GetQuadrantList(x, y);
            //  var normalisedVect = Normalise(x, y);
            var quadrantList = GetQuadrantList(x, y);
            var quad = quadrantList[x, y];

            return quad == null ? default : quad.Value;
        }
        set
        {
            //var normalisedVect = Normalise(x, y);
            var quadrantList = GetQuadrantList(x, y);
            quadrantList[x, y] = new PointValue<T>(x, y, value);
        }
    }

    private PointValueList<T> GetQuadrantList(int x, int y)
    {
        return GetQuadrantArray(x, y);
    }

    private PointValueList<T> GetQuadrantArray(int x, int y)
    {
        return x > 0 ?
             y > 0 ? TopRightQuadrant : BottomRightQuadrant :
             y > 0 ? TopLeftQuadrant : BottomLeftQuadrant;
    }

    //public T this[int x, int y]
    //{
    //    get
    //    {
    //        var quadrant = GetQuadrant(x, y);
    //        var value = quadrant.Where(c => c.Key == $"{x}.{y}").Select(c => c.Value).FirstOrDefault().Value;
    //        return value != null ? value.Value : default;
    //    }
    //}

    //private Dictionary<string, PointValue<T>> GetQuadrant(int x, int y)
    //{
    //    if (x > 0)
    //        return y > 0 ? TopRightQuadrant : BottomRightQuadrant;

    //    return y > 0 ? TopLeftQuadrant : BottomLeftQuadrant;
    //}
}