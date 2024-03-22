using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Lighter version of the <see cref="BaseBehaviour"/> class
/// containing only the most essentials properties, methods and so on.
/// </summary>
public abstract class BaseBehaviourLight : MonoBehaviour
{
    #region Properties

    [Header("Logging", order = 10000)]
    public bool EnableDebugLog = false;

    #endregion Properties

    #region Methods - GetInitialisedComponent

    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining), DebuggerStepThrough]
    protected static TComponent GetInitialisedComponent<TComponent>(Behaviour behaviour, ref TComponent innerReference) where TComponent : Component
    {
        if (innerReference == null)
            innerReference = behaviour.GetComponent<TComponent>();

        return innerReference;
    }

    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining), DebuggerStepThrough]
    protected TComponent GetInitialisedComponent<TComponent>() where TComponent : Component
    {
        var component = GetComponent<TComponent>();

        Assert.IsNotNull(component, $"Couldn't find component {typeof(TComponent).FullName} on object {gameObject.name}");

        return component;
    }

    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining), DebuggerStepThrough]
    protected TComponent GetInitialisedComponent<TComponent>(ref TComponent innerReference) where TComponent : Component
        => innerReference != null ? innerReference : (innerReference = GetInitialisedComponent<TComponent>());

    #endregion Methods - GetInitialisedComponent

    #region Methods - Logging

    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining), DebuggerStepThrough, Conditional("DEBUG")]
    protected void DebugLog(object message, bool condition = true)
    => DebugLog(message, 0, condition);

    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining), DebuggerStepThrough, Conditional("DEBUG")]
    protected void DebugLog(object message, int indentationSpaces, bool condition = true)
    {
        if (!EnableDebugLog || !condition)
            return;

        var spaces = new string(' ', indentationSpaces);
        UnityEngine.Debug.Log($"{spaces}{GetType().FullName} - {message}", this);
    }

    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining), DebuggerStepThrough, Conditional("DEBUG")]
    protected void DebugLogMethodEntry([CallerMemberName] string message = null)
        => DebugLog($"Entry Method - {message}");

    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining), DebuggerStepThrough, Conditional("DEBUG")]
    protected void DebugLogMethodExit([CallerMemberName] string message = null)
        => DebugLog($"Entry Method - {message}");

    #endregion Methods - Logging
}