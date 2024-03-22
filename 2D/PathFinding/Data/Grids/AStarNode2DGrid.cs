using UnityEngine;

/// <summary>
/// An grid made of <see cref="AStarNode2D"/>, similar to <see cref="Node2DGrid"/>.
/// The reason for this additional grid class is that <see cref="AStarNode2D"/> instances are not supposed to be shared
/// as the properties within that class are relative to the state of the agent, while <see cref="Node2D"/> instances contain
/// generic information (such as the position of the node within its grid).
/// See <see cref="AStarNode2D"/> for further details.
/// </summary>
public class AStarNode2DGrid
{
    #region Properties

    private readonly AStarNode2D[,] _nodes;

    private readonly Node2DGrid grid;

    public float CellSize { get => grid.CellSize; }

    public int Height { get => grid.Height; }

    public Vector3 Offset { get => grid.Offset; }

    public Vector2Int Size => new Vector2Int(Width, Height);

    public int Width { get => grid.Width; }

    #endregion Properties

    #region Indexers

    public AStarNode2D this[int row, int col] => _nodes[row, col];

    public AStarNode2D this[Vector2Int location] => this[location.y, location.x];

    public AStarNode2D this[AStarNode2D node] => this[node.Node];

    public AStarNode2D this[Node2D node] => this[node.Row, node.Column];

    #endregion Indexers

    #region Ctor

    public AStarNode2DGrid(Node2DGrid grid)
    {
        this.grid = grid;
        _nodes = new AStarNode2D[grid.Height, grid.Width];
        Reset();
    }

    #endregion Ctor

    #region Methods

    public int GetNodeId(Vector2Int location) => location.x * Width + location.y;

    public void Reset()
    {
        for (var row = 0; row <= _nodes.GetUpperBound(0); row++)
            for (var col = 0; col <= _nodes.GetUpperBound(1); col++)
            {
                var cell = _nodes[row, col];

                if (cell == null)
                {
                    _nodes[row, col] = new AStarNode2D(grid[row, col]);
                    continue;
                }

                cell.G = 0;
                cell.H = 0;
                cell.F = 0;
                cell.Closed = false;
                cell.Parent = null;
            }
    }

    #endregion Methods
}