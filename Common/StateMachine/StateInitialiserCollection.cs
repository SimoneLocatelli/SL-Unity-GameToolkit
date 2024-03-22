using System;
using System.Collections.Generic;

public class StateInitialiserCollection<TState> : Dictionary<TState, Action>
{
    public Action GetValueOrDefault(TState state)
    {
        if (TryGetValue(state, out Action value))
            return value;

        return null;
    }
}