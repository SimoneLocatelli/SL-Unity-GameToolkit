using UnityEngine;

public class MouseSystem
{
    private float defaultRadius = 2f;

    public float DefaultRadius
    {
        get => defaultRadius;
        set => defaultRadius = Mathf.Clamp(value, 1f, 5f);
    }
    public Vector2 Axis => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

    public GameObject GetItemAtMousePosition() => GetItemAtMousePosition(DefaultRadius);

    public GameObject GetItemAtMousePosition(float radius)
    {
        var worldPosition = GetMouseWorldPosition();
        var colliders = Physics2D.OverlapCircleAll(worldPosition.ToVector2(), radius);

        if (colliders == null || colliders.Length == 0) return null;

        return worldPosition.GetClosestObject(colliders.SelectGameObjects());
    }

    public virtual Vector2 GetMousePosition() => Input.mousePosition;

    public Vector3 GetMouseWorldPosition()
        => Camera.main.ScreenToWorldPoint(GetMousePosition());

    public bool HasPressedLeftButton() => Input.GetMouseButtonDown(0);

    public bool HasPressedMiddleButton() => Input.GetMouseButtonDown(2);

    public bool HasPressedRightButton() => Input.GetMouseButtonDown(1);

    public bool HasReleasedLeftButton() => Input.GetMouseButtonUp(0);

    public bool IsInsideGameScreen(out Vector2 mousePosition)
    {
        mousePosition = GetMousePosition();

        var x = mousePosition.x;
        var y = mousePosition.y;

        if (x < 0 || x > Screen.width)
            return false;

        if (y < 0 || y > Screen.height)
            return false;

        return true;
    }

    public bool IsInsideGameScreen()
        => IsInsideGameScreen(out Vector2 mousePosition);

    public bool IsZooming(out Vector2 mouseScrollDelta)
    {
        mouseScrollDelta = Input.mouseScrollDelta;
        if (mouseScrollDelta == Vector2.zero)
            return false;

        return true;
    }
}