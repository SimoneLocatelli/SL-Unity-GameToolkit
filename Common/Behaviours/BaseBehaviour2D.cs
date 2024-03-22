using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class BaseBehaviour2D : BaseBehaviour
{
    #region Properties

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;

    public SpriteRenderer ParentSpriteRenderer { get => ParentGameObject.GetComponent<SpriteRenderer>(); }

    protected Rigidbody2D Rb { get => GetInitialisedComponent(ref _rb); }

    protected Vector3 RendererCenter { get => SpriteRenderer.bounds.center; }

    protected SpriteRenderer SpriteRenderer { get => GetInitialisedComponent(ref _spriteRenderer); }

    #endregion Properties

    #region Methods - GetComponentsInRange

    public TComponent[] GetComponentsInRange<TComponent>(float radius) where TComponent : MonoBehaviour
    {
        var colliders = GetCollidersInRange(radius);

        var components = colliders.SelectGameObjects().Select(c => c.GetComponent<TComponent>());
        return components.Where(c => c != null).ToArray();
    }

    #endregion Methods - GetComponentsInRange

    #region Methods - GetCollidersInRange

    public Collider2D[] GetCollidersInRange(float radius)
        => Physics2D.OverlapCircleAll(transform.position, radius);

    public Collider2D[] GetCollidersInRange(float radius, int layerMask)
        => Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

    #endregion Methods - GetCollidersInRange

    #region Methods - DisableCollider

    protected void DisableCollider()
    {
        var colliders = GetComponents<Collider2D>();
        foreach (var c in colliders)
            c.enabled = false;
    }

    #endregion Methods - DisableCollider
}