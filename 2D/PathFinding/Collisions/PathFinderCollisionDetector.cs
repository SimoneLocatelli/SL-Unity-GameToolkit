using UnityEngine;

internal class PathFinderCollisionDetector
{
    public static bool IsColliding(Vector2 position, float radius, LayerMask mask)
        => Physics2D.OverlapCircle(position, radius, mask) != null;
}