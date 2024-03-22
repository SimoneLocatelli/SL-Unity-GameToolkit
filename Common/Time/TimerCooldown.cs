using System;
using UnityEngine;

[Serializable]
public class TimerCooldown
{
    #region Properties

    private float _cooldown;
    private float elapsedTime;

    public static TimerCooldown ZeroCooldown { get => new TimerCooldown(0); }

    public float Cooldown
    {
        get => _cooldown;
        set
        {
            CustomAssert.IsNotNegative(_cooldown, nameof(_cooldown));
            _cooldown = value;
            IsReady = elapsedTime >= Cooldown;
        }
    }

    public float ElapsedTime { get => elapsedTime; }
    public float InversePercentage { get => 1 - Percentage; }

    public bool IsReady { get; private set; }
    public float Percentage { get => ElapsedTime / Cooldown; }

    #endregion Properties

    #region Constructor

    public TimerCooldown(float cooldown)
    {
        Cooldown = cooldown;
    }

    #endregion Constructor

    #region Methods

    public void Reset(float newCooldown)
    {
        Cooldown = newCooldown;
        Reset();
    }

    public void Reset()
    {
        elapsedTime = 0;
        IsReady = false;
    }

    public void Update()
    {
        if (!IsReady)
            elapsedTime += Time.deltaTime;

        IsReady = elapsedTime >= Cooldown;
    }

    #endregion Methods
}