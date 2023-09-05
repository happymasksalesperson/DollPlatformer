using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

/// <summary>
/// Both grapple types can be canceled from a jump, retaining their momentum
/// </summary>
public class GrappleState : MonoBehaviour
{
    [Header("Important Shit")]
    public PlayerStateManager stateManager;
    public PlayerControlsMiddleMan middleMan;
    
    public GroundCheck groundCheck;
    public bool grounded;

    [Header("State Shit")]
    public Rigidbody rb;
    public PlayerControls controls;
    public bool isGrappling;
    [Tooltip("Do Not Change")]
    public Vector2 GrappleVector;
    public GrapplePoint nearestPoint;

    private void OnEnable()
    {
        middleMan.canRangeAttack = false;
        middleMan.canJump = true;
        CheckGrappleType();
    }
    
    private void CheckGrappleType()
    {
        if (!isGrappling)
        {
            if (nearestPoint.pointType == GrappleType.swing)
                StartSwingGrapple();
            else if (nearestPoint.pointType == GrappleType.zipline)
                StartZiplineGrapple();
        }
    }

    private void StartSwingGrapple()
    {
        isGrappling = true;
    }

    private void StartZiplineGrapple()
    {
        isGrappling = true;
    }

    void BreakGrappleLine()
    {
        isGrappling = false;
        stateManager.ChangeState(PlayerStates.Fall);
    }
}
