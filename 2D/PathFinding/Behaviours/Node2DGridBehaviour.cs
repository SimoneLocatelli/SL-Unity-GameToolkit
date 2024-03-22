using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines a grid of <see cref="Node2D"/> within
/// a scene. The main purpose is just to expose the properties
/// of the <see cref="Node2DGrid" /> instance held by this script.
/// </summary>
[ExecuteInEditMode]
public class Node2DGridBehaviour : MonoBehaviour
{
    #region Editor Properties

    #region Grid Parameters

    [Range(0.20f, 1)]
    [SerializeField]
    private float _CollisionDiameterPercentage = 1;

    [Header("Grid Parameters")]
    [Min(0.25f)]
    public float CellSize = 1f;

    public LayerMask CollisionMask;

    [Range(0.02f, 10f)]
    public float RefreshRate = 0.02f;

    #endregion Grid Parameters

    #region Gizmos

    [Header("Gizmos")]
    [SerializeField, InspectorName("Draw Grid")]
    private bool GizmosCanDrawGrid = true;

    [SerializeField, InspectorName("Draw Blocked Cells")]
    private bool GizmosCanDrawBlockedCells = true;

    [SerializeField, InspectorName("Draw Collision ranges")]
    private bool DrawCollisionsGizmos = true;

    #endregion Gizmos

    #region Edit Mode

    [Header("Edit Mode")]
    public bool EquallyDivideGrid;

    public bool TrimGrid;

    #endregion Edit Mode

    #endregion Editor Properties

    #region Properties

    private float _cachedCellSize;

    private int _cachedColumns;

    private int _cachedRows;

    private Node2DGrid _grid;

    private List<Node2D> _unwalkableNodes;

    private float elapsedTime;

    private Color ObstacleColor = new Color(1, 0, 0, 0.5f);

    private Color WalkableColor = new Color(1, 1, 1, 0.5f);

    public Vector2 CellSizeVector { get => new Vector2(CellSize, CellSize); }

    public float CollisionRadiusPercentage { get => _CollisionDiameterPercentage / 2; }

    public int Columns { get => (int)(transform.localScale.x / CellSize); }

    public Node2DGrid Grid
    {
        get
        {
            if (_grid == null)
                RebuildArray();

            return _grid;
        }
    }

    public float HalfCellSize { get => CellSize / 2; }

    public Vector3 Offset { get => transform.position; }

    public Vector2 Offset2D { get => transform.position.ToVector2(); }

    public int Rows { get => (int)(transform.localScale.y / CellSize); }

    #endregion Properties

    #region Methods

    private void DoEquallyDivideGrid()
    {
        var localScale = transform.localScale;
        var divider = (int)Mathf.Min(localScale.x, localScale.y, CellSize);

        CellSize = FindMaxDivider.FindLargestInt(divider, localScale.x, localScale.y);
        EquallyDivideGrid = false;
    }

    private void DoTrimGrid()
    {
        transform.position = transform.position.ToVector3Int();
        transform.localScale = transform.localScale.ToVector3Int();
        TrimGrid = false;
        Debug.Log("Grid Trimmed", this);
    }

    private void GizmoDrawColumns(float width, float height)
    {
        var from = new Vector3(0, 0);
        var to = new Vector3(0, height);

        for (float x = 0; x <= width; x += CellSize)
        {
            from.x = x;
            to.x = x;
            Gizmos.DrawLine(Offset + from, Offset + to);
        }

        from.y += CellSize;
        to.y += CellSize;
        //Gizmos.DrawLine(Offset + from, Offset + to);
    }

    private void GizmoDrawRows(float width, float height)
    {
        var from = new Vector3(0, 0);
        var to = new Vector3(width, 0);
        for (float y = 0; y < height; y += CellSize)
        {
            from.y = y;
            to.y = y;
            Gizmos.DrawLine(Offset + from, Offset + to);
        }

        from.y += CellSize;
        to.y += CellSize;
        Gizmos.DrawLine(Offset + from, Offset + to);
    }

    private void RebuildArray()
    {
        _cachedCellSize = CellSize;
        _cachedRows = Rows;
        _cachedColumns = Columns;

        if (_grid == null)
        {
            _grid = new Node2DGrid(width: _cachedColumns, height: _cachedRows, Offset, CellSize);
        }
        else
        {
            _grid.Reset(_cachedRows, _cachedColumns);
        }
    }

    private bool ShouldRebuildArray()
    {
        return CellSize != _cachedCellSize ||
               Columns != _cachedColumns ||
               Rows != _cachedRows;
    }

    public IEnumerable<Node2D> GetAdjacentNodes(Node2D node, bool onlyWalkableTiles = false)
                                => GetAdjacentNodes(node, onlyWalkableTiles);

    public Node2D GetNode(Vector2Int nodePosition) => Grid[nodePosition];

    public Vector2 GetNodeCenterPosition(Node2D node)
       => Offset + new Vector3(HalfCellSize + CellSize * node.Column, HalfCellSize + CellSize * node.Row);

    public Node2D GetNodeFromWorldPosition(Vector2 position)
    {
        var normalisedPosition = position - Offset2D;
        int x = (int)(normalisedPosition.x / _cachedCellSize);
        int y = (int)(normalisedPosition.y / _cachedCellSize);

        if (x < 0 || y < 0 || x >= _cachedColumns || y >= _cachedRows)
            return null;

        return GetNode(new Vector2Int(x, y));
    }

    public void Initialise()
    {
        if (TrimGrid)
            DoTrimGrid();

        if (EquallyDivideGrid)
            DoEquallyDivideGrid();

        if (ShouldRebuildArray())
            RebuildArray();
    }

    public void UpdateNodeBlockedStates()
    {
        var radius = CollisionRadiusPercentage;
        var mask = CollisionMask;

        var unwalkableNodes = new List<Node2D>();

        for (int row = 0; row < _cachedRows; row++)
            for (int col = 0; col < _cachedColumns; col++)
            {
                var node = Grid[row, col];

                if (node == null) break;

                var nodeCenter = GetNodeCenterPosition(node);

                node.Blocked = PathFinderCollisionDetector.IsColliding(nodeCenter, radius, mask);
                if (!node.Blocked) continue;

                unwalkableNodes.Add(node);
            }

        _unwalkableNodes = unwalkableNodes;
    }

    #endregion Methods

    #region LifeCycle

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = WalkableColor;
        var width = CellSize * Columns;
        var height = CellSize * Rows;

        if (GizmosCanDrawGrid)
        {
            GizmoDrawRows(width, height);
            GizmoDrawColumns(width, height);
        }

        if (_unwalkableNodes == null) return;

        foreach (var node in _unwalkableNodes)
        {
            var nodeCenter = GetNodeCenterPosition(node);

            if (GizmosCanDrawBlockedCells)
            {
                Gizmos.color = ObstacleColor;
                Gizmos.DrawCube(nodeCenter, new Vector3(CellSize, CellSize));
            }
        }

        if (DrawCollisionsGizmos)
        {
            Gizmos.color = Color.yellow;

            foreach (var node in Grid.Nodes)
            {
                var nodeCenter = GetNodeCenterPosition(node);
                Gizmos.DrawWireSphere(nodeCenter, CollisionRadiusPercentage * CellSize);
            }
        }
    }

    private void Start()
    {
        Initialise();
        UpdateNodeBlockedStates();
    }

    private void Update()
    {
        if (Application.isPlaying)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime < RefreshRate) return;
        }

        elapsedTime = elapsedTime % RefreshRate;
        Initialise();
        UpdateNodeBlockedStates();
    }

    #endregion LifeCycle
}