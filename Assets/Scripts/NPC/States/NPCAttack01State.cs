using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttack01State : MonoBehaviour
{
    private StateManager _stateManager;

    private DollPlayerModelView testModelView;

    private NPCModelView modelView;
    private StatsComponent _stats;

    [SerializeField] private float attack01Time;

    [SerializeField] private float windup01Time;

    private float attack01Radius;

    private int attack01Power;

    //
    private bool conjoined;

    //tracks whether or not attack gets cancelled by OnDisable or continues
    private bool isAttacking;

    private void OnEnable()
    {
        _stateManager = GetComponent<StateManager>();
        _stats = GetComponent<StatsComponent>();

        testModelView = GetComponentInChildren<DollPlayerModelView>();

            attack01Power = _stats.attack01Power;

        attack01Radius = _stats.attack01Radius;

        conjoined = _stats.conjoined;

        attack01Time = _stats.attack01Time;

        modelView = GetComponentInChildren<NPCModelView>();

        isAttacking = true;

        StartCoroutine(Jab());
    }

    private IEnumerator Jab()
    {
        while (isAttacking)
        {
            modelView.OnAttack01Windup();

            yield return new WaitForSeconds(windup01Time);

            if (_stats.isAlive)
            {
                _stats.vulnerable = false;

                if (conjoined)
                {
                    StartCoroutine(CojoinedAttack01());
                    yield break;
                }

                modelView.OnAttack01();
                
                testModelView.OnChangeState(State.Attack01);

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
                    IPlayer player = hits[i].GetComponent<IPlayer>();
                    if (player != null)
                    {
                        ITakeDamage damageable = hits[i].GetComponent<ITakeDamage>();
                        if (damageable != null)
                        {
                            damageable.ChangeHP(attack01Power);
                        }
                    }
                }
            }

            isAttacking = false;
        }

        yield return new WaitForSeconds(attack01Time);

        _stateManager.ChangeStateString("patrol");
    }

    private IEnumerator CojoinedAttack01()
    {
        _stateManager.ChangeStateString("attack");

        modelView.OnAttack01();

        yield return new WaitForSeconds(attack01Time);

        modelView.OnIdle();

        _stateManager.ChangeStateString("idle");
    }
    
    private void OnDrawGizmos()
    {
       // Gizmos.color = Color.red;
       // Gizmos.DrawSphere(transform.position, attack01Radius);
    }

    private void OnDisable()
    {
        
    }
}