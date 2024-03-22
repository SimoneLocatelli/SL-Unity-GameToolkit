using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Provides common logic that helps following a
/// given path. This class should be reused by other
/// Behaviour scripts where possible. If a more complex logic is required,
/// consider creating a new class that inherits from this and overrides methods
/// such as <see cref="UpdateMovementVector"/>.
/// </summary>
public class PathFinderFollower
{
    #region Properties

    private readonly PathUpdater _pathUpdater;

    private float _minimumVicinityToNode = 0.25f;

    public bool CanSearch
    {
        get => _pathUpdater.CanSearch;
        set => _pathUpdater.CanSearch = value;
    }

    public Vector2 CurrentDirection { get; private set; }

    public bool HasPath { get => _pathUpdater.HasPath; }

    public bool HasReachedTarget { get; private set; }

    public bool HasTarget => _pathUpdater.HasTarget;

    public float MinimumVicinityToEndNode
    {
        get => _minimumVicinityToNode;
        set => _minimumVicinityToNode = Mathf.Max(0, value);
    }

    public float MinimumVicinityToNode
    {
        get => _minimumVicinityToNode;
        set => _minimumVicinityToNode = Mathf.Max(0, value);
    }

    public float Speed { get; }

    #endregion Properties

    #region Ctors

    public PathFinderFollower(PathUpdater pathUpdater)
    {
        Assert.IsNotNull(pathUpdater);

        _pathUpdater = pathUpdater;
        _pathUpdater.OnPathUpdated += OnPathOrTargetUpdated;
        _pathUpdater.PathTarget.OnTargetUpdated += OnPathOrTargetUpdated;
    }

    #endregion Ctors

    #region Methods

    public void ClearTarget()
        => _pathUpdater.ClearTarget();

    public void SetTarget(Transform transform)
        => _pathUpdater.PathTarget.SetTarget(transform);

    public void SetTarget(Vector3 position)
            => _pathUpdater.PathTarget.SetTarget(position);

    public void Update()
    {
        CurrentDirection = UpdateMovementVector();
    }

    internal void ClearPath()
        => _pathUpdater.ClearPath();

    private void OnPathOrTargetUpdated()
    {
        HasReachedTarget = false;

        if (_pathUpdater != null)
            _pathUpdater.ResetCurrentNodeIndex();
    }

    private Vector2 UpdateMovementVector()
    {
        if (!HasPath)
        {
            return Vector2.zero;
        }

        var path = _pathUpdater.Path;

        var nextPosition = path.GetNodeCenterPosition(path.CurrentNodeIndex);

        var currentPosition = _pathUpdater.GetColliderPivotPoint();

        if (!currentPosition.HasValue)
            return Vector2.zero;

        var distanceFromEndNode = currentPosition.Value.Distance(path.LastNodePosition);

        if (distanceFromEndNode <= MinimumVicinityToEndNode)
        {
            HasReachedTarget = true;
            return Vector2.zero;
        }

        var distanceFromNextPoint = currentPosition.Value.Distance(nextPosition);
        if (distanceFromNextPoint <= MinimumVicinityToNode)
        {
            HasReachedTarget = path.IsCurrentNodeLastNode;

            if (HasReachedTarget)
                return Vector2.zero;

            path.CurrentNodeIndex++;
            nextPosition = path.GetNodeCenterPosition(path.CurrentNodeIndex);
        }
        else
        {
            HasReachedTarget = false;
        }

        var movementVector = currentPosition.Value.GetDirectionNormalised(nextPosition);
        //Debug.Log($"Distance {distanceFromEndNode} - Movement {movementVector}");

        return movementVector;
    }
    #endregion Methods
}