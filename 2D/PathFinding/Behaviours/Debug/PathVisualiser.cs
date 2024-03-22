using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PathUpdater))]
public class PathVisualiser : MonoBehaviour
{
    #region Properties

    private PathUpdater _pathUpdater;

    public bool DrawNodePathMetrics = false;

    #endregion Properties

    #region LifeCycle

    private void OnDrawGizmos()
    {
        if (_pathUpdater == null || !_pathUpdater.HasPath)
            return;

        var path = _pathUpdater.Path;
        var pathTask = _pathUpdater.PathTask;

        if(DrawNodePathMetrics)
            DoDrawNodePathMetrics(pathTask);

        var nodesCenterPos = path.GetAllNodesCenterPosition().ToList();

        var colliderPivotPoint = _pathUpdater.GetColliderPivotPoint();

        if (!colliderPivotPoint.HasValue)
            return;

        nodesCenterPos.Insert(0, colliderPivotPoint.Value);

        Gizmos.color = Color.blue;

        Gizmos2D.DrawOpenPolygon(nodesCenterPos);
    }

    private static void DoDrawNodePathMetrics(PathTask pathTask)
    {
        var grid = pathTask.Grid;
        var path = pathTask.Path;
        var cellSize = grid.CellSize;
        var halfCellSize = cellSize / 2;
        var labelsDistance = cellSize / 6;


        for (int x = 0; x < grid.Width; x++)
            for (int y = 0; y < grid.Height; y++)
            {
                var node = grid[y, x];
                var position = path.GetNodeCenterPosition(node);
                position = position.Add(xDelta: -halfCellSize, yDelta: halfCellSize);

                DebugStringDrawer.DrawString($"{node.Node.Column} - {node.Node.Row}", position, colour: Color.white);
                DebugStringDrawer.DrawString($"F ({node.F:F2})", position.Add(yDelta: -labelsDistance * 1), colour: Color.white);
                DebugStringDrawer.DrawString($"G ({node.G:F2})", position.Add(yDelta: -labelsDistance * 2), colour: Color.white);
                DebugStringDrawer.DrawString($"H ({node.H:F2})", position.Add(yDelta: -labelsDistance * 3), colour: Color.white);
            }
    }

    private void Start()
        => _pathUpdater = GetComponent<PathUpdater>();

    #endregion LifeCycle
}