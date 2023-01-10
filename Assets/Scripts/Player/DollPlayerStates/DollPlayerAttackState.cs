using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DollPlayerAttackState : MonoBehaviour
{
    private DollPlayerMovement playerMovement;
    
    private DollPlayerModelView modelView;

    private DollPlayerStats stats;

    private StateManager stateManager;

    private float attack01Time;

    private int attack01Power;

    private float attack01Radius;

    private void OnEnable()
    {
        playerMovement = GetComponent<DollPlayerMovement>();
        
        modelView = GetComponentInChildren<DollPlayerModelView>();

        modelView.OnAttack01();

        stateManager = GetComponent<StateManager>();
        stats = GetComponent<DollPlayerStats>();

        attack01Power = stats.attack01Power;

        attack01Radius = stats.attack01Radius;

        attack01Time = stats.attack01Time;
        StartCoroutine(GroundAttack01());
    }

    private IEnumerator GroundAttack01()
    {
        {
            //Collider[] hitColliders = Physics.OverlapSphereNonAlloc(attackCenter, attackRadius, Quaternion.identity, 9999, QueryTriggerInteraction.Collide);

            //note the 10 is the max amount of returns per overlapsphere
            //change at will
            Collider[] hits = new Collider[10];

            // Call Physics.OverlapSphereNonAlloc and pass in the center point of the sphere, the radius of the sphere,
            // the array you declared, and an optional layer mask
            int numHits = Physics.OverlapSphereNonAlloc(transform.position, attack01Radius, hits);

            // Iterate through the array of Colliders and do something with each one
            for (int i = 0; i < numHits; i++)
            {
                ITakeDamage damageable = hits[i].GetComponent<ITakeDamage>();
                if (damageable != null)
                {
                    damageable.ChangeHP(attack01Power);
                }
            }

            Debug.Log(attack01Time);
            yield return new WaitForSeconds(attack01Time);
        }
        
        Debug.Log("Done");
        stateManager.ChangeStateString("idle");
    }

    private void OnDisable()
    {
        playerMovement.attacking = false;
    }
}