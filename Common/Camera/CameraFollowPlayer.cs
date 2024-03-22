using UnityEngine;

[ExecuteInEditMode]
public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField]
    Transform _target;
    public Transform Target
    {
        get => _target;
        set 
            {
            if (value == null) return;
            _target = value;
            Refresh();
        }
    }
    public float Speed = 10f; //0.125f;
    public Vector3 offset;

    public bool UpdateInExperienceEditorMode = false;

    private void FixedUpdate()
    {
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        if (Target == null)
            return;

        var distance = Vector3.Distance(Target.position, transform.position);
        if (distance > 1f)
        {
            Refresh();
        }
    }

    private void Refresh()
    {
        //transform.position = Target.position + offset;
        var desiredPosition = Target.position + offset;
        var smoothedPosition = PreciseLerp.Lerp(transform.position, desiredPosition, Speed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }

    private void Update()
    {
        if (UpdateInExperienceEditorMode && Application.isEditor)
        {
            UpdateCamera();
        }
    }
}