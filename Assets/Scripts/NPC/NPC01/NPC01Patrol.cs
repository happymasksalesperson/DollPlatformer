using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

public class NPC01Patrol : NPC01StateBase
{
    public float ledgeDetectionRange;

    public bool facingRight;

    public Vector3 facingDirVector;

    public float patrolSpeed;

    public float maxSpeed;

    public Transform characterTransform;

    public LayerMask layerMask;

    public bool seesPlayer;
    
    public override void OnEnable()
    {
        base.OnEnable();
        characterTransform = brain.transform;
        
        StartState();
        
        //change to patrol later
        brain.modelView.OnIdle();
    }

    public void Update()
    {
        if (enabled)
        {
            Move();
            CheckForLedge();
        }

        seesPlayer = brain.seesPlayer;
        if (seesPlayer)
            Finish();
    }

    private void Move()
    {
        if (!facingRight)
        {
            facingDirVector = new Vector3(-1, 0, 0);
        }

        else if (facingRight)
        {
            facingDirVector = new Vector3(1, 0, 0);
        }

        rb.velocity = new Vector3(facingDirVector.x * patrolSpeed, 0f, 0f);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        brain.facingRight = facingRight;
    }

    public void Flip()
    {
        facingRight = !facingRight;
        brain.FacingRight(facingRight);
    }

    private void CheckForLedge()
    {
        Vector3 raycastDirection = new Vector3(facingRight ? 1f : -1f, -1f, 0f).normalized;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, raycastDirection, out hit, ledgeDetectionRange))
        {
            ILedge ledge = hit.collider.GetComponent<ILedge>();
            IWall wall = hit.collider.GetComponentInParent<IWall>();
            if (ledge != null || wall != null)
            {
                Flip();
            }
        }
        Color rayColor = hit.collider != null && hit.collider.GetComponent<ILedge>() != null ? Color.red : Color.green;
        Debug.DrawRay(transform.position, raycastDirection * ledgeDetectionRange, rayColor);
    }

    private void Finish()
    {
        brain.patrolling = false;
    }
}