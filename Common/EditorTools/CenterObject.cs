using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class CenterObject : BaseBehaviourLight
{
    [SerializeField] private Vector3 Offset = new Vector3(0.5f, 0, 0.5f);

    [ReadOnlyProperty]
    [SerializeField] private bool transformHasChanged;

    private void FixPosition()
    {
        var tr = transform;

        transformHasChanged = tr.hasChanged;

        if (!transform.hasChanged)
            return;

        var position = tr.position;

        var x = SnapToPosition(position.x, Offset.x);
        var y = SnapToPosition(position.y, Offset.y);
        var z = SnapToPosition(position.z, Offset.z);

        //        var x = Mathf.FloorToInt(position.x) + Offset.x;
        //        var y = Mathf.FloorToInt(position.y) + Offset.y;
        //        var z = Mathf.FloorToInt(position.z) + Offset.z;
        var newPosition = new Vector3(x, y, z);

        DebugLog($"Moving from {position} to {newPosition}");
 tr.position = newPosition;
        tr.hasChanged = false;
    }

    public float SnapToPosition(float currentCoordinate, float snapValue)
    {
        // When zero, no snapping will occurr
        if (snapValue == 0)
            return currentCoordinate;

        // Offset from closest snap divider (e.g. snapValue == 0.5; x=10.24 -> 0.24)
        float remainder = currentCoordinate % snapValue;

        if (remainder == 0)
            return currentCoordinate;

        int difference2 = (int)(remainder * 100 / snapValue);

        // Round up (add difference to next snap point)
        if (difference2 >= 50)
            return currentCoordinate + (snapValue - remainder);

        // difference is < 50)
        // Round down (subtract difference from previous snap point)
        return currentCoordinate - remainder;
    }

    private void Update()
    {
        if (!Application.isPlaying)
            FixPosition();
    }
}