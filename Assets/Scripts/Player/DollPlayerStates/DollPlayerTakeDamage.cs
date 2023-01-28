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
    
    private bool facingRight;

    [Header("HOW FAR KNOCKED BACK HORIZONTAL")]
    [SerializeField]
    private float horizontalDist;
    
    [Header("HOW FAR KNOCKED BACK VERTICAL")]
    [SerializeField]
    private float verticalDist;

    [Header("HOW LONG DISABLED FOR")]
    [SerializeField]
    private float disableTime;

    private void OnEnable()
    {
        stateManager = GetComponent<StateManager>();

        stats = GetComponent<DollPlayerStats>();
        stats.vulnerable = false;

        modelView = GetComponentInChildren<DollPlayerModelView>();
        
        modelView.OnTakeDamage();

        rb = GetComponent<Rigidbody>();
        
        playerMovement = GetComponent<DollPlayerMovement>();
        playerMovement.disabled = true;
        facingRight = playerMovement.facingRight;

        StartCoroutine(Knockback());
    }

    private IEnumerator Knockback()
    {
        if (!facingRight)
        {
            horizontalDist = -horizontalDist;
        }

        bool isArmoured = stats.armoured;
        if(isArmoured)
            rb.AddForce(new Vector3(horizontalDist, verticalDist, 0), ForceMode.Impulse);
        
        yield return new WaitForSeconds(disableTime);

        int HP = stats.HP;
        if(HP==0)
            stateManager.ChangeStateString("death");

        else
        {
            playerMovement.disabled = false;
            stats.vulnerable = true;
            stateManager.ChangeStateString("idle");
        }
    }

}