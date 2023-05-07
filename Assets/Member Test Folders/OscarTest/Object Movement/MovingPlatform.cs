using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform bottomLevel;
    public Transform topLevel;
    public float moveSpeed;

    public bool isGoingUp = true;
    public Rigidbody rb;
    
    public bool CallElevator()
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

        rb.AddRelativeForce(direction * (moveSpeed * distance));

        if (distance < 0.1f)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            rb.constraints = ~RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }
}
