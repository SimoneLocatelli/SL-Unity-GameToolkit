using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class BaseBehaviour3D : BaseBehaviour
{
    #region Properties

    private Rigidbody _rb;


    protected Rigidbody Rb { get => GetInitialisedComponent(ref _rb); }

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

    public Collider[] GetCollidersInRange(float radius)
        => Physics.OverlapSphere(transform.position, radius);

    public Collider[] GetCollidersInRange(float radius, int layerMask)
        => Physics.OverlapSphere(transform.position, radius, layerMask);

    #endregion Methods - GetCollidersInRange

    #region Methods - DisableCollider

    protected void DisableCollider()
    {
        var colliders = GetComponents<Collider>();
        foreach (var c in colliders)
            c.enabled = false;
    }

    #endregion Methods - DisableCollider
}