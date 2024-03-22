using UnityEngine;

public class RotateSprite : BaseBehaviour
{
    public bool CanRotate = true;
    public bool ResetTimer;
    public int RotationAngle = -90;

    [SerializeField, ReadOnlyProperty]
    private bool _hasCompletedRotation;

    private TimerCooldown timer;

    [SerializeField]
    private float timeToComplete = 1.2f;

    public bool HasCompletedRotation { get => _hasCompletedRotation; }

    /// <summary>
    /// Expressed in time ms. Updating the value causes the timer to reset.
    /// </summary>
    public float TimeToComplete
    {
        get => timeToComplete;
        set
        {
            timeToComplete = value;

            if (timer == null)
                timer = new TimerCooldown(value);
            else
                timer.Reset(value);
        }
    }

    public void Update()
    {
        if (ResetTimer)
            Reset();

        if (!CanRotate || _hasCompletedRotation)
            return;

        timer.Update();

        var percentage = Mathf.Clamp01(timer.Percentage);

        var rotationValue = -RotationAngle * percentage;

        transform.localEulerAngles = transform.localEulerAngles.Update(z: rotationValue);

        _hasCompletedRotation = percentage >= 1;
    }

    private void Reset()
    {
        ResetTimer = false;
        _hasCompletedRotation = false;
        timer.Reset(TimeToComplete);
    }

    private void Start()
    {
        if (timer == null)
            timer = new TimerCooldown(timeToComplete);
    }
}