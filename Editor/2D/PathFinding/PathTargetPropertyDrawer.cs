using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(PathTarget))]
public class PathTargetPropertyDrawer : BasePropertyDrawer
{
    public Vector3 PositionTarget;

    public TargetModes TargetMode;

    public Transform TransformTarget;

    protected override bool HideLabel => true;

    protected override void OnDrawProperty()
    {
        IndentLevel++;
        TargetMode = DrawEnumProperty<TargetModes>(nameof(PathTarget.TargetMode));

        if (TargetMode == TargetModes.Transform)
            DrawProperty(nameof(PathTarget.TransformTarget));
        else if (TargetMode == TargetModes.Vector)
            DrawProperty(nameof(PathTarget.PositionTarget));

        IndentLevel--;
    }
}