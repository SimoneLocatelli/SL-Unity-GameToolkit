using System;
using System.Collections.Generic;

public class StateUpdaterCollection<TState> : Dictionary<TState, Func<TState>>
{
    public Func<TState> GetValueOrDefault(TState state)
    {
        if (TryGetValue(state, out Func<TState> value))
            return value;

        return null;
    }
}