using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollPlayerCrouchState : MonoBehaviour
{
    private DollPlayerModelView modelView;

    private StateManager stateManager;

    private DollPlayerMovement playerMovement;

    private bool crouching = true;

    private void OnEnable()
    {
        modelView = GetComponentInChildren<DollPlayerModelView>();

        stateManager = GetComponent<StateManager>();

        playerMovement = GetComponent<DollPlayerMovement>();

        modelView.OnCrouch();

        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        crouching = playerMovement.crouching;

        while (crouching)
            yield return null;

        stateManager.ChangeStateString("idle");
    }

    private void OnDisable()
    {
        crouching = false;
    }
}