using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DollPlayerJumpState : MonoBehaviour
{
    private Rigidbody rb;

    private float jumpForce;

    private DollPlayerStats stats;

    private void OnEnable()
    {
        stats = GetComponent<DollPlayerStats>();

        jumpForce = stats.jumpForce;
        
        rb.AddForce(Vector3.up*jumpForce);
    }
}
