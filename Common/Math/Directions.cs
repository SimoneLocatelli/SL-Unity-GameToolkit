using UnityEngine;

public enum Directions
{
    Zero = 0,
    Up, Down,
    Left, Right,
}

public static class DirectionsExtensions
{
    public static Vector2 ToVector2(this Directions direction)
    {
        switch (direction)
        {
            case Directions.Up: return Vector2.up;
            case Directions.Down: return Vector2.down;
            case Directions.Left: return Vector2.left;
            case Directions.Right: return Vector2.right;
            case Directions.Zero: return Vector2.zero;
            default: throw new System.NotImplementedException();
        }
    }

    public static Vector2 ToVector2(this VectorDirection direction)
    {
        var x = direction.X.ToVector2().x;
        var y = direction.Y.ToVector2().y;
        return new Vector2(x, y);
    }

    public static VectorDirection ToVectorDirection(this Vector2 v, bool allowForZeroDirection = true)
        => new VectorDirection(v, allowForZeroDirection);

    public static Directions ToVerticalDirection(this Vector2 v)
    {
        if (v.y < 0) return Directions.Down;
        if (v.y > 0) return Directions.Up;
        return Directions.Zero;
    }

    public static Directions ToHorizontalDirection(this Vector2 v)
    {
        if (v.x > 0) return Directions.Right;
        if (v.x < 0) return Directions.Left;
        return Directions.Zero;
    }
}