using System;
using UnityEngine;
using UnityEngine.Assertions;

public class DropItem : BaseBehaviour
{
    #region Enums

    public enum Triggers
    {
        OnDeath
    }

    #endregion Enums

    #region Properties

    public GameObject ItemToDrop;

    public Triggers Trigger;

    #endregion Properties

    #region Methods

    private void AttachDeathEvent()
    {
        var lifeSystem = GetComponent<LifeSystem>();

        Assert.IsNotNull(lifeSystem, $"Expected {nameof(LifeSystem)} component.");

        lifeSystem.OnDeath += TriggerDropItem;
    }

    private void RefreshTrigger()
    {
        switch (Trigger)
        {
            case Triggers.OnDeath:
                AttachDeathEvent();

                break;

            default:
                throw new InvalidOperationException("Enum not recognised - " + Trigger);
        }
    }

    private void TriggerDropItem(LifeSystem obj)
    {
        var itemDropped = Instantiate(ItemToDrop);

        itemDropped.transform.position = transform.position;
    }

    #endregion Methods

    #region LifeCycle

    private void Awake()
    {
        RefreshTrigger();
    }

    #endregion LifeCycle
}