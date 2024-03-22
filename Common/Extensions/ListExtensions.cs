using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static void AddIfNotNull<TSource>(this List<TSource> list, TSource value)
    {
        if (value != null)
            list.Add(value);
    }

    public static List<TSource> GetListWithoutNullObjects<TSource>(this List<TSource> list) where TSource : class
    {
        var result = new List<TSource>();
        foreach (var item in list)
        {
            if (item is GameObject gameObject)
            {
                if (gameObject == null) continue;
            }
            else if (item == null) continue;

            result.Add(item);
        }

        return result;
    }
}