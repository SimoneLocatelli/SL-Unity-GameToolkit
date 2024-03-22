public abstract class TimerBehaviour : BaseBehaviour
{
    #region Properties

    public float Cooldown = 0.5f;
    private TimerCooldown Timer;
    public bool IsReady { get; private set; }

    #endregion Properties

    #region Methods

    protected abstract void OnTimerReady();

    protected void ResetTimer()
    {
        Timer.Reset(Cooldown);
    }

    #endregion Methods

    #region LifeCycle

    protected virtual void Awake()
    {
        Timer = new TimerCooldown(0);
    }

    protected virtual void Update()
    {
        IsReady = Timer.IsReady;

        if (IsReady)
        {
            OnTimerReady();
        }
        else
        {
            Timer.Update();
            Timer.Reset();
        }

    }

    #endregion LifeCycle
}