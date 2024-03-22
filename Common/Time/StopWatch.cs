using System;
using UnityEngine;

public class StopWatch : BaseBehaviour
{
    public float ElapsedTime { get; private set; }

    private void Update()
    {
        if (!enabled) return;

        ElapsedTime += Time.deltaTime;
    }

    [SerializeField]
    private Signs sign;

    public enum Signs
    {
        P= -1,
        Up = 1
    }

    internal void Reset()
    {
        ElapsedTime = 0;
        throw new NotImplementedException();
    }
}