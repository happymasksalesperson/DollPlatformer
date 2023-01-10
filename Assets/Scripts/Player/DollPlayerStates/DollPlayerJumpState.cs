using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DollPlayerJumpState : MonoBehaviour
{
    private StateManager stateManager;
    
    private DollPlayerMovement playerMovement;
    
    private Rigidbody rb;

    public float jumpForce;

    private DollPlayerStats stats;

    private DollPlayerModelView modelView;

    private bool grounded=false;
    
    public float jumpSpeedMultiplier;

    private void OnEnable()
    {
        playerMovement = GetComponent<DollPlayerMovement>();
        
        stateManager = GetComponent<StateManager>();
        
        rb = GetComponent<Rigidbody>();

        stats = GetComponent<DollPlayerStats>();

        jumpForce = stats.jumpForce;

        jumpSpeedMultiplier = stats.jumpSpeedMultipler;
        
        modelView = GetComponentInChildren<DollPlayerModelView>();
        
        modelView?.OnJump();
        
        
        //BOING!!
        //
        rb.AddForce((Vector3.up * jumpForce) + (rb.velocity * jumpSpeedMultiplier), ForceMode.Impulse);
        
        rb.AddForce(Vector3.up*jumpForce);

        StartCoroutine(Jump());
    }

    private IEnumerator Jump()
    {
        grounded = playerMovement.grounded;
        
        while (!grounded)
            yield return null;
        
        playerMovement.jumping = false;
    }

    private void OnDisable()
    {
        stateManager.ChangeStateString("idle");
    }
}
