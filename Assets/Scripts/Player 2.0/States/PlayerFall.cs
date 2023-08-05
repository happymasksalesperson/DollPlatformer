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

    private void OnEnable()
    {
        grounded = false;
        fallTime = 0;
    }

    private void Update()
    {
        grounded = groundCheck.grounded;

        //apply forces while in air to fall faster
        if (!grounded)
        {
            Vector3 fallForce = new Vector3(0f, -fallSpeedMultiplier, 0f);
            rb.AddForce(fallForce, ForceMode.Acceleration);

            fallTime += Time.deltaTime;
        }

        if (grounded)
        {
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