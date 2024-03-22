using UnityEngine;

public class GroundChecker
{
    private Collider2D collider;

    public GroundChecker(Collider2D collider)
    {
        this.collider = collider;
    }

    private Vector2 ColliderCenter { get => collider.bounds.center; }
    private Vector2 ColliderSize { get => collider.bounds.size; }

    public bool IsOnGround { get; private set; }

    public bool Update(LayerMask whatIsGround, float groundRayDistance = 1f)
    {
        RaycastHit2D hit = Physics2D.BoxCast(ColliderCenter, ColliderSize, 0f, Vector2.down, groundRayDistance, whatIsGround);
        IsOnGround = hit.collider != null;

        return IsOnGround;
    }
}