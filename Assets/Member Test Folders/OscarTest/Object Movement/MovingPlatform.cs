using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class MovingPlatform : MonoBehaviour
    {
        public Transform bottomLevel;
        public Transform topLevel;
        public float moveSpeed;

        public bool isGoingUp = true;
        public Rigidbody rb;

        public bool ChangeDirection()
        {
            if (isGoingUp)
            {
                isGoingUp = false;
            }
            else
            {
                isGoingUp = true;
            }

            return isGoingUp;
        }

        void FixedUpdate()
        {
            Vector3 targetPosition;

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

            rb.AddRelativeForce(direction * (moveSpeed * distance * 2));

            if (distance <= 1f)
            {
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                ChangeDirection();
            }
            else
            {
                rb.constraints = ~RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY |
                                 RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            }
        }
    }
}