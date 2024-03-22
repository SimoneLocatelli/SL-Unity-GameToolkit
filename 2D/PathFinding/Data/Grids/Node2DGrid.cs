using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Taken from https://github.com/JamieG/AStar
/// </summary>
public class Node2DGrid
{
    #region Properties

    private Node2D[,] _nodes;

    public float CellSize { get; private set; }

    public int Height { get; private set; }

    public Node2D[,] Nodes { get => _nodes; }

    public Vector3 Offset { get; private set; }

    public Vector2Int Size => new Vector2Int(Width, Height);

    public int Width { get; private set; }

    #endregion Properties

    #region Indexers

    public Node2D this[int row, int col] => _nodes[row, col];
    public Node2D this[Vector2Int location] => _nodes[location.y, location.x];
    public Node2D this[Node2D node] => this[node.Row, node.Column];

    private bool IsOutOfBounds(int x, int y)
        => x >= _nodes.GetLength(1) || y >= _nodes.GetLength(0);

    #endregion Indexers

    #region Ctor

    public Node2DGrid(int width, int height, Vector3 offset, float cellSize)
    {
        Offset = offset;
        CellSize = cellSize;
        Reset(height, width);
    }

    #endregion Ctor

    #region Methods

    internal IEnumerator<Node2D> GetEnumerator()
        => (IEnumerator<Node2D>)_nodes.GetEnumerator();

    public int GetNodeId(Vector2Int location) => location.x * Width + location.y;

    public void Reset(int height, int width)
    {
        Width = width;
        Height = height;
        _nodes = new Node2D[height, width];
        Reset();
    }

    public void Reset()
    {
        for (var row = 0; row <= _nodes.GetUpperBound(0); row++)
            for (var col = 0; col <= _nodes.GetUpperBound(1); col++)
            {
                var cell = this[row, col];

                if (cell == null)
                    _nodes[row, col] = new Node2D(row, col);
            }
    }

    #endregion Methods
}