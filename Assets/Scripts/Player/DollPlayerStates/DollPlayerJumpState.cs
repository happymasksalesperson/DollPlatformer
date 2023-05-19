using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
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

    private float horizontalMultiplier;

    public AnimationCurve jumpCurve;
    [ReadOnly]
    public float jumpForce;
    [ReadOnly]
    public float jumpTime;

    public Rigidbody rb;
    public bool holdingJump;
    private float timeElapsed;

    public bool grounded;
    
    public LayerMask playerLayer;
    public LayerMask groundLayer;

    private void OnEnable()
    {
        grounded = false;

        holdingJump = true;

        rb = GetComponent<Rigidbody>();

        playerMovement = GetComponent<DollPlayerMovement>();
        playerStats = GetComponent<DollPlayerStats>();
        horizontalMultiplier = playerStats.jumpSpeedMultipler;

        stateManager = GetComponent<StateManager>();
       gravity = GetComponent<Gravity>();
       gravity.enabled = true;
       // defaultScale = gravity.CurrentGravity();
        modelView = GetComponentInChildren<DollPlayerModelView>();
        modelView.OnJump();

        playerMovement.canTalk = false;

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
    }

    void FixedUpdate()
    {
        grounded = playerMovement.grounded;
        /*holdingJump = playerMovement.holdingJump;
        if (holdingJump)
        {
            gravityScale = defaultScale * gravityMultiplier;

            gravity.ChangeGravity(gravityScale);
        }*/
        
        int playerLayerIndex = LayerMask.NameToLayer("Player");
        int groundLayerIndex = LayerMask.NameToLayer("Ground");
        
        if (rb.velocity.y > 0)
        {
            Physics.IgnoreLayerCollision(playerLayerIndex, groundLayerIndex, true);
        }
        else
        {
            Physics.IgnoreLayerCollision(playerLayerIndex, groundLayerIndex, false);
        }
    }

    private void Jump()
    {
        rb.AddForce(new Vector3(rb.velocity.x, 1 * jumpForce, 0), ForceMode.Impulse);
        /*timeElapsed += Time.deltaTime;
        if (holdingJump && timeElapsed < jumpTime)
        {
            float jumpForceMultiplier = jumpCurve.Evaluate(timeElapsed / jumpTime);
            rb.AddForce(new Vector3(rb.velocity.x * horizontalMultiplier, jumpForce * jumpForceMultiplier, 0), ForceMode.Acceleration);
        }*/
    }

    private void OnDisable()
    {
        holdingJump = false;
        grounded = false;
//        gravity.ChangeGravity(defaultScale);
    }
}