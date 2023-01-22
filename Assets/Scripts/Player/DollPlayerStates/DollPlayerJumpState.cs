using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DollPlayerJumpState : MonoBehaviour
{
    private DollPlayerMovement playerMovement;
    private DollPlayerStats playerStats;
    private StateManager stateManager;

    private DollPlayerModelView modelView;

    private Gravity gravity;
    public float gravityScale;
    private float defaultScale;
    public float gravityMultiplier;

    public AnimationCurve jumpCurve;
    public float jumpForce;
    public float jumpTime;

    public Rigidbody rb;
    public bool holdingJump;
    private float timeElapsed;

    public bool grounded;

    private void OnEnable()
    {
        grounded = false;

        holdingJump = true;

        rb = GetComponent<Rigidbody>();

        playerMovement = GetComponent<DollPlayerMovement>();
        playerStats = GetComponent<DollPlayerStats>();
        stateManager = GetComponent<StateManager>();
        gravity = GetComponent<Gravity>();
        defaultScale = gravity.CurrentGravity();
        modelView = GetComponentInChildren<DollPlayerModelView>();
        modelView.OnJump();

        jumpForce = playerStats.jumpForce;

        timeElapsed = 0f;

        Jump();
        StartCoroutine(WaitForGrounded());
    }

    IEnumerator WaitForGrounded()
    {
        while (!grounded)
        {
            yield return null;
        }

        playerMovement.JumpEnd();
        stateManager.ChangeStateString("idle");
    }

    void FixedUpdate()
    {
        grounded = playerMovement.grounded;
        holdingJump = playerMovement.holdingJump;
        if (holdingJump)
        {
            gravityScale = defaultScale * gravityMultiplier;

            gravity.ChangeGravity(gravityScale);
        }
    }

    private void Jump()
    {
        timeElapsed += Time.deltaTime;
        if (holdingJump && timeElapsed < jumpTime)
        {
            float jumpForceMultiplier = jumpCurve.Evaluate(timeElapsed / jumpTime);
            rb.AddForce(Vector3.up * jumpForce * jumpForceMultiplier, ForceMode.Acceleration);
        }
    }

    private void OnDisable()
    {
        holdingJump = false;
        grounded = false;
        gravity.ChangeGravity(defaultScale);
    }
}