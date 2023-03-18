using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollPlayerCrouchState : MonoBehaviour
{
    private DollPlayerModelView modelView;

    private StateManager stateManager;

    private DollPlayerMovement playerMovement;

    private void OnEnable()
    {
        modelView = GetComponentInChildren<DollPlayerModelView>();

        stateManager = GetComponent<StateManager>();

        playerMovement = GetComponent<DollPlayerMovement>();

        modelView.OnCrouch();
        modelView.OnChangeState(State.Crouch);
    }

    private void OnDisable()
    {
    }
}