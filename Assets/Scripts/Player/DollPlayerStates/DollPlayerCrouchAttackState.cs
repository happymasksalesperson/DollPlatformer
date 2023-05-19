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

        crouchAttack01Radius = stats.crouchAttack01Radius;

        //custom crouch shit later
        crouchAttack01Power = stats.attack01Power;

        slideTime = stats.crouchAttack01Time;

        stats.armoured = true;

        Slide();
    }

    private void Slide()
    {
        float horizontalForce = facingRight ? -slideForce : slideForce;
        rb.AddForce(new Vector3(horizontalForce, 0, 0), ForceMode.Impulse);
        
        

        StartCoroutine(CrouchAttack01());
    }

    private void Update()
    {
        int playerLayer = 3;
        int allLayers = ~0; // all bits set to 1, represents all layers

        int layerMask = allLayers & ~(1 << playerLayer);
        //copy/paste from Doll Play Attack State
        Collider[] hits = new Collider[10];

        int numHits = Physics.OverlapSphereNonAlloc(transform.position, crouchAttack01Radius, hits, layerMask);
        
        for (int i = 0; i < numHits; i++)
        {
            ITakeDamage damageable = hits[i].GetComponent<ITakeDamage>();
            if (damageable != null)
            {
                damageable.ChangeHP(crouchAttack01Power);
            }
        }
    }

    private IEnumerator CrouchAttack01()
    {

            yield return new WaitForSeconds(slideTime);
            
            playerMovement.AttackEnd();
    }

    private void OnDisable()
    {
    }
}
