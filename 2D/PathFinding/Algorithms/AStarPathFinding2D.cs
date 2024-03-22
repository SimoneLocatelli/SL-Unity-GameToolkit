using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public static class AStarPathFinding2D
{
    #region Properties

    public static readonly Vector2Int[] Directions = {
            // Cardinal
            new Vector2Int(-1, +0), // W
            new Vector2Int(+1, +0), // E
            new Vector2Int(+0, +1), // N
            new Vector2Int(+0, -1), // S
            // Diagonal
            new Vector2Int(-1, -1), // NW
            new Vector2Int(-1, +1), // SW
            new Vector2Int(+1, -1), // NE
            new Vector2Int(+1, +1)  // SE
        };

    #endregion Properties

    #region Methods

    public static PathTask Find(AStarNode2DGrid grid, Node2D startNode, Node2D endNode)
    {
        if (startNode == null || endNode == null) return null;

        Assert.IsNotNull(grid);

        return Find(grid, new AStarNode2D(startNode), new AStarNode2D(endNode));
    }

    public static PathTask Find(AStarNode2DGrid grid, AStarNode2D startNode, AStarNode2D endNode)
    {
        // Taken from https://github.com/JamieG/AStar

        var _open = new FastPriorityQueue(grid.Width * grid.Height);

        grid.Reset();
        _open.Clear();

        AStarNode2D startCell = grid[startNode];
        AStarNode2D goalCell = grid[endNode];

        _open.Enqueue(startCell, 0);

        var bounds = grid.Size;

        AStarNode2D node = null;

        while (_open.Count > 0)
        {
            node = _open.Dequeue();

            node.Closed = true;

            var cBlock = false;

            var g = node.G + 1;

            if (goalCell == node)
                break;

            Vector2Int proposed = new Vector2Int(0, 0);

            for (var i = 0; i < Directions.Length; i++)
            {
                var direction = Directions[i];

                proposed.x = node.Node.Column + direction.x;
                proposed.y = node.Node.Row + direction.y;

                // Bounds checking
                if (proposed.x < 0 || proposed.x >= bounds.x ||
                    proposed.y < 0 || proposed.y >= bounds.y)
                    continue;

                AStarNode2D neighbour = grid[proposed];

                if (neighbour == node)
                {
                    node = neighbour;
                    continue;
                }

                if (neighbour.Blocked)
                {
                    if (neighbour == endNode)
                    {
                        _open.Clear();
                        break;
                    }

                    if (i < 4) cBlock = true;

                    continue;
                }

                // Prevent slipping between blocked cardinals by an open diagonal
                if (i >= 4 && cBlock)
                    continue;

                if (grid[neighbour.Node].Closed)
                    continue;

                if (!_open.Contains(neighbour))
                {
                    neighbour.G = g;
                    neighbour.H = Heuristic(neighbour, endNode);
                    neighbour.Parent = node;

                    // F will be set by the queue
                    _open.Enqueue(neighbour, neighbour.G + neighbour.H);
                }
                else if (g + neighbour.H < neighbour.F)
                {
                    neighbour.G = g;
                    neighbour.F = neighbour.G + neighbour.H;
                    neighbour.Parent = node;
                }
            }
        }

        var path = new Stack<AStarNode2D>();

        while (node != null)
        {
            path.Push(node);
            node = node.Parent;
        }

        var pathTask = new PathTask(path, grid);

        return pathTask;
    }

    //private static double Heuristic(AStarNode2D cell, AStarNode2D goal)
    //{
    //    var dX = Math.Abs(cell.Node.Column - goal.Node.Column);
    //    var dY = Math.Abs(cell.Node.Row - goal.Node.Row);

    //    // Octile distance
    //    return 1 * (dX + dY) + (Math.Sqrt(2) - 2 * 1) * Math.Min(dX, dY);
    //}

    private static double Heuristic(AStarNode2D cell, AStarNode2D goal)
    {
        var heuristicEstimate = 2;
        var dX = Math.Abs(cell.Node.Column - goal.Node.Column);
        var dY = Math.Abs(cell.Node.Row - goal.Node.Row);
        var h = heuristicEstimate * (dY + dX);
        return h;
    }

    #endregion Methods
}