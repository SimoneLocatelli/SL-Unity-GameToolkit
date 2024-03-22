using System;
using UnityEngine;
using UnityEngine.Assertions;

public class PathUpdater : MonoBehaviour
{
    #region Properties

    public const string Node2DGridPropertyName = nameof(_node2DGrid);

    public const string Collider2DPivotPointFieldName = nameof(_collider2DPivotPoint);

    public bool CanSearch;

    [SerializeField]
    public PathTarget PathTarget = new PathTarget();

    public float RefreshInterval = 1;

    public RefreshModes RefreshMode = RefreshModes.Automatic;

    [SerializeField]
    private Collider2D _collider;

    [SerializeField]
    private Collider2DPivotPoint _collider2DPivotPoint;

    [SerializeField]
    [InspectorName("Node 2D Grid")]
    private Node2DGridBehaviour _node2DGrid;

    [SerializeField, ReadOnlyProperty]
    private float _timeSinceLastPathRefresh;

    private AStarNode2DGrid AstarGrid;

    private bool isSearching = false;

    private PathTask pathTask;

    public enum RefreshModes
    {
        Manual,
        Automatic
    }

    public bool HasPath { get => Path != null; }

    public bool HasTarget => PathTarget != null && PathTarget.HasTarget;

    public Path Path
    {
        get => PathTask?.Path;
        //private set => PathTask = value;
    }

    public PathTask PathTask { get; private set; }

    private Collider2DPivotPoint Collider2DPivotPoint
    {
        get
        {
            if (_collider2DPivotPoint == null)
            {
                if (_collider == null)
                    _collider = GetComponent<Collider2D>();

                _collider2DPivotPoint = new Collider2DPivotPoint(_collider);
            }
            return _collider2DPivotPoint;
        }
    }

    private Node2DGridBehaviour Node2DGrid
    {
        get
        {
            if (_node2DGrid == null)
                _node2DGrid = GameObject.FindObjectOfType<Node2DGridBehaviour>();

            return _node2DGrid;
        }
        set => _node2DGrid = value;
    }

    internal void ClearTarget()
    {
        PathTarget.ClearTarget();
    }

    private bool IsRefreshTimerExpired() => _timeSinceLastPathRefresh > 0;

    #endregion Properties

    #region Events

    public delegate void OnPathUpdatedHandler();

    public event OnPathUpdatedHandler OnPathUpdated;

    #endregion Events

    #region LifeCycle

    internal void ResetCurrentNodeIndex()
    {
        if (HasPath)
            Path.CurrentNodeIndex = 0;
    }

    private void Awake()
    {
        PathTarget.OnTargetUpdated += OnTargetUpdated;
    }

    private void OnDestroy()
    {
        PathTarget.OnTargetUpdated -= OnTargetUpdated;
    }

    private void OnDrawGizmos()
    {
        Collider2DPivotPoint.DrawGizmos();
    }

    private void Refresh()
    {
        if (RefreshMode != RefreshModes.Manual)
            return;

        Refresh();
    }

    private void RefreshInternal()
    {
        if (!CanSearch || isSearching) return;

        var targetPosition = PathTarget.GetCurrentTargetPosition();

        if (!targetPosition.HasValue) return;

        isSearching = true;

        var startPosition = GetColliderPivotPoint();

        if (!startPosition.HasValue) return;

        UpdatePath(startPosition.Value, targetPosition.Value);
    }

    private void Reset()
    {
        Node2DGrid = null;
        GameObject.FindObjectOfType<Node2DGridBehaviour>();
        Assert.IsNotNull(Node2DGrid);
        RefreshMode = RefreshModes.Automatic;
    }

    private void Start()
    {
        Assert.IsNotNull(Node2DGrid);

        _timeSinceLastPathRefresh = RefreshInterval;
    }

    private void Update()
    {
        if (RefreshMode == RefreshModes.Manual) return;

        var shouldRefresh = UpdateRefreshTimer();

        if (shouldRefresh)
            RefreshInternal();
    }

    private bool UpdateRefreshTimer()
    {
        if (!CanSearch || isSearching)
            return false;

        _timeSinceLastPathRefresh -= Time.deltaTime;

        if (IsRefreshTimerExpired())
            return false;

        _timeSinceLastPathRefresh = RefreshInterval;

        return true;
    }

    #endregion LifeCycle

    #region Event Handlers

    private void OnTargetUpdated()
    {
        ClearPath();
        RefreshInternal();
    }

    #endregion Event Handlers

    #region Methods

    public Vector3? GetColliderPivotPoint() => Collider2DPivotPoint.ColliderPivotPoint;

    public void SetPositionTarget(Vector3 position)
        => PathTarget.SetTarget(position);

    public void SetTransformTarget(Transform transform)
        => PathTarget.SetTarget(transform);

    internal void ClearPath()
        => PathTask = null;

    private void UpdatePath(Vector2 startPosition, Vector2 endPosition)
    {
        try
        {
            var startNode = Node2DGrid.GetNodeFromWorldPosition(startPosition);
            var endNode = Node2DGrid.GetNodeFromWorldPosition(endPosition);

            if (startNode == null || endNode == null)
                return;

            if (AstarGrid == null)
                AstarGrid = new AStarNode2DGrid(Node2DGrid.Grid);

            PathTask = AStarPathFinding2D.Find(AstarGrid, startNode, endNode);
            OnPathUpdated?.Invoke();
        }
        catch (Exception ex)
        {
            Debug.LogException(ex, this);
        }
        finally
        {
            isSearching = false;
        }
    }

    #endregion Methods
}