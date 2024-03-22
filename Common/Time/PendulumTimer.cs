using UnityEngine;

public class PendulumTimer
{
    public float Phase = 1;
    private float _elapsedTime;

    public PendulumTimer(float cooldown)
    {
        Cooldown = cooldown;
    }

    public float Cooldown { get; set; }

    public float ElapsedTime { get => _elapsedTime; }

    public float InversePercentage { get => 1 - Percentage; }

    public bool Looping { get; set; }

    public float Percentage { get =>Mathf.Clamp01(ElapsedTime / Cooldown); }

    public void InvertPhase()
    {
        Phase *= -1;
    }

    public void Update(float time)
    {
        Mathf.Clamp(_elapsedTime, 0, Cooldown);

        if (Phase > 0)
        {
            if (_elapsedTime >= Cooldown)
            {
                if (!Looping)
                    return;

                InvertPhase();
            }
        }
        else
        {
            if (_elapsedTime <= 0)
            {
                InvertPhase();
            }
        }

        _elapsedTime += time * Phase;
    }
}