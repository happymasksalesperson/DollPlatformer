using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public Rigidbody rb;
    public float maxSpeed = 10.0f; // Maximum speed cap
    private Vector3 newVelocity;

    public bool MoveTheBounds;

    public float moveSpeed;

    public bool directionLeft;
    
    private void Update()
    {
        if (MoveTheBounds)
        {
            if (directionLeft)
            {
                rb.AddForce(-moveSpeed, 0, 0);
            }
            else
            {
                rb.AddForce(moveSpeed, 0, 0);
            }
            
            newVelocity = rb.velocity;
            newVelocity.x = Mathf.Clamp(newVelocity.x, -maxSpeed, maxSpeed);
            rb.velocity = newVelocity;
        }
    }
}
