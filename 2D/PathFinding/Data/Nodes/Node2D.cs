using System.Diagnostics;
using UnityEngine;

/// <summary>
/// Define a node in a 2D space (x, y => Column, Row).
/// These nodes can be referenced by multiple <see cref="AStarNode2D"/> instances
/// as they hold generic data about a location/node of a given grid.
/// </summary>
[DebuggerDisplay("Row, Col - ({Row}, {Column}) - Blocked - {Blocked}")]
public class Node2D
{
    public readonly int Column;
    public readonly int Row;

    public bool Blocked = false;

    public Node2D(int row, int col)
    {
        Row = row;
        Column = col;
    }

    private Vector2Int? _columnRowVector;

    public Vector2Int ColumnRowVector
    {
        get
        {
            _columnRowVector = _columnRowVector ?? new Vector2Int(Column, Row);
            return _columnRowVector.Value;
        }
    }

    public bool Equals(Node2D node)
    {
        return node != null && node.Row == Row && node.Column == Column;
    }

    public override bool Equals(object obj)
    {
        if (obj is Node2D node) return Equals(node);

        return false;
    }

    /// <summary>
    /// Generates an hashcode based on the location of the node
    /// </summary>
    public override int GetHashCode()
        => (7 * Column).GetHashCode() + (13 * Row).GetHashCode();
}