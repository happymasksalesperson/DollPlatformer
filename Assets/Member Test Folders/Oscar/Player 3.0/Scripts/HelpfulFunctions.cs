using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HelpfulFunctions : MonoBehaviour
{
    /// <HelpfullFunctionsInfo>
    /// This script will be created so we dont need to have multiple
    /// instances of the same function.
    /// This way a script can inherit the function they require and read
    /// what they need from it/run the function without having extra code.
    /// </HelpfullFunctionsInfo>

    public Rigidbody rb;
    
    public float speed;

    private float desiredVelocity;
    private float velocityChange;
    
    public float jumpTime;
    public float jumpForce;
    private Vector2 jumpVector;
    private Coroutine jumpCoroutine;
    private float timer;

    public LayerMask groundLayer;
    public LayerMask damageLayer;
    public float checkSphereRadius;
    
    public void Movement(float moveDirection)
    {
        desiredVelocity = moveDirection * speed;

        velocityChange = desiredVelocity - rb.velocity.x;

        rb.AddForce(new Vector2(velocityChange * rb.mass / Time.fixedDeltaTime, 0f));
    }
    
    /// <isGrounded>
    /// Can be used to check if it is grounded or not
    /// </isGrounded>
    /// <Transform="transformLoc"></Transform>
    /// <returns>Bool for whether its grounded or not</returns>
    public bool isGrounded(Vector3 position)
    {
        return Physics.CheckSphere(position, checkSphereRadius, groundLayer);
    }

    /// <Jump>
    /// Can be used for anything that needs to jump
    /// </Jump>
    /// <Bool="jumpBool"> Can the player jump or not</Bool>
    public void Jump()
    {
        //rb.velocity = Vector2.zero;
        timer = 0;
        while (timer < jumpTime)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
            timer += Time.deltaTime;
        }
    }

    /// <CloseAttack>
    /// Can be used for anything that has a close attack
    /// </CloseAttack>
    /// <Transform="transformLoc"> where to attack plus offset for direction</Transform>
    /// <Int="attackDamage">How much damage to deal</Int>
    /// <float="attackDamage">How big the damage area is</float>
    public void CloseAttack(Transform transformLoc,int attackDamage, float attackSize)
    {
        RaycastHit[] hits = new RaycastHit[10];
        Vector3 boxHalfExtents = new Vector3(attackSize, attackSize, attackSize);
        int numCollisions = Physics.BoxCastNonAlloc(transformLoc.position, boxHalfExtents, Vector3.forward, hits, Quaternion.identity, 0f, damageLayer, QueryTriggerInteraction.Collide);

        if (numCollisions > 0)
        {
            for (int i = 0; i < numCollisions; i++)
            {
                RaycastHit hit = hits[i];
                ITakeDamage damageReceiver = hit.collider.GetComponent<ITakeDamage>();
                if (damageReceiver != null)
                {
                    // health will be modified to keep in mind for this function
                    damageReceiver.ChangeHP(-attackDamage);
                    return;
                }
            }
        }
    }
}
