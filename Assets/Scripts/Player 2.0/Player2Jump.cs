using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Player2Jump : MonoBehaviour
{
    [Header("Custom Amounts")] public float jumpTime;

    public float jumpForce;

    [Header("Read Data")] [ReadOnly] public Vector2 jumpVector;
    public Rigidbody rb;

    public PlayerControls playerControls;

    public bool jumping;

    public PlayerStateManager stateManager;

    public float timer;

    public GroundCheck groundCheck;

    public bool grounded;

    public PlayerControlsMiddleMan middleMan;

    public void OnEnable()
    {
        middleMan.canRangeAttack = false;
        middleMan.canJump = false;
        StartJump();
    }
    
    public void StartJump()
    {
        jumping = true;
        jumpVector = new Vector2(0, jumpForce);
        StartCoroutine(JumpRoutine());
    }

    private void Update()
    {
        grounded = groundCheck.grounded;
        if (!grounded)
            middleMan.canRangeAttack = true;
    }

    private IEnumerator JumpRoutine()
    {
        rb.velocity = Vector2.zero;
           timer = 0;
        while (playerControls.jumpHeld && timer < jumpTime)
        {
            float proportionCompleted = timer / jumpTime;
            Vector2 thisFrameJumpVector = Vector2.Lerp(jumpVector, Vector2.zero, proportionCompleted);
            rb.AddForce(thisFrameJumpVector);
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        jumping = false;
        
        if(grounded)
            stateManager.ChangeState(PlayerStates.Idle);

        else
        {
            stateManager.ChangeState(PlayerStates.Fall);
        }
    }
}