using UnityEngine;

public class WeaponImpact : BaseCollisionListener
{
    [Range(1, 75)]
    public float ImpulseForce = 10;

    protected override void OnImpactOccurred(ICollisionHandler collisionHandler, GameObject obj)
    {
        ApplyForce(collisionHandler, obj);
    }

    private void ApplyForce(ICollisionHandler collisionHandler, GameObject obj)
    {
        if (ImpulseForce > 0)
            obj.GetComponent<Rigidbody2D>().AddForce(collisionHandler.Direction * ImpulseForce, ForceMode2D.Impulse);
    }
}