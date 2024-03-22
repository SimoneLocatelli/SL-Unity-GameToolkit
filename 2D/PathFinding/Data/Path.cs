using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// List of nodes sorted by the order in which they need to be traversed.
/// This list is usually the result of a path calculation and should not be shared
/// by multiple agents (path followers) as it contains information about the current
/// node an agent is moving towards.
/// </summary>
public class Path : List<AStarNode2D>
{
    public readonly float CellSize;
    public readonly Vector3 HalfCellSizeVector;
    private Vector3 _offset;

    private int _currentNodeIndex;

    public Vector3 LastNodePosition
    {
        get => GetNodeCenterPosition(LastNodeIndex);
    }

    public int CurrentNodeIndex
    {
        get => _currentNodeIndex;
        set => _currentNodeIndex = Mathf.Clamp(value, 0, LastNodeIndex);
    }

    internal bool IsCurrentNodeLastNode => CurrentNodeIndex == LastNodeIndex;

    private int LastNodeIndex
    {
        get
        {
            if (this.Any()) return Count - 1;
            return 0;
        }
    }

    public Path(IEnumerable<AStarNode2D> collection, Vector3 offset, float cellSize) : base(collection)
    {
        Assert.IsNotNull(collection);

        _offset = offset;
        CellSize = cellSize;
        HalfCellSizeVector = new Vector3(CellSize / 2, CellSize / 2);
    }

    public IEnumerable<Vector3> GetAllNodesCenterPosition()
        => this.Skip(CurrentNodeIndex).Select(n => GetNodeCenterPosition(n));

    public Vector3 GetNodeCenterPosition(int nodeIndex)
    {
        Assert.IsTrue(this.Any(), "Method cannot be called when path is empty");

        return GetNodeCenterPosition(this[Mathf.Clamp(nodeIndex, 0, LastNodeIndex)]);
    }

    public Vector3 GetNodeCenterPosition(AStarNode2D n)
    {
        var nodeColRowVector = n.Node.ColumnRowVector.ToVector3();

        return _offset + HalfCellSizeVector + Vector3.Scale(nodeColRowVector, new Vector3(CellSize, CellSize));
    }
}