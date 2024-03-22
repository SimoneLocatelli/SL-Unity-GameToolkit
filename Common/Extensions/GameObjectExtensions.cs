using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameObjectExtensions
{
    public static T AddBehaviourIfNotPresent<T>(this GameObject gameObject, bool enabled) where T : Behaviour
    {
        T component;
        if (!gameObject.TryGetComponent(out component))
            component = gameObject.AddComponent<T>();

        component.enabled = enabled;

        return component;
    }

    public static T AddComponentIfNotPresent<T>(this GameObject gameObject) where T : Component
    {
        if (gameObject.TryGetComponent(out T component))
            return component;

        return gameObject.AddComponent<T>();
    }

    public static T AddComponentIfNotPresent<T>(this GameObject gameObject, Action<T> componentInitialiser) where T : Component
    {
        var c = gameObject.AddComponentIfNotPresent<T>();
        componentInitialiser?.Invoke(c);
        return c;
    }

    public static void Destroy(this GameObject gameObject)
        => GameObject.Destroy(gameObject);

    public static void DestroyImmediate(this GameObject gameObject)
        => GameObject.DestroyImmediate(gameObject);

    public static void DestroyImmediateAllChildren(this GameObject gameObject)
    {
        const int maxAttempts = 10;

        for (int i = 0; i < maxAttempts; i++)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (gameObject == child.gameObject)
                {
                    Debug.Log("SAME");
                }
                else
                {
                    GameObject.DestroyImmediate(child.gameObject);
                    //child.parent = null;
                }
            }
        }
    }

    public static IEnumerable<GameObject> GetAllChildren(this GameObject gameObject)
    {
        return gameObject.GetComponentsInChildren<Transform>().Select(c => c.gameObject).Distinct().ToList();
    }

    public static TBehaviour GetClosestComponent<TBehaviour>(this GameObject obj,
        float maxRange = float.MaxValue,
        Func<TBehaviour, bool> predicate = null)
        where TBehaviour : MonoBehaviour
    {
        var objs = GameObject.FindObjectsOfType<TBehaviour>();

        if (predicate != null)
            objs = objs.Where(predicate).ToArray();
        return obj.transform.position.GetClosestComponent<TBehaviour>(objs, maxRange);
    }

    public static GameObject GetTopParent(this GameObject gameObject)
    {
        var parent = GetTopTransform(gameObject.transform);

        return parent == null ? null : parent.gameObject;
    }

    public static Transform GetTopTransform(this Transform transform)
    {
        var parent = transform.parent;
        return parent == null ? transform : GetTopTransform(parent);
    }

    public static void RemoveComponentIfPresent<T>(this GameObject gameObject) where T : Component
    {
        if (!gameObject.TryGetComponent(out T component))
            return;

        GameObject.Destroy(component);
    }

    public static TAdd ReplaceComponent<TRemove, TAdd>(this GameObject gameObject, bool enabled = true)
                        where TRemove : MonoBehaviour
        where TAdd : MonoBehaviour

    {
        gameObject.RemoveComponentIfPresent<TRemove>();
        return gameObject.AddBehaviourIfNotPresent<TAdd>(enabled);
    }

    public static IEnumerable<TComponent> SelectComponents<TComponent>(this IEnumerable<GameObject> gameObject)
                        where TComponent : MonoBehaviour
                        => gameObject.Select(o => o.GetComponent<TComponent>());
}