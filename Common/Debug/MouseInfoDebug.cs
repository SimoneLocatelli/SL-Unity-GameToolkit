using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInfoDebug : MonoBehaviour
{
    private readonly MouseSystem mouseSystem = new MouseSystem();

    [ReadOnlyProperty]
    public Vector2 ScreenSize;

    [ReadOnlyProperty]
    public Vector2 MousePosition;

    [ReadOnlyProperty]
    public bool IsInsideGameScreen;

    void Update()
    {
        ScreenSize = new Vector2(Screen.width, Screen.height);
        IsInsideGameScreen = mouseSystem.IsInsideGameScreen(out MousePosition);
    }
}
