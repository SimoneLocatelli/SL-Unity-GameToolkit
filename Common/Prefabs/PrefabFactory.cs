using UnityEngine;

public static class PrefabFactory
{
    public static TPrefab Create<TPrefab>(string prefabPath, Vector3 position, Transform parent = null) where TPrefab : Component
    {
        var transform = Create(prefabPath, position, parent);
        return transform.GetComponent<TPrefab>();
    }

    public static GameObject Create(string prefabPath, Vector3 position, Transform parent = null)
    {
        var instance = Create(prefabPath, parent);
        if (parent == null)
            instance.transform.position = position;
        else
            instance.transform.localPosition = position;

        return instance;
    }

    public static GameObject Create(string prefabPath, Transform parent)
    {
        var obj = Resources.Load(prefabPath) as GameObject;
        var instance = parent == null ?
            Object.Instantiate(obj) :
            Object.Instantiate(obj, parent);

        return instance;
    }
}