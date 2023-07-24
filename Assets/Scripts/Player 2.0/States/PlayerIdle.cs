using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : MonoBehaviour
{
    public PlayerControls controls;

    public PlayerStateManager stateManager;

    public GroundCheck groundCheck;

    public bool grounded;
    
    public void Update()
    {
        grounded = groundCheck.grounded;
        
        if(!grounded)
            stateManager.ChangeState(PlayerStates.Fall);
        
        else if(controls.aimInput <0)
            stateManager.ChangeState(PlayerStates.Crouch);
    }
}