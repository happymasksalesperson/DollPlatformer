using System;
using UnityEngine;

namespace Player_2._0
{
    public class Player2Run : MonoBehaviour
    {
        public Rigidbody rb;
        public float runSpeed;
        public float maxSpeed;
        public PlayerControls playerControls;
        public PlayerStateManager stateManager;
        public GroundCheck groundCheck;
        private bool grounded;

        public bool hasStateChanged = false;
        
        public bool wasMoving;

        public bool isMoving;

        public void FixedUpdate()
        {
            if (stateManager.currentState == PlayerStates.Crouch || stateManager.currentState == PlayerStates.Slide)
                return;

            grounded = groundCheck.grounded;

            rb.velocity = new Vector3(playerControls.movementInput * runSpeed, 0, 0);

            isMoving = playerControls.movementInput != 0;

            if (grounded && !wasMoving && isMoving)
            {
                stateManager.ChangeState(PlayerStates.Run);
            }

            else if (grounded && wasMoving && !isMoving)
            {
                stateManager.ChangeState(PlayerStates.Idle);
            }

            wasMoving = isMoving;

            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }
}