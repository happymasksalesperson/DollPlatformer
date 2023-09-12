using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : HelpfulFunctions
{
    public PlayerBrain playerModel;
    public event Action FallEvent;
    private void OnEnable()
    {
        FallEvent?.Invoke();
    }

    private void Update()
    {
        if (isGrounded(transform.position))
        {
            playerModel.fallBool = false;
        }
    }
}
