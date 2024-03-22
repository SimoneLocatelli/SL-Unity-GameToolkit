//using UnityEngine;

//public abstract class BaseMovement2D : IMovement2D
//{
//    #region Properties

//    public CanMoveToPositionChecker CanMoveToPositionChecker { get; internal set; }
//    public bool IsMoving { get => Movement != Vector2.zero; }
//    public float LastX { get; protected set; }
//    public Vector2 Movement { get; private set; }

//    #endregion Properties

//    #region Methods

//    public void MoveTowardsTarget(Rigidbody2D rb, float speed)
//    {
//        if (Movement == Vector2.zero)
//            return;

//        //var targetPosition = rb.position + Movement;
//        //Vector3 movePosition = CalculateMovePosition(speed, rb.transform.position, targetPosition);
//        //if (CanGoTo(movePosition))
//        //    rb.MovePosition(movePosition);

//        var position = rb.transform.position;
//        var targetPosition = position + new Vector3(Movement.x, Movement.y);
//        Vector3 movePosition = CalculateMovePosition(speed, position, targetPosition);
//        if (CanGoTo(movePosition))
//            rb.transform.position = movePosition;
//        rb.transform.position = movePosition;
//    }

//    public void MoveTowardsTarget(Transform transform, float speed)
//    {
//        if (Movement == Vector2.zero)
//            return;

//        var position = transform.position;
//        var targetPosition = position + new Vector3(Movement.x, 0, Movement.y);
//        Vector3 movePosition = CalculateMovePosition(speed, position, targetPosition);
//        if (CanGoTo(movePosition))
//            transform.position = movePosition;
//        transform.position = movePosition;
//    }

//    public void UpdateMovement(Vector3 movement)
//    {
//        Movement = movement.normalized;

//        if (Movement.x != 0)
//        {
//            LastX = Movement.x;
//        }
//    }

//    protected Vector2 GetMovementVector(Vector2 currentPosition, Vector2 targetPosition)
//    {
//        return (targetPosition - currentPosition).normalized;
//    }

//    private static Vector3 CalculateMovePosition(float speed, Vector3 position, Vector3 targetPosition)
//    {
//        Vector3 movePosition = position;
//        movePosition.x = Mathf.MoveTowards(movePosition.x, targetPosition.x, speed * Time.deltaTime);
//        //movePosition.y = Mathf.MoveTowards(movePosition.y, targetPosition.y, speed * Time.deltaTime);
//        movePosition.z = Mathf.MoveTowards(movePosition.z, targetPosition.z, speed * Time.deltaTime);
//        return movePosition;


//    }

//    private bool CanGoTo(Vector3 movePosition)
//    {
//        return CanMoveToPositionChecker == null || CanMoveToPositionChecker.CanGoTo(movePosition);
//    }

//    #endregion Methods
//}