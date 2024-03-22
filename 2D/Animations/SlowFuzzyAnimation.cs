using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowFuzzyAnimation : MonoBehaviour
{
    public float Delta = 0.0001f;

    private int sign = 1;

    public int IntervalCount = 200;
    public int BreakInterval = 3;
    private int currentIntervalCount;
    private int currentBreakInterval;

    private void Update()
    {
        if (currentBreakInterval == 0)
        {
            currentIntervalCount++;

            if (currentIntervalCount >= IntervalCount)
            {
                currentIntervalCount = 0;
                sign *= -1;
            }
            transform.position = transform.position.Update(y: transform.position.y + Delta * sign);
        }

        currentBreakInterval++;
        if (currentBreakInterval > BreakInterval)
        {
            currentBreakInterval = 0;
        }
    }
}