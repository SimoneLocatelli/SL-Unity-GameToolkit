using UnityEngine;

public static class MovementUtils2D
{
    public static Vector3 CalculateMovePosition(Vector2 position, Vector2 targetPosition, float speed, float time)
    {
        var x = Mathf.MoveTowards(position.x, targetPosition.x, speed * time);
        var y = Mathf.MoveTowards(position.y, targetPosition.y, speed * time);

        var newPosition = Vector3.MoveTowards(position, targetPosition, speed * Time.deltaTime);

        x = newPosition.x;
        y = newPosition.y;

        if (x != targetPosition.x)
            x = x < targetPosition.x ?
                Mathf.Min(x, targetPosition.x):
                Mathf.Max(x, targetPosition.x);


        if (y != targetPosition.y)
            y = y < targetPosition.y ?
                Mathf.Min(y, targetPosition.y) :
                Mathf.Max(y, targetPosition.y);

        return new Vector2(x, y);
    }

    public static void MoveTowardsDirection(Rigidbody2D rb, Vector2 direction, float speed)
        => MoveTowardsDirection(rb, direction, speed, Time.deltaTime);

    public static void MoveTowardsDirection(Rigidbody2D rb, Vector2 direction, float speed, float time)
    {
        var targetPosition = rb.position + direction;
        Vector3 movePosition = CalculateMovePosition(rb.position, targetPosition, speed, time);
        rb.MovePosition(movePosition);
    }

    public static void MoveTowardsDirection(Transform transform, Vector2 direction, float speed)
        => MoveTowardsDirection(transform, direction, speed, Time.deltaTime);

    public static void MoveTowardsDirection(Transform transform, Vector2 direction, float speed, float time)
    {
        var position = transform.position.ToVector2();
        var targetPosition = position + direction;
        Vector3 movePosition = CalculateMovePosition(position, targetPosition, speed, time);
        transform.position = movePosition;
    }

    public static void MoveTowardsTarget(Transform transform, Vector2 target, float speed)
        => MoveTowardsTarget(transform, target, speed, Time.deltaTime);

    public static void MoveTowardsTarget(Transform transform, Vector2 target, float speed, float time)
    {
        var dir = transform.position.GetDirection(target);

        MoveTowardsDirection(transform, dir, speed, time);
    }
}