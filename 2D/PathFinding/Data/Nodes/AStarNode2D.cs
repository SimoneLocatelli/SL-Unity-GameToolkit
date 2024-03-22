using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

/// <summary>
/// Wrapper for the <see cref="Node2D"/> class which adds properties
/// regarding aspect of the A* PathFinding logic.
/// These nodes are not supposed to be shared
/// (as in being referenced by different <see cref="AStarNode2DGrid"/> instances)
/// as they contain data that is relative to a specific path and agent.
/// For example, the values for <see cref="F"/>, <see cref="G"/> and <see cref="H"/>
/// will depend on the position of the start node and end node.
/// </summary>
[DebuggerDisplay("x,y - ({Column}, {Row}) - [F, G, H] - [{F}, {G}, {H}] - Blocked {Blocked} - Closed {Closed}")]
public class AStarNode2D
{
    #region Properties

    public bool Blocked { get => Node.Blocked; }

    public bool Closed;

    public double F;

    public double G;

    public double H;

    public int QueueIndex;

    public Node2D Node { get; private set; }

    public AStarNode2D Parent { get; set; }

    private int Column { get => Node.Column; }

    private int Row { get => Node.Row; }

    #endregion Properties

    #region Ctors

    public AStarNode2D(Node2D node)
        => Node = node ?? throw new ArgumentNullException(nameof(node));

    public AStarNode2D(Node2D node, AStarNode2D parent = null) : this(node)
    {
        Parent = parent;
    }

    #endregion Ctors

    #region Equals / == overrides

    /// <summary>
    /// See <see cref="Equals(object)" /> summary for important gotcha.
    /// </summary>
    public static bool operator !=(AStarNode2D left, AStarNode2D right)
        => !(left == right);

    /// <summary>
    /// See <see cref="Equals(object)" /> summary for important gotcha.
    /// </summary>
    public static bool operator ==(AStarNode2D left, AStarNode2D right)
    {
        if (left is null)
            return right is null;

        return left.Equals(right);
    }

    /// <summary>
    /// Compares an object instance to this <see cref="AStarNode2D"/> instance.
    /// IMPORTANT NOTE - The comparison is based on the <see cref="Node2D"/> comparison logic
    /// which takes into consideration only the position of the node.
    /// Two <see cref="AStarNode2D"/> instances with the same position
    /// (e.g. coming from two different <see cref="AStarNode2DGrid"/> instances)
    /// will return true even though they are two different instances
    /// </summary>
    /// <param name="obj">The instance to compare</param>
    public override bool Equals(object obj)
    {
        if (obj is AStarNode2D astarNode) return Equals(astarNode);
        if (obj is Node2D node) return Equals(node);
        return false;
    }

    public override int GetHashCode()
        => Node.GetHashCode();

    private bool Equals(AStarNode2D node)
        => node != null && Equals(node.Node);

    private bool Equals(Node2D node)
        => node != null && this.Node.Equals(node);

    #endregion Equals / == overrides
}