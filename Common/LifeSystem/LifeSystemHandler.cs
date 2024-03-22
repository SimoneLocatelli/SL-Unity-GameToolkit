using UnityEngine;

public static class LifeSystemHandler
{
    internal static bool ApplyDamage(Collision2D collision, int damage)
    {
        if (collision == null)
            return false;

        return ApplyDamage(collision.gameObject, damage);
    }

    public static bool ApplyDamage(Collider2D collider, int damage)
    {
        if (collider == null)
            return false;

        return ApplyDamage(collider.gameObject, damage);
    }

    internal static bool ApplyDamage(Collision collision, int damage)
    {
        if (collision == null)
            return false;

        return ApplyDamage(collision.gameObject, damage);
    }

    internal static bool ApplyDamage(Collider collider, int damage)
    {
        if (collider == null)
            return false;

        return ApplyDamage(collider.gameObject, damage);
    }

    public static bool ApplyDamage(GameObject gameObject, int damage)
    {
        if (gameObject == null)
            return false;

        var lifeSystem = gameObject.GetComponent<LifeSystem>();

        if (lifeSystem == null)
            return false;

        return lifeSystem.ApplyDamage(damage);
    }
}