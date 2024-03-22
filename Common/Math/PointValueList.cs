using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

[DebuggerDisplay("{location}, x = {x} y = {y}")]
public class PointValueList<T> : List<PointValue<T>>
{
    public readonly int x;
    public readonly int y;
    public readonly QuadrantLocations location;

    public PointValueList(QuadrantLocations location)

    {
        this.location = location;
        switch (location)
        {
            case QuadrantLocations.TopRight:
                this.x = 1;
                this.y = 1;
                break;

            case QuadrantLocations.TopLeft:
                this.x = -1;
                this.y = 1;
                break;

            case QuadrantLocations.BottomRight:
                this.x = 1;
                this.y = -1;
                break;

            case QuadrantLocations.BottomLeft:
                this.x = -1;
                this.y = -1;
                break;
        }
    }

    public PointValue<T> this[int x, int y]
    {
        get => this.FirstOrDefault(c => c.X == x && c.Y == y);
        set
        {
            RemovePoint(x, y);
            Add(value);
        }
    }

    public List<int> AllX { get => this.Select(c => c.X).ToList(); }
    public List<int> AllY { get => this.Select(c => c.Y).ToList(); }

    public int MaxX { get => this.Any() ? AllX.Max() : 0; }
    public int MinX { get => this.Any() ? AllX.Min() : 0; }
    public int MaxY { get => this.Any() ? AllY.Max() : 0; }
    public int MinY { get => this.Any() ? AllY.Min() : 0; }

    public void RemovePoint(int x, int y)
    {
        var point = this[x, y];
        if (point != null)
            Remove(point);
    }
}