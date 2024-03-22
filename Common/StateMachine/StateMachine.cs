using UnityEngine;

public class StateMachine<TState> where TState : struct, System.Enum
{
    private TState currentState;
    private bool stateInitialised;

    public StateInitialiserCollection<TState> StatesInitialisers = new StateInitialiserCollection<TState>();
    public StateUpdaterCollection<TState> StatesUpdaters = new StateUpdaterCollection<TState>();

    public TState CurrentState { get => currentState; }

    public void SetState(TState newState)
    {
        if (currentState.Equals(newState)) return;
        stateInitialised = false;
        currentState = newState;
    }

    public void Update()
    {
        if (!stateInitialised)
        {
            var statesInitialisers = StatesInitialisers.GetValueOrDefault(currentState);
            statesInitialisers?.Invoke();
            stateInitialised = true;
        }

        var statesUpdater = StatesUpdaters.GetValueOrDefault(currentState);

        if (statesUpdater == null) return;

        var newState = statesUpdater.Invoke();
        SetState(newState);
    }
}