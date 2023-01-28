using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollPlayerTakeDamage : MonoBehaviour
{
    private DollPlayerMovement playerMovement;

    private DollPlayerStats stats;

    private StateManager stateManager;

    private Rigidbody rb;

    private DollPlayerModelView modelView;
    
    public bool facingRight;

    private bool isArmoured;

    [Header("HOW FAR KNOCKED BACK HORIZONTAL")]
    [SerializeField]
    private float horizontalDist;
    
    [Header("HOW FAR KNOCKED BACK VERTICAL")]
    [SerializeField]
    private float verticalDist;
    
    private float disableTime;

    private void OnEnable()
    {
        stateManager = GetComponent<StateManager>();

        stats = GetComponent<DollPlayerStats>();
        stats.vulnerable = false;
        disableTime = stats.takeDamageGracePeriod;

        modelView = GetComponentInChildren<DollPlayerModelView>();
        
        modelView.OnTakeDamage();

        rb = GetComponent<Rigidbody>();
        
        playerMovement = GetComponent<DollPlayerMovement>();
        playerMovement.disabled = true;
        rb.velocity = Vector3.zero;
        
        facingRight = playerMovement.facingRight;

        isArmoured = stats.armoured;

        StartCoroutine(Knockback());
    }

    private IEnumerator Knockback()
    {
        float dist;

        if (!facingRight)
            dist = horizontalDist * -1;

        else
            dist = horizontalDist;
        
        
        if(!isArmoured)
            rb.AddForce(new Vector3(dist, verticalDist, 0), ForceMode.VelocityChange);
        
        yield return new WaitForSeconds(disableTime);

        int HP = stats.HP;
        if(HP==0)
            stateManager.ChangeStateString("death");

        else
        {
            stateManager.ChangeStateString("idle");
        }
    }

    private void OnDisable()
    {
        playerMovement.TakeDamageGracePeriod();
    }
}