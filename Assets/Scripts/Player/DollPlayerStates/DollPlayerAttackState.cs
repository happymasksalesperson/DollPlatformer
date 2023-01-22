using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DollPlayerAttackState : MonoBehaviour
{
    private StateManager stateManager;

    private DollPlayerMovement playerMovement;

    private DollPlayerModelView modelView;

    private DollPlayerStats stats;

    public float attack01Time;

    private int attack01Power;

    public float attack01Radius;

    //distance sphere appears from Player centre
    private Vector3 offsetPosition;

    public float offsetDist;

    private bool facingRight;

    private Rigidbody rb;

    private Vector3 originalVelocity;

    private Gravity gravity;

    private void OnEnable()
    {
        gravity = GetComponent<Gravity>();
        
        rb = GetComponent<Rigidbody>();
        
        stateManager = GetComponent<StateManager>();

        playerMovement = GetComponent<DollPlayerMovement>();

        facingRight = playerMovement.facingRight;

        modelView = GetComponentInChildren<DollPlayerModelView>();

        modelView.OnAttack01();

        stats = GetComponent<DollPlayerStats>();

        attack01Power = stats.attack01Power;

        attack01Radius = stats.attack01Radius;

        attack01Time = stats.attack01Time;
        
        if (playerMovement.grounded)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            gravity.enabled = false;
            originalVelocity = rb.velocity;
            rb.velocity = Vector3.zero;
        }
        
        StartCoroutine(GroundAttack01());
    }

    //CHAT GPT wrote this for me! :)
    void CheckOffset(Transform transform, float offset, bool facingRight)
    {
        Vector3 offsetVector = Vector3.right * offset;
        if(facingRight)
            offsetVector = -offsetVector;
        offsetPosition = transform.position + offsetVector;
    }

    private IEnumerator GroundAttack01()
    {
        CheckOffset(transform, offsetDist, facingRight);
        {
            
            int playerLayer = 3;
            int targetLayer = 8;

            int layerMask = 1 << targetLayer;
            layerMask = ~(1 << playerLayer);
            
            //Collider[] hitColliders = Physics.OverlapSphereNonAlloc(attackCenter, attackRadius, Quaternion.identity, 9999, QueryTriggerInteraction.Collide);

            //note the 10 is the max amount of returns per overlapsphere
            //change at will
            Collider[] hits = new Collider[10];

            // Call Physics.OverlapSphereNonAlloc and pass in the center point of the sphere, the radius of the sphere,
            // the array you declared, and an optional layer mask
            int numHits = Physics.OverlapSphereNonAlloc(offsetPosition, attack01Radius, hits, layerMask);

            // Iterate through the array of Colliders and do something with each one
            for (int i = 0; i < numHits; i++)
            {
                ITakeDamage damageable = hits[i].GetComponent<ITakeDamage>();
                    if (damageable != null)
                    {
                        damageable.ChangeHP(attack01Power);
                    }
            }

            yield return new WaitForSeconds(attack01Time);

            stateManager.ChangeStateString("idle");
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(offsetPosition, attack01Radius);
    }

    private void OnDisable()
    {
        gravity.enabled = true;
        playerMovement.AttackEnd();
        if(!playerMovement.grounded){
            rb.velocity = originalVelocity;
        }
    }
    
}