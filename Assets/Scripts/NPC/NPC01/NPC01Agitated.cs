using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC01Agitated : MonoBehaviour
{
    public NPC01Brain brain;

    public bool facingRight;

    public Vector3 lastPointOfInterest;

    public float agitatedTimer;

    public bool seesPlayer;

    public float distanceToPlayer;

    public float meleeAttackDist;

    public List<bool> attackOptions;

    public float jumpAttackTime;

    public void OnEnable()
    {
        brain = GetComponentInParent<NPC01Brain>();

        if (brain.oscarVision.PlayerTransform() != null)
            lastPointOfInterest = brain.oscarVision.PlayerTransform().position;

        else lastPointOfInterest = brain.lastPointOfInterest;

        seesPlayer = brain.seesPlayer;

        distanceToPlayer = Vector3.Distance(transform.position, lastPointOfInterest);
        
        Vector3 direction = lastPointOfInterest - transform.position;

        if (direction.x > 0)
        {
            facingRight = true;
        }
        else if (direction.x < 0)
        {
            facingRight = false;
        }
        
        brain.FacingRight(facingRight);
        
        attackOptions.Clear();
        attackOptions.Add(brain.meleeAttack);
        attackOptions.Add(brain.jumpAttacking);
        attackOptions.Add(brain.rangeAttack);

        brain.FacingRight(facingRight);
        
        //change to agitated later
        
        brain.modelView.OnIdle();

        StartCoroutine(AgitatedWait());
    }

    private IEnumerator AgitatedWait()
    {
        yield return new WaitForSeconds(agitatedTimer);

        if (seesPlayer)
            DecideAttack();

        else
            Finish();
    }

    private void DecideAttack()
    {
        if (distanceToPlayer <= meleeAttackDist)
            brain.meleeAttack = true;

        if (attackOptions.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, attackOptions.Count);
            attackOptions[randomIndex] = true;
        }
        
        brain.meleeAttack = attackOptions[0];
        brain.jumpAttacking = attackOptions[1];
        brain.rangeAttack = attackOptions[2];
    }

    private void Finish()
    {
        NPCType myType = brain.myType;
        
        if (myType == NPCType.Idle)
            brain.idle = true;
        
        else if (myType == NPCType.Patrol)
            brain.patrolling = true;

        brain.agitated = false;
    }
}
