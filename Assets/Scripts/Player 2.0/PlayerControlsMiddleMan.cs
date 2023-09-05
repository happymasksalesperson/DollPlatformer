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

    public bool canJump;

    public bool canRangeAttack = true;

    public bool moving = false;

    public Rigidbody rb;

    public BoxCollider boxCollider;

    public HealthModel health;

    public void OnEnable()
    {
        playerControls.JumpEvent += JumpState;
        health.ChangeHealthEvent += TakeDamage;
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
                    moving = true;
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
                    moving = true;
                    modelView.OnFacingRight(previous);
                }
            }
            else
                moving = false;
        }

        if (!grounded && rb.velocity.y > 0)
            boxCollider.enabled = false;

        else
            boxCollider.enabled = true;
    }

    public void JumpState()
    {
        if (!canJump)
            return;

        if(inControl)
        stateManager.ChangeState(PlayerStates.Jump);
    }

    public void TakeDamage(float amount)
    {
        if(amount < 0)
            stateManager.ChangeState(PlayerStates.TakeDamage);
    }

    public void OnDisable()
    {
        playerControls.JumpEvent -= JumpState;
    }
}
