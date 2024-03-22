using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Multitags : MonoBehaviour
{
    [TagSelector]
    public List<string> Tags;

    internal bool ContainsAny(List<string> tags)
    {
        foreach (var tag in tags)
        {
            if (string.IsNullOrWhiteSpace(tag))
                continue;

            if (this.Tags.Contains(tag))
                return true;
        }

        return false;
    }

    public bool Contains(string tag)
        => Tags.Contains(tag);

    public bool Contains(IEnumerable<string> tags)
        => tags.All(t => Tags.Contains(t));
}