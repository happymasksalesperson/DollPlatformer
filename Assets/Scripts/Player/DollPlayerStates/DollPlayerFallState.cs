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

        stateManager = GetComponent<StateManager>();
    }

    private void FixedUpdate()
    {
        
        grounded = playerMovement.grounded;
        if(!grounded)
            //"land"
            stateManager.ChangeStateString("idle");
    }
}
