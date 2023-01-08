using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DollPlayerJumpState : MonoBehaviour
{
    private DollPlayerMovement playerMovement;
    
    private Rigidbody rb;

    public float jumpForce;

    private DollPlayerStats stats;

    private DollPlayerModelView modelView;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        
        stats = GetComponent<DollPlayerStats>();

        jumpForce = stats.jumpForce;

        modelView = GetComponentInChildren<DollPlayerModelView>();
        
        modelView?.OnJump();
        
        rb.AddForce(Vector3.up*jumpForce);
    }

    private void OnDisable()
    {
        
    }
}
