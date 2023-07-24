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

    private void Start()
    {
        playerControls.JumpEvent += StartJump;
    }

    public void StartJump()
    {
        if (jumping)
            return;

        jumping = true;
        jumpVector = new Vector2(0, jumpForce);
        StartCoroutine(JumpRoutine());
    }

    private void FixedUpdate()
    {
        if(jumping)
            timer += Time.deltaTime;

        else
        {
            timer = 0;
        }
    }

    private IEnumerator JumpRoutine()
    {
        rb.velocity = Vector2.zero;

        while (playerControls.jumpHeld && timer < jumpTime)
        {
            float proportionCompleted = timer / jumpTime;
            Vector2 thisFrameJumpVector = Vector2.Lerp(jumpVector, Vector2.zero, proportionCompleted);
            rb.AddForce(thisFrameJumpVector);
            yield return null;
        }

        jumping = false;
    }
}