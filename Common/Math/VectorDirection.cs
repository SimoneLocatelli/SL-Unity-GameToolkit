using System;
using UnityEngine;

public struct VectorDirection
{
    private Directions _x;
    private Directions _y;

    public Directions X
    {
        get => _x;
        set
        {
            if (value == Directions.Zero || value == _x)
                return;

            if (value != Directions.Left && value != Directions.Right)
                throw new ArgumentException($"Invalid value {value} for X (Horizontal) direction");

            _x = value;
        }
    }

    public Directions Y
    {
        get => _y;
        set
        {
            if (value == Directions.Zero || value == _y)
                return;

            if (value != Directions.Up && value != Directions.Down)
                throw new ArgumentException($"Invalid value {value} for Y (Vertical) direction");

            _y = value;
        }
    }

    public VectorDirection(Vector2 v, bool allowForZeroDirection = true)
        : this(v.ToHorizontalDirection(), v.ToVerticalDirection(), allowForZeroDirection)
    { }

    public VectorDirection(Directions x, Directions y, bool allowForZeroDirection = true)
    {
        if (!allowForZeroDirection)
        {
            if (x == Directions.Zero) throw new ArgumentException($"X cannot be initialised with {Directions.Zero}");
            if (y == Directions.Zero) throw new ArgumentException($"Y cannot be initialised with {Directions.Zero}");
        }

        _x = x;
        _y = y;
    }

    public bool Update(Vector2 movement)
    {
        var x = movement.x;
        var y = movement.y;

        var xDir = Directions.Zero;
        var yDir = Directions.Zero;

        if (x != 0) xDir = x > 0 ? Directions.Right : Directions.Left;
        if (y != 0) yDir = y > 0 ? Directions.Up : Directions.Down;

        return Update(xDir, yDir);
    }

    /// <summary>
    /// Returns true if the directions has changed after the update.
    /// </summary>
    /// <param name="x">The new x (horizontal) direction</param>
    /// <param name="y">The new y (vertical) direction</param>
    public bool Update(Directions x, Directions y)
    {
        var oldX = X;
        var oldY = Y;

        X = x;
        Y = y;

        return oldX != X || oldY != Y;
    }
}