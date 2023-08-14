using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : MonoBehaviour
{
    public GroundCheck groundCheck;

    public PlayerStateManager stateManager;

    public PlayerControls controls;

    public bool grounded;

    public Rigidbody rb;

    public float fallSpeedMultiplier;

    public float fallTimeThreshold;

    public float fallTime;
    
    public PlayerControlsMiddleMan middleMan;

    public bool slowFall = false;
    public float slowFallForce = 20f;
    
    public LayerMask slowFallLayer;
    public float raycastDistance = 1.0f;
    
    public float[] raycastOffsets = { -1f, 1f };

    private void OnEnable()
    {
        grounded = false;
        fallTime = 0;
    }


    private void Update()
    {
        grounded = groundCheck.grounded;
        middleMan.canJump = false;

        if (!grounded)
        {
            slowFall = false;

            foreach (float offset in raycastOffsets)
            {
                Vector3 raycastPosition = transform.position + new Vector3(offset, 0f, 0f);
                RaycastHit hit;
                if (Physics.Raycast(raycastPosition, Vector3.down, out hit, raycastDistance, slowFallLayer))
                {
                    slowFall = true;
                    break;

                }
            }
            
            if (slowFall)
            {
                rb.AddForce(Vector3.up * slowFallForce, ForceMode.Acceleration);
            }

            Vector3 fallForce = new Vector3(0f, -fallSpeedMultiplier, 0f);
            rb.AddForce(fallForce, ForceMode.Acceleration);

            fallTime += Time.deltaTime;
        }

        if (grounded)
        {
            middleMan.canJump = true;

            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (fallTime > fallTimeThreshold)
                stateManager.DeclareMoment(PlayerMoments.LandedHard);

            else
                stateManager.DeclareMoment(PlayerMoments.LandedSoft);

            if (controls.movementInput == 0)
                stateManager.ChangeState(PlayerStates.Idle);

            else
                stateManager.ChangeState(PlayerStates.Run);
        }
    }
}