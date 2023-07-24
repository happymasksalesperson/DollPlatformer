using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollPlayerFallState : MonoBehaviour
{
    private DollPlayerMovement playerMovement;
    
    private DollPlayerModelView modelView;

    private StateManager stateManager;

    private bool grounded;
    
    private void OnEnable()
    {
        modelView = GetComponentInChildren<DollPlayerModelView>();
        modelView.OnJump();

        playerMovement = GetComponent<DollPlayerMovement>();

        stateManager = GetComponent<StateManager>();

    //    playerMovement._gravity.enabled = true;
    }

    private void FixedUpdate()
    {
        grounded = playerMovement.IsGrounded();
        if(grounded)
            playerMovement.JumpEnd();
    }
}
