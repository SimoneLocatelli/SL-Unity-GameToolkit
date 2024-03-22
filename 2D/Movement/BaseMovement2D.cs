using System;
using UnityEngine;

[Serializable]
public class BaseMovement2D /*: IMovement2D*/
{
    #region Properties

    public bool IsMoving { get => Movement != Vector2.zero; }
    public float LastX { get; protected set; }
    public Vector2 Movement { get; private set; }

    #endregion Properties

    #region Methods

    public void MoveTowardsTarget(Rigidbody2D rb, float speed)
    {
        if (Movement == Vector2.zero)
            return;

        MovementUtils2D.MoveTowardsDirection(rb, Movement, speed);
    }

    public void MoveTowardsTarget(Transform transform, float speed)
    {
        if (Movement == Vector2.zero)
            return;

        MovementUtils2D.MoveTowardsDirection(transform, Movement, speed);
    }

    public void ResetMovement() => UpdateMovement(Vector2.zero);

    public void UpdateMovement(Vector2 movement)
    {
        Movement = movement.normalized;

        if (Movement.x != 0)
            LastX = Movement.x;
    }

    protected Vector2 GetMovementVector(Vector2 currentPosition, Vector2 targetPosition)
        => targetPosition.GetDirectionNormalised(currentPosition);

    #endregion Methods
}