using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DemoMovementTest : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        if (Keyboard.current[Key.A].wasPressedThisFrame)
        {
            Vector3 movement = new Vector3(transform.position.x - (moveSpeed * Time.deltaTime), 0f, 0f) * (moveSpeed * Time.deltaTime);
            rb.AddForce(movement);
        }

        if (Keyboard.current[Key.D].wasPressedThisFrame)
        {
            Vector3 movement = new Vector3(transform.position.x + (moveSpeed * Time.deltaTime), 0f, 0f) * (moveSpeed * Time.deltaTime);
            rb.AddForce(movement);
        }
    }
}
