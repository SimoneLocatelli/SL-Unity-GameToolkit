using UnityEngine;

/// <summary>
/// Minimal implementation of an agent following a path.
/// This can be used as a starting point to create new scripts
/// with a more advanced logic.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PathUpdater))]
public class SimplePathFollower : BaseBehaviour2D
{
    #region Properties

    public bool CanMove;

    [Min(0)]
    public float MinimumVicinityToEndNode = 1;

    public float Speed = 5f;

    private readonly BaseMovement2D movement2D = new BaseMovement2D();

    private PathFinderFollower _pathFinderFollower;

    public bool HasReachedTarget => PathFinderFollower.HasReachedTarget;

    public float MinDistanceFromTarget
    {
        get => PathFinderFollower.MinimumVicinityToNode;
        set => PathFinderFollower.MinimumVicinityToNode = value;
    }

    public PathFinderFollower PathFinderFollower
    {
        get
        {
            if (_pathFinderFollower != null)
                return _pathFinderFollower;

            _pathFinderFollower = new PathFinderFollower(GetComponent<PathUpdater>());

            return _pathFinderFollower;
        }
    }

    public VectorDirection Direction
    {
        get => movement2D.Movement.ToVectorDirection();
    }

    #endregion Properties

    #region LifeCycle

    private void FixedUpdate()
    {
        if (!CanMove || PathFinderFollower.HasReachedTarget)
            return;

        movement2D.UpdateMovement(_pathFinderFollower.CurrentDirection);
        movement2D.MoveTowardsTarget(Rb, Speed);
    }

    private void Update()
    {
        if (!CanMove) return;

        PathFinderFollower.MinimumVicinityToEndNode = MinimumVicinityToEndNode;
        PathFinderFollower.Update();
    }

    #endregion LifeCycle
}