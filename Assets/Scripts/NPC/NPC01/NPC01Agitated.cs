using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC01Agitated : MonoBehaviour
{
    public NPC01Brain brain;

    public bool facingRight;

    public Vector3 lastPointOfInterest;

    public float agitatedTimer;
    
    public void OnEnable()
    {
        brain = GetComponentInParent<NPC01Brain>();

        lastPointOfInterest = brain.lastPointOfInterest;
        
        Vector3 direction = lastPointOfInterest - transform.position;

        if (direction.x > 0)
        {
            facingRight = false;
        }
        else if (direction.x < 0)
        {
            facingRight = true;
        }

        brain.FacingRight(facingRight);

        StartCoroutine(AgitatedWait());
    }

    private IEnumerator AgitatedWait()
    {
        yield return new WaitForSeconds(agitatedTimer);
        
        Finish();
    }

    public void Finish()
    {
        NPCType myType = brain.myType;
        
        if (myType == NPCType.Idle)
            brain.idle = true;
        
        else if (myType == NPCType.Patrol)
            brain.patrolling = true;

        brain.agitated = false;
    }
}
