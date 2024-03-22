using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TagsFinder
{
    public static GameObject[] FindObjectsWithTag(string tag)
    {
        if (string.IsNullOrWhiteSpace(tag))
            return new GameObject[0];

        var multitagsObjects = GameObject.FindObjectsOfType<Multitags>();

        return multitagsObjects.Where(m => m != null && m.gameObject != null && m.Contains(tag)).Select(m => m.gameObject).ToArray();
    }

    public static GameObject[] FindObjectsWithTag(string tag, Vector3 rangeCentre, float radius)
    {
        return FindObjectsWithTag(tag).Where(o => rangeCentre.Distance(o.transform.position) <= radius).ToArray();
    }

    internal static GameObject[] FindObjectsWithAnyTags(List<string> tags)
    {
        if (tags == null || !tags.Any())
            return new GameObject[0];

        var multitagsObjects = GameObject.FindObjectsOfType<Multitags>();

        return multitagsObjects.SelectObjectsWithAnyTag(tags).ToArray();
    }

    internal static GameObject[] FindObjectsWithAnyTags(List<string> tags, Vector3 rangeCentre, float radius)
    {
        return FindObjectsWithAnyTags(tags).Where(o => rangeCentre.Distance(o.transform.position) <= radius).ToArray();
    }

    public static IEnumerable<GameObject> SelectObjectsWithAnyTag(this IEnumerable<Multitags> multitags, List<string> tags)
    {
        return multitags.Where(m => m != null && m.gameObject != null && m.ContainsAny(tags)).Select(m => m.gameObject);
    }

    public static IEnumerable<GameObject> SelectObjectsWithAnyTag(this IEnumerable<GameObject> objects, List<string> tags)
    {
        foreach (var m in objects)
        {
            if (m == null) continue;

            var multitags = m.GetComponent<Multitags>();
            if (multitags == null) continue;

            if (multitags != null && multitags.ContainsAny(tags))
                yield return m;
        }
    }

    public static TComponent[] SelectWithAnyTag<TComponent>(this IEnumerable<Multitags> multitags, List<string> tags)
        where TComponent : MonoBehaviour
    {
        return multitags.Where(m => m != null && m.gameObject != null && m.ContainsAny(tags)).Select(c => c.GetComponent<TComponent>()).ToArray();
    }

    public static Multitags[] GetMultitagsComponents(IEnumerable<GameObject> objs)
    {
        return objs.Select(c => c.GetComponent<Multitags>()).Where(c => c != null).ToArray();
    }
}