using UnityEngine;

public abstract class BaseCollisionListener : BaseBehaviour
{
    private ICollisionHandler _collisionHandler;

    private void Awake()
    {
        _collisionHandler = GetComponent<ICollisionHandler>();

        if (_collisionHandler == null) return;

        _collisionHandler.OnImpact += OnImpactOccurred;
    }

    private void OnDestroy()
    {
        if (_collisionHandler == null) return;
        _collisionHandler.OnImpact -= OnImpactOccurred;
    }

    protected abstract void OnImpactOccurred(ICollisionHandler collisionHandler, GameObject collidedObject);
}
