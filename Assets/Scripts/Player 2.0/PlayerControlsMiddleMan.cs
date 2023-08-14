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

    public bool inControl=true;

    public bool grounded;

    public bool facingRight;
    
    private bool previous;

    public Rigidbody rb;

    public BoxCollider boxCollider;

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

        if (inControl)
        {
            if (playerControls.movementInput < 0)
            {
                if (previous)
                {
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                    facingRight = false;
                    previous = false;
                    modelView.OnFacingRight(previous);
                }
            }
            else if (playerControls.movementInput > 0)
            {
                if (!previous)
                {
                    transform.rotation = new Quaternion(0, 180, 0, 0);

                    facingRight = true;
                    previous = true;
                    modelView.OnFacingRight(previous);
                }
            }
        }

        if (!grounded && rb.velocity.y > 0)
            boxCollider.enabled = false;

        else
            boxCollider.enabled = true;
    }

    public void JumpState()
    {
        if (stateManager.currentState == PlayerStates.Jump || stateManager.currentState == PlayerStates.Fall)
            return;

        if(inControl)
        stateManager.ChangeState(PlayerStates.Jump);
    }

    public void OnDisable()
    {
        playerControls.JumpEvent -= JumpState;
    }
}
