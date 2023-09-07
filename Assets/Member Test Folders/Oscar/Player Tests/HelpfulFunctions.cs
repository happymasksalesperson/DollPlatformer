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
    
    private float speed;

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
    
    
    public void Movement(float moveDirection, float maxSpeed)
    {
        desiredVelocity = moveDirection * maxSpeed;

        velocityChange = desiredVelocity - rb.velocity.x;

        rb.AddForce(new Vector2(velocityChange * rb.mass / Time.fixedDeltaTime, 0f));
    }

    

    /// <isGrounded>
    /// Can be used to check if it is grounded or not
    /// </isGrounded>
    /// <Transform="transformLoc"></Transform>
    /// <returns>Bool for whether its grounded or not</returns>
    private bool isGrounded(Transform transformLoc)
    {
        return Physics.CheckSphere(transformLoc.position, checkSphereRadius, groundLayer);
    }

    /// <Jump>
    /// Can be used for anything that needs to jump
    /// </Jump>
    /// <Bool="jumpBool"> Can the player jump or not</Bool>
    public void Jump(bool jumpBool)
    {
        jumpCoroutine = StartCoroutine(JumpRoutine(jumpBool));
    }
    
    private IEnumerator JumpRoutine(bool jumpHeld)
    {
        // might remove the jumpHeld bool if the stop coroutine works well.
        rb.velocity = Vector2.zero;
        timer = 0;
        while (jumpHeld && timer < jumpTime)
        {
            float proportionCompleted = timer / jumpTime;
            Vector2 thisFrameJumpVector = Vector2.Lerp(jumpVector, Vector2.zero, proportionCompleted);
            rb.AddForce(thisFrameJumpVector);
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    /// <Fall>
    /// Can be used for when the players are falling
    /// </Fall>
    public void Fall()
    {
        // I'm unsure what needs to go here but I know something else need sto go here.
        if (jumpCoroutine != null)
        {
            StopCoroutine(jumpCoroutine);
        }
        // Probably slow falling
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
