using UnityEngine;
using UnityEngine.Assertions;

public static class CustomResources
{
    public static GameObject InstantiatePrefab(string path, Vector3 position)
        => InstantiatePrefab(path, position, Quaternion.identity);

    public static GameObject InstantiatePrefab(string path, Vector3 position, float zAngle)
        => InstantiatePrefab(path, position, GetQuaternion(zAngle));

    public static GameObject InstantiatePrefab(string path, Vector3 position, Quaternion quaternion)
        => (GameObject)Object.Instantiate(Load(path), position, quaternion);

    public static GameObject Load(string path)
        => Load<GameObject>(path);

    public static T Load<T>(string path) where T : Object
    {
        var resource = Resources.Load<T>(path);
        Assert.IsNotNull(resource, $"Resource '{path}' not found");
        return resource;
    }

    public static T[] LoadAll<T>(string path) where T : Object
    {
        var resource = Resources.LoadAll<T>(path);
        Assert.IsTrue(resource != null && resource.Length > 0, $"Resource '{path}' not found");
        return resource;
    }

    public static Sprite LoadSprite(string path, int index)
    {
        CustomAssert.IsNotNegative(index, nameof(index));
        var resources = LoadAll<Sprite>(path);
        return (Sprite)resources[index];
    }

    public static Sprite LoadSprite(string path)
        => (Sprite)Load<Sprite>(path);

    internal static GameObject Instantiate(GameObject prefab, Vector3 position)
        => Object.Instantiate(prefab, position, Quaternion.identity);

    internal static GameObject Instantiate(GameObject prefab, Transform transform)
        => Object.Instantiate(prefab, transform.position, Quaternion.identity);

    internal static TComponent Instantiate<TComponent>(GameObject prefab, Vector3 position) where TComponent : Component
        => Object.Instantiate(prefab, position, Quaternion.identity).GetComponent<TComponent>();

    internal static TComponent Instantiate<TComponent>(GameObject prefab, Transform transform) where TComponent : Component
            => Object.Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<TComponent>();

    internal static GameObject Instantiate(GameObject prefab, Vector3 position, Transform parent)
            => Object.Instantiate(prefab, position, Quaternion.identity, parent);

    internal static GameObject Instantiate(GameObject prefab, Vector3 position, float zAngle)
        => Object.Instantiate(prefab, position, GetQuaternion(zAngle));

    internal static TComponent InstantiatePrefab<TComponent>(string path, Vector3 start, float angle)
        where TComponent : Component
    {
        var obj = InstantiatePrefab(path, start, angle);
        return obj == null ? null : obj.GetComponent<TComponent>();
    }

    internal static TComponent InstantiatePrefab<TComponent>(string path, Vector3 start, Transform parent)
        where TComponent : Component
    {
        var obj = InstantiatePrefab(path, start);

        if (obj == null) return null;

        obj.transform.parent = parent;
        obj.transform.localPosition = start;
        return obj.GetComponent<TComponent>();
    }

    internal static TComponent InstantiatePrefab<TComponent>(string path, Vector3 start) where TComponent : Component
    {
        var obj = InstantiatePrefab(path, start);
        return obj == null ? null : obj.GetComponent<TComponent>();
    }

    private static Quaternion GetQuaternion(float zAngle)
     => Quaternion.Euler(0, 0, zAngle);
}