using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC02ConjoinedKnockback : MonoBehaviour
{
    private GroundCheck groundCheck;

    private StateManager stateManager;

    private bool grounded = false;

    private Rigidbody rb;
    
    [SerializeField] private float verticalDist;

    [SerializeField] private float horizontalDist;
    
    private void OnEnable()
    {
        groundCheck = GetComponentInChildren<GroundCheck>();

        stateManager = GetComponent<StateManager>();

        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(horizontalDist, verticalDist, 0), ForceMode.Impulse);
        
        StartCoroutine(WaitUntilGrounded());
        
        IEnumerator WaitUntilGrounded()
        {
            while (!grounded)
            {
                yield return null;
            }
            
            stateManager.ChangeStateString("patrol");
        }
    }

    private void FixedUpdate()
    {
        grounded = groundCheck.isGrounded;
    }
}
