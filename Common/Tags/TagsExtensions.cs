using System.Collections.Generic;
using UnityEngine;

public static class TagsExtensions
{

    /// <summary>
    /// Retrieves the <see cref="Multitags"/> component from <paramref name="obj"/> and checks
    /// if it contains all the tags in <paramref name="tags"/>.
    /// </summary>
    /// <param name="obj">The Unity object with the Multitags component</param>
    /// <param name="tags">List of tags to search.</param>
    /// <returns>Returns true if the object is not null, it has a <see cref="Multitags"/> component and it contains all the tags in <paramref name="tags"/>.</returns>
    public static bool HasAllTags(this GameObject obj, params string[] tags)
    {
        var multitags = obj.GetMultitagsComponent();

        if (multitags == null)
            return false;

        return multitags.Contains(tags);
    }

    /// <summary>
    /// Retrieves the <see cref="Multitags"/> component from <paramref name="obj"/> and checks
    /// if it contains at least one of the tags in <paramref name="tags"/>.
    /// </summary>
    /// <param name="obj">The Unity object with the Multitags component</param>
    /// <param name="tags">List of tags to search.</param>
    /// <returns>Returns true if the object is not null, it has a <see cref="Multitags"/> component and it contains any of the tags in <paramref name="tags"/>.</returns>
    public static bool HasAnyTag(this GameObject obj, List<string> tags)
    {
        var multitags = obj.GetMultitagsComponent();

        if (multitags == null)
            return false;

        return multitags.ContainsAny(tags);
    }

    /// <summary>
    /// Retrieves the <see cref="Multitags"/> component from <paramref name="obj"/> and checks
    /// if it contains the <paramref name="tag"/> tag.
    /// </summary>
    /// <param name="obj">The Unity object with the Multitags component.</param>
    /// <param name="tag">The tag to search.</param>
    /// <returns>Returns true if the object is not null, it has a <see cref="Multitags"/> component and it contains the tag.</returns>
    public static bool HasTag(this GameObject obj, string tag)
    {
        var multitags = obj.GetMultitagsComponent();

        if (multitags == null)
            return false;

        return multitags.Contains(tag);
    }

    private static Multitags GetMultitagsComponent(this GameObject obj)
    {
        if (obj == null)
            return null;

        var multitags = obj.GetComponent<Multitags>();

        return multitags;
    }
}