using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DollPlayerJumpState : MonoBehaviour
{
    private Rigidbody rb;

    public float jumpForce;

    private DollPlayerStats stats;

    private DollPlayerMovement playerMovement;

    private DollPlayerModelView modelView;

    public float jumpSpeedMultiplier;

    private StateManager stateManager;

    private void OnEnable()
    {
        stateManager = GetComponent<StateManager>();

        rb = GetComponent<Rigidbody>();

        stats = GetComponent<DollPlayerStats>();

        jumpForce = stats.jumpForce;

        jumpSpeedMultiplier = stats.jumpSpeedMultipler;

        playerMovement = GetComponent<DollPlayerMovement>();

        modelView = GetComponentInChildren<DollPlayerModelView>();

        modelView?.OnJump();

        //BOING!!
        //
        if (rb.velocity == Vector3.zero)
            rb.AddForce((Vector3.up * jumpForce), ForceMode.Impulse);

        else
            rb.AddForce((Vector3.up * jumpForce) + (rb.velocity * jumpSpeedMultiplier), ForceMode.Impulse);

        StartCoroutine(Jump());
    }

    private IEnumerator Jump()
    {
        yield return new WaitUntil(() => !playerMovement.grounded);

        stateManager.ChangeStateString("idle");
    }
}