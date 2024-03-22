using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class BehaviourUtils
{
    public static float Distance(Behaviour behaviour, Vector2 vector) => Vector2.Distance(behaviour.transform.position, vector);

    public static TSource GetComponentFromCollision<TSource>(Collision2D collision) where TSource : Component
    {
        if (collision == null || collision.gameObject == null) return null;
        return collision.gameObject.GetComponent<TSource>();
    }

    public static Vector2 GetDirectionNormalised(Behaviour behaviour, Vector2 target) => behaviour.transform.position.GetDirectionNormalised(target);

    public static TComponent GetInitialisedComponent<TComponent>(Behaviour behaviour, ref TComponent innerReference) where TComponent : Component
    {
        if (innerReference == null)
            innerReference = behaviour.GetComponent<TComponent>();

        return innerReference;
    }

    public static TComponent GetInitialisedObject<TComponent>(string name, ref TComponent innerReference) where TComponent : Component
    {
        if (innerReference == null)
            innerReference = GameObject.Find(name).GetComponent<TComponent>();

        return innerReference;
    }

    public static List<TComponent> GetInitialisedObjects<TComponent>(ref List<TComponent> innerReference) where TComponent : Component
    {
        if (innerReference == null)
            innerReference = GameObject.FindObjectsOfType<TComponent>().ToList();

        return innerReference;
    }

    public static void MoveTowards(Behaviour behaviour, Vector3 target, float speed)
    {
        behaviour.transform.position = Vector3.MoveTowards(behaviour.transform.position, target, speed * Time.deltaTime);
    }

    public static void UpdateDebugText(Behaviour behaviour, string value)
    {
        var debugTextObject = behaviour.transform.Find("DebugText");
        if (debugTextObject != null)
        {
            var textMesh = behaviour.transform.Find("DebugText").GetComponent<TextMesh>();
            textMesh.text = value;
        }
    }
}