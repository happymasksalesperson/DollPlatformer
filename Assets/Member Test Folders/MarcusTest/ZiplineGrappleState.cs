using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ZiplineGrappleState : MonoBehaviour
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
    [ReadOnly] public Vector2 GrappleVector;

    private void OnEnable()
    {
        middleMan.canRangeAttack = false;
        CheckForGrapplePoint();
    }

    private void CheckForGrapplePoint()
    {
        throw new NotImplementedException();
    }

    private void StartZiplineGrapple()
    {
        isGrappling = true;
        
    }
}
