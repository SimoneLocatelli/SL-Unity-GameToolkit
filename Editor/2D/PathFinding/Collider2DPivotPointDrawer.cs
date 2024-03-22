using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Collider2DPivotPoint))]
public class Collider2DPivotPointDrawer : BasePropertyDrawer
{
    public Vector3 PositionTarget;

    public TargetModes TargetMode;

    public Transform TransformTarget;

    protected override bool HideLabel => true;

    protected override void OnDrawProperty()
    {
        DrawHeader("Collider");

        IndentLevel++;

        DrawProperty(Collider2DPivotPoint.ColliderFieldName);

        IndentLevel--;
    }
}


