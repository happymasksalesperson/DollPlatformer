using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC01Jump : MonoBehaviour
{
    public NPC01Brain brain;

    public Rigidbody rb;

    public float jumpForce;

    public Gravity gravity;

    public float gravityWaitTime;

    private int playerLayerIndex;
    private int groundLayerIndex;

    public bool grounded;

    public bool checking;

    public void OnEnable()
    {
        grounded = false;
        checking = false;
        brain = GetComponentInParent<NPC01Brain>();
        gravity = brain.gravity;
        brain.jumping = true;
        rb = GetComponentInParent<Rigidbody>();

        playerLayerIndex = LayerMask.NameToLayer("NPC");
        groundLayerIndex = LayerMask.NameToLayer("Ground");
        
        StartCoroutine(Jump());
    }

    public void Update()
    {
        if (rb.velocity.y > 0)
        {
            Physics.IgnoreLayerCollision(playerLayerIndex, groundLayerIndex, true);
        }
        else
        {
            Physics.IgnoreLayerCollision(playerLayerIndex, groundLayerIndex, false);
        }

        if (checking)
        {
            grounded = brain.groundCheck.grounded;
            if (grounded)
            {
                brain.jumping = false;
            }
        }
    }

    private IEnumerator Jump()
    {
        rb.AddForce(new Vector3(rb.velocity.x, 1 * jumpForce, 0), ForceMode.Impulse);

        yield return new WaitForSeconds(gravityWaitTime);

        gravity.enabled = true;
        checking = true;
    }
}