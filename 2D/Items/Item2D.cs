using UnityEngine;

public abstract class Item2D : BaseBehaviour, IItem
{
    #region Properties

    [SerializeField]
    private bool _canBePickedUp = true;

    public bool CanBePickedUp { get => _canBePickedUp; set => _canBePickedUp = value; }


    #endregion Properties

    #region LifeCycle

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CanBePickedUp) return;

        var obj = collision == null ? null : collision.gameObject;

        if (obj == null)
            return;

        var entity = obj.GetComponent<IPickUpItemAbility>();

        if (entity == null)
            return;

        entity.OnItemPickedUp(this);
    }

    #endregion LifeCycle
}