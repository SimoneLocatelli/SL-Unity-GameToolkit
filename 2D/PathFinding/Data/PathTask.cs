using System.Collections.Generic;

public class PathTask
{
    public AStarNode2DGrid Grid { get; set; }

    public Path Path { get; set; }

    public PathTask(Path path, AStarNode2DGrid grid)
    {
        Path = path;
        Grid = grid;
    }

    public PathTask(Stack<AStarNode2D> path, AStarNode2DGrid grid)
        : this(new Path(path, grid.Offset, grid.CellSize), grid)
    {
    }
}