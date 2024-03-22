using UnityEngine;
using UnityEngine.Assertions;

public static class TransformExtensions
{
    public static GameObject GetRootObject(this Transform transform)
    {
        Assert.IsNotNull("Transform is null!");

        if (transform.parent == null)
            return transform.gameObject;

        return GetRootObject(transform.parent);
    }

    public static void RotateZAngle(this Transform transform, float zAngle)
    {
        transform.Rotate(0, 0, zAngle);
    }
}