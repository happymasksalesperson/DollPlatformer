using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC01JumpAttack : MonoBehaviour
{
    public NPC01RangeAttack rangeAttack;

    public float jumpWaitTime;

    public NPC01Brain brain;

    public void OnEnable()
    {
        brain = GetComponentInParent<NPC01Brain>();
        rangeAttack = GetComponent<NPC01RangeAttack>();
        StartCoroutine(JumpAttack());
    }

    private IEnumerator JumpAttack()
    {
        yield return new WaitForSeconds(jumpWaitTime); 
        
        rangeAttack.Aim();
        
        yield return new WaitForSeconds(jumpWaitTime); 
        
        rangeAttack.Aim();

        yield return new WaitForSeconds(jumpWaitTime); 
        
        rangeAttack.Aim();
    }

    private void Update()
    {
        
    }
}
