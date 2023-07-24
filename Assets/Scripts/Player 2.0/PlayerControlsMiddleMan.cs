using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlsMiddleMan : MonoBehaviour
{
    public PlayerControls playerControls;

    public PlayerStateManager stateManager;

    public PlayerModelView modelView;

    public GroundCheck groundCheck;

    public bool grounded;

    public bool facingRight;
    
    private bool previous;

    public Rigidbody rb;

    public void OnEnable()
    {
        playerControls.JumpEvent += JumpState;
    }
    
    public void Update()
    {
        grounded = groundCheck.grounded;
        if (grounded)
        {
            rb.velocity = Vector3.zero;
        }
        
        if (playerControls.movementInput < 0)
        {
            if (previous)
            {
                facingRight = false;
                previous = false;
                modelView.OnFacingRight(previous);
            }
        }
        else if (playerControls.movementInput > 0)
        {
            if (!previous)
            {
                facingRight = true;
                previous = true;
                modelView.OnFacingRight(previous);
            }
        }
    }

    public void JumpState()
    {
        stateManager.ChangeState(PlayerStates.Jump);
    }

    public void OnDisable()
    {
        playerControls.JumpEvent -= JumpState;
    }
}
