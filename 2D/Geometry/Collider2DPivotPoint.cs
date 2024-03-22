using System;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
public class Collider2DPivotPoint
{
    public const string ColliderFieldName = nameof(_collider);

    [SerializeField]
    private Collider2D _collider;

    public Vector3? ColliderPivotPoint
    {
        get
        {
            if (_collider == null)
                return null;

            var bounds = _collider.bounds;

            if (bounds == null)
                return null;

            return bounds.center;
        }
    }

    public Collider2DPivotPoint(Collider2D collider)
    {
        Assert.IsNotNull(collider);

        _collider = collider;
    }

    public void DrawGizmos()
    {
        if (_collider == null) return;

        Gizmos.color = Color.cyan;
        Gizmos2D.DrawRect(_collider.bounds);
    }
}