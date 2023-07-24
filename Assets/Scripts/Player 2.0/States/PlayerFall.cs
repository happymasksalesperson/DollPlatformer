using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : MonoBehaviour
{
    public GroundCheck groundCheck;

    public PlayerStateManager stateManager;

    public PlayerControls controls;

    public bool grounded;

    private void OnEnable()
    {
        grounded = false;
    }

    private void Update()
    {
        grounded = groundCheck.grounded;

        if (grounded && controls.movementInput == 0)
            stateManager.ChangeState(PlayerStates.Idle);
        
        else if (grounded)
            stateManager.ChangeState(PlayerStates.Run);
    }
}