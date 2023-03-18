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
        
        //crouch is just stats attackPower
        crouchAttack01Power = stats.attack01Power;

        crouchAttack01Radius = stats.crouchAttack01Radius;

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

    private IEnumerator CrouchAttack01()
    {

            yield return new WaitForSeconds(slideTime);
            
            playerMovement.AttackEnd();

            if(playerMovement.crouching)
            stateManager.ChangeStateString("crouch");
            
            else
                stateManager.ChangeStateString("idle");
    }

    private void OnDisable()
    {
        
    }
}
