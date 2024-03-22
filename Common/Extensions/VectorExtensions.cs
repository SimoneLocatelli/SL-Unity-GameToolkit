using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class VectorExtensions
{
    public static float AngleBetweenPoints(this Vector3 vectorA, Vector3 vectorB)
    {
        var direction = vectorA.GetDirection(vectorB);
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180;// + 90;// - 90;
    }

    public static float Distance(this Vector3 vectorA, Vector3 vectorB) => Vector3.Distance(vectorA, vectorB);

    public static float Distance(this Vector2 vectorA, Vector2 vectorB) => Vector3.Distance(vectorA, vectorB);

    public static TBehaviour GetClosestComponent<TBehaviour>(this Vector3 position, IEnumerable<TBehaviour> components, float maxRange = float.MaxValue)
        where TBehaviour : MonoBehaviour
    {
        var closestObject = position.GetClosestObject(components.Select(o => o.gameObject), maxRange);

        if (closestObject == null)
            return null;

        return components.FirstOrDefault(o => o.gameObject == closestObject);
    }

    public static TBehaviour GetClosestComponent<TBehaviour>(this Vector3 position, IEnumerable<GameObject> objects, float maxRange = float.MaxValue)
         where TBehaviour : MonoBehaviour
    {
        var closestObject = position.GetClosestObject(objects, maxRange);

        if (closestObject == null)
            return null;

        return closestObject.GetComponent<TBehaviour>();
    }

    public static GameObject GetClosestObject(this Vector3 position, IEnumerable<GameObject> objects)
    {
        if (objects == null || !objects.Any())
            return null;

        GameObject closestObject = null;
        float closestDistance = float.MaxValue;
        foreach (var obj in objects)
        {
            if (obj == null) continue;
            var distance = position.Distance(obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = position.Distance(obj.transform.position);
                closestObject = obj;
            }
        }

        return closestObject;
    }

    public static GameObject GetClosestObject(this Vector3 position, IEnumerable<GameObject> objects, float radius = float.MaxValue)
    {
        var closestObject = GetClosestObject(position, objects);

        return closestObject != null && position.IsInRange(closestObject.transform.position, radius) ? closestObject : null;
    }

    public static GameObject GetClosestObject(this Vector3 position, IEnumerable<Vector3> vectors, float radius = float.MaxValue)
    {
        var closestObject = GetClosestObject(position, vectors);

        return closestObject != null && position.IsInRange(closestObject.transform.position, radius) ? closestObject : null;
    }

    public static Vector3? GetClosestVector(this Vector3 position, IEnumerable<Vector3> vectors)
    {
        Vector3? closestObject = null;
        float closestDistance = float.MaxValue;
        foreach (var v in vectors)
        {
            if (v == null) continue;
            var distance = position.Distance(v);
            if (distance < closestDistance)
            {
                closestDistance = position.Distance(v);
                closestObject = v;
            }
        }

        return closestObject;
    }

    public static Vector2? GetClosestVector(this Vector2 position, IEnumerable<Vector2> vectors)
    {
        Vector2? closestObject = null;
        float closestDistance = float.MaxValue;
        foreach (var v in vectors)
        {
            if (v == null) continue;
            var distance = position.Distance(v);
            if (distance < closestDistance)
            {
                closestDistance = position.Distance(v);
                closestObject = v;
            }
        }

        return closestObject;
    }

    public static Vector3 GetDirection(this Vector3 startingPoint, Vector3 terminalPoint)
        => terminalPoint - startingPoint;

    public static Vector2 GetDirection(this Vector2 startingPoint, Vector2 terminalPoint)
        => terminalPoint - startingPoint;

    public static Vector3 GetDirectionNormalised(this Vector3 startingPoint, Vector3 terminalPoint)
        => startingPoint.GetDirection(terminalPoint).normalized;

    public static Vector2 GetDirectionNormalised(this Vector2 startingPoint, Vector2 terminalPoint)
        => startingPoint.GetDirection(terminalPoint).normalized;

    public static Vector2 GetRandomPositionInRange(this Vector2 position, float radius) => position + Random.insideUnitCircle * radius;

    public static Vector2 GetRandomPositionInRange(this Vector3 position, float radius) => (Vector2)position + Random.insideUnitCircle * radius;

    public static bool IsInRange(this Vector3 vectorA, Vector3 vectorB, float range)
        => Vector3.Distance(vectorA, vectorB) <= range;

    public static bool IsInRange(this Vector2 vectorA, Vector2 vectorB, float range)
        => vectorA.ToVector3().IsInRange(vectorB, range);

    public static bool IsOutsideRange(this Vector3 vectorA, Vector3 vectorB, float range)
            => Vector3.Distance(vectorA, vectorB) > range;

    public static bool IsOutsideRange(this Vector2 vectorA, Vector2 vectorB, float range)
        => Vector3.Distance(vectorA, vectorB) > range;

    public static bool IsZero(this Vector2 v) => v == Vector2.zero;

    public static Vector3 MoveTowards(this Vector3 source, GameObject gameObject, float speed)
        => source.MoveTowards(gameObject.transform.position, speed);

    public static Vector3 MoveTowards(this Vector3 source, Transform transform, float speed)
        => source.MoveTowards(transform.position, speed);

    public static Vector3 MoveTowards(this Vector3 source, Vector3 target, float speed)
        => Vector3.MoveTowards(source, target, speed * Time.deltaTime);

    public static Vector3 MoveTowards(this Vector3 source, Vector3 target, float speed, float time)
        => Vector3.MoveTowards(source, target, speed * time);

    public static Vector2 ToVector2(this Vector3 v) => v;

    public static Vector2 ToVector2(this Vector2Int v) => new Vector2(v.x, v.y);

    public static Vector2[] ToVector2Array(this Vector3[] vectors)
    {
        return vectors.Select(v => v.ToVector2()).ToArray();
    }

    public static Vector2Int ToVector2Int(this Vector2 v)
            => new Vector2Int((int)v.x, (int)v.y);

    public static Vector2Int ToVector2Int(this Vector3 v)
        => new Vector2Int((int)v.x, (int)v.y);

    public static Vector3 ToVector3(this Vector2 vector) => vector;

    public static Vector3 ToVector3(this Vector2Int vector) => new Vector3(vector.x, vector.y);

    public static Vector3Int ToVector3Int(this Vector3 v)
        => new Vector3Int((int)v.x, (int)v.y, (int)v.z);

    public static Vector3 Update(this Vector3 vector, float? x = null, float? y = null, float? z = null)
    {
        var newX = x.GetValueOrDefault(vector.x);
        var newY = y.GetValueOrDefault(vector.y);
        var newZ = z.GetValueOrDefault(vector.z);

        return new Vector3(newX, newY, newZ);
    }

    public static Vector3 Add(this Vector3 vector, float xDelta = 0, float yDelta = 0, float zDelta = 0)
    {
        var newX = vector.x + xDelta;
        var newY = vector.y + yDelta;
        var newZ = vector.z + zDelta;

        return new Vector3(newX, newY, newZ);
    }

    public static Vector2 Add(this Vector2 vector, float xDelta = 0, float yDelta = 0)
    {
        var newX = vector.x + xDelta;
        var newY = vector.y + yDelta;

        return new Vector2(newX, newY);
    }

    public static Vector2 Update(this Vector2 vector, float? x = null, float? y = null)
    {
        var newX = x.GetValueOrDefault(vector.x);
        var newY = y.GetValueOrDefault(vector.y);

        return new Vector2(newX, newY);
    }
}