using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ComponentExtensions
{
    public static T GetComponentFromObjectOrParentOrChildren<T>(this Component component) where T : Component
    {
        var componentResult = component.GetComponent<T>() ?? component.GetComponentInParent<T>();
        return componentResult ?? component.GetComponentInChildren<T>();
    }

    public static GameObject[] SelectGameObjects(this IEnumerable<Component> components)
    {
        if (components == null || !components.Any())
            return new GameObject[0];

        return components.Select(c => c.gameObject).ToArray();
    }

    /// <summary>
    /// Destroys the object to which the <paramref name="component"/> is attached to.
    /// </summary>
    /// <param name="component">The component attached to the object to destroy.</param>
    public static void Destroy(this Component component)
    {
        if (component == null)
            return;

        if (component.gameObject != null)
            Object.Destroy(component.gameObject);
    }

    /// <summary>
    /// Destroys the object to which the <paramref name="component"/> is attached to.
    /// Use <see cref="DestroyImmediate(Component)"/> if you need to run it in the editor otherwise use <see cref="Destroy(Component)"/>
    /// </summary>
    /// <param name="component">The component attached to the object to destroy.</param>
    public static void DestroyImmediate(this Component component)
    {
        if (component == null)
            return;

        if (component.gameObject != null)
            Object.DestroyImmediate(component.gameObject);
    }

    /// <summary>
    /// Remove a <paramref name="component"/> from the object it is attached to.
    /// </summary>
    /// <param name="component">The component to remove from the object.</param>
    public static void RemoveComponentImmediate(this Component component)
    {
        if (component == null)
            return;

        if (component.gameObject != null)
            Object.DestroyImmediate(component);
    }
}