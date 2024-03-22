using System;
using UnityEngine;

public class InputMovement2D : BaseMovement2D
{
    public bool UseLegacyInputSystem = false;

    public void Update()
    {
        if (UseLegacyInputSystem)
        {
            var movement = new Vector2
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = Input.GetAxisRaw("Vertical")
            };
            UpdateMovement(movement);
        }
    }

}