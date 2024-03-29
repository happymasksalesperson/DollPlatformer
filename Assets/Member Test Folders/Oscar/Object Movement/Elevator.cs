using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Oscar
{
    public class Elevator : MonoBehaviour
    {
        public InElevator insideElevator;

        public Transform bottomLevel;
        public Transform topLevel;
        public float moveSpeed;
        private Vector3 targetPosition;

        public bool isGoingUp = true;
        public Rigidbody rb;

        public bool isFinished;

        void FixedUpdate()
        {
            if (insideElevator.players != null)
            {
                if (isGoingUp)
                {
                    targetPosition = topLevel.position;
                }
                else
                {
                    targetPosition = bottomLevel.position;
                }

                Vector3 direction = (targetPosition - transform.position).normalized;
                float distance = Vector3.Distance(transform.position, targetPosition);

                rb.AddForce(direction * (moveSpeed * distance * 2));

                if (distance <= 1f)
                {
                    rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                    isFinished = true;
                }
                else
                {
                    rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |
                                     RigidbodyConstraints.FreezeRotation;
                }
            }
        }

        public static event Action ElevatorMoveEvent;

        public bool ElevatorUp()
        {
            insideElevator.players[0].gameObject.transform.parent = this.transform;
            isFinished = false;
            ElevatorMoveEvent?.Invoke();
            return isGoingUp = true;
        }

        public bool ElevatorDown()
        {
            insideElevator.players[0].gameObject.transform.parent = this.transform;
            isFinished = false;
            ElevatorMoveEvent?.Invoke();
            return isGoingUp = false;
        }
    }
}