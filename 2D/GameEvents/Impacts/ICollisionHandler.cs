using UnityEngine;

public delegate void ImpactEventHandler(ICollisionHandler collisionHandler, GameObject collidedObject);

public interface ICollisionHandler
{
    public Vector3 Direction { get; }

    event ImpactEventHandler OnImpact;
}