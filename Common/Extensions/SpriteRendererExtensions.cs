using UnityEngine;

public static class SpriteRendererExtensions
{
    public static void FlipTowardsTarget(this SpriteRenderer sr, Vector2 target)
    {
        var x = sr.transform.position.GetDirectionNormalised(target).x;

        sr.flipX = x > 0;
    }
}