using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class BaseBehaviour : BaseBehaviourLight
{
    #region Properties

    private Animator _animator;
    private AudioManager _audioManager;

    public GameObject ParentGameObject
    {
        get
        {
            var parent = transform.parent;
            if (parent == null) return null;
            return parent.gameObject;
        }
    }

    public GameObject TopParent { get => gameObject.GetTopParent(); }

    protected virtual Animator Animator { get => GetInitialisedComponent(ref _animator); }

    protected AudioManager AudioManager { get => GetInitialisedComponent(ref _audioManager); }

    #endregion Properties


    #region Methods - IsInRange

    public bool IsInRange(GameObject obj, float range)
    {
        if (obj == null)
            return false;

        return IsInRange(obj.transform.position, range);
    }

    public bool IsInRange(Transform transform, float radius)
    {
        if (transform == null)
            return false;

        return IsInRange(transform.position, radius);
    }

    public bool IsInRange(Vector3 position, float radius) => transform.position.IsInRange(position, radius);

    #endregion Methods - IsInRange

    #region Methods - IsOutsideRange

    public bool IsOutsideRange(GameObject obj, float radius)
    {
        if (obj == null)
            return false;

        return IsOutsideRange(obj.transform.position, radius);
    }

    public bool IsOutsideRange(Vector3 position, float attackRange) => transform.position.IsOutsideRange(position, attackRange);

    #endregion Methods - IsOutsideRange

    #region Methods - Destroy

    protected void Destroy()
        => Destroy(gameObject);

    protected void DestroyTopParent()
        => Destroy(TopParent);

    #endregion Methods - Destroy
}