using System;
using UnityEngine;

/// <summary>
/// Holds the information about the target (as in position in the world or transform)
/// that an agent (path followers) is trying to reach.
/// </summary>
[Serializable]
public class PathTarget
{
    #region Properties

    public Vector3 PositionTarget;

    public TargetModes TargetMode;

    public Transform TransformTarget;

    public bool HasTarget
    {
        get
        {
            if (TargetMode == TargetModes.NoTarget)
                return false;

            if (TargetMode == TargetModes.Vector)
                return true;

            return TransformTarget != null;
        }
    }

    #endregion Properties

    #region Events

    public delegate void OnTargetUpdatedHandler();

    public event OnTargetUpdatedHandler OnTargetUpdated;

    #endregion Events

    #region Methods

    public void ClearTarget()
    {
        TargetMode = TargetModes.NoTarget;
        //TransformTarget = null;
        //PositionTarget = Vector3.zero;
        OnTargetUpdated?.Invoke();
    }

    public bool SetTarget(Vector3 position)
    {
        if (TargetMode == TargetModes.Vector && PositionTarget == position)
            return false;

        PositionTarget = position;
        TargetMode = TargetModes.Vector;

        OnTargetUpdated?.Invoke();

        return true;
    }

    public bool SetTarget(Transform transform)
    {
        if (TargetMode == TargetModes.Transform && TransformTarget == transform)
            return false;

        TransformTarget = transform;
        TargetMode = TargetModes.Transform;

        OnTargetUpdated?.Invoke();

        return true;
    }

    internal Vector3? GetCurrentTargetPosition()
    {
        switch (TargetMode)
        {
            case TargetModes.NoTarget:
                return null;

            case TargetModes.Transform:
                return TransformTarget == null ? null : TransformTarget.position;

            case TargetModes.Vector:
                return PositionTarget;

            default: throw new NotImplementedException();
        }
    }

    #endregion Methods
}