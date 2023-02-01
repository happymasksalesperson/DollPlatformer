using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollPlayerCrouchAttackState : MonoBehaviour
{
     private StateManager stateManager;

    private DollPlayerMovement playerMovement;

    private DollPlayerModelView modelView;

    private DollPlayerStats stats;
    
    [Header("SLIDE STATS")]

    public float slideForce;

    public float slideTime;

    public float crouchAttack01Time;

    public int crouchAttack01Power;

    public float crouchAttack01Radius;

    [Header("PLAYER AIM FROM PLAYERMOVEMENT")] public Vector3 playerAimVector;

    //distance sphere appears from Player centre
    private Vector3 offsetPosition;

    public float offsetDistX;
    public float offsetDistY;

    private bool facingRight;

    private Rigidbody rb;

    private Vector3 originalVelocity;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();

        stateManager = GetComponent<StateManager>();

        playerMovement = GetComponent<DollPlayerMovement>();

        playerMovement.crouching = true;

        facingRight = playerMovement.facingRight;

        modelView = GetComponentInChildren<DollPlayerModelView>();

        modelView.OnCrouchAttack01();

        stats = GetComponent<DollPlayerStats>();
        
        //crouch is just stats attackPower
        crouchAttack01Power = stats.attack01Power;

        crouchAttack01Radius = stats.crouchAttack01Radius;

        crouchAttack01Time = stats.crouchAttack01Time;

        stats.armoured = true;

        StartCoroutine(Slide());
    }

    private IEnumerator Slide()
    {
        modelView.OnCrouch();
        
        float horizontalForce = facingRight ? slideForce : -slideForce;
        rb.AddForce(new Vector2(horizontalForce, 1));

        yield return new WaitForSeconds(slideTime);

        StartCoroutine(CrouchAttack01());
    }

    //CHAT GPT wrote this for me! :)
    void CheckOffset(Transform transform, float offsetX, float offsetY, bool facingRight)
    {
        Vector3 offsetVector = (Vector3.up * offsetX+ (Vector3.right * offsetY));
        if (facingRight)
            offsetVector = -offsetVector;
        offsetPosition = transform.position + offsetVector;
    }

    
    private IEnumerator CrouchAttack01()
    {
        modelView.OnAttack01();
        CheckOffset(transform, offsetDistX, offsetDistY, facingRight);
        {
            int playerLayer = 3;
            int allLayers = ~0; // all bits set to 1, represents all layers

            int layerMask = allLayers & ~(1 << playerLayer);

            // // // // //
            // note the 10 is the max amount of returns per overlapsphere
            // change at will
            Collider[] hits = new Collider[10];

            int numHits = Physics.OverlapSphereNonAlloc(offsetPosition, crouchAttack01Radius, hits, layerMask);

            for (int i = 0; i < numHits; i++)
            {
                ITakeDamage damageable = hits[i].GetComponent<ITakeDamage>();
                if (damageable != null)
                {
                    damageable.ChangeHP(crouchAttack01Power);
                }
            }

            yield return new WaitForSeconds(crouchAttack01Time);

            if(playerMovement.crouching)
            stateManager.ChangeStateString("crouch");
            
            else
                stateManager.ChangeStateString("idle");
        }
    }

    private void OnDisable()
    {
        
    }
}
