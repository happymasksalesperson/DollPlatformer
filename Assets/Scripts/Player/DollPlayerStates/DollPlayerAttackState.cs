using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DollPlayerAttackState : MonoBehaviour
{
    private StateManager stateManager;

    private DollPlayerMovement playerMovement;

    private DollPlayerModelView modelView;

    private DollPlayerStats stats;

    public float attack01Time;

    private int attack01Power;

    public float attack01Radius;

    //distance sphere appears from Player centre
    private Vector3 offsetPosition;

    public float offsetDist;

    private bool facingRight;

    private void OnEnable()
    {
        stateManager = GetComponent<StateManager>();

        playerMovement = GetComponent<DollPlayerMovement>();

        facingRight = playerMovement.facingRight;

        modelView = GetComponentInChildren<DollPlayerModelView>();

        modelView.OnAttack01();

        stats = GetComponent<DollPlayerStats>();

        attack01Power = stats.attack01Power;

        attack01Radius = stats.attack01Radius;

        attack01Time = stats.attack01Time;
        StartCoroutine(GroundAttack01());
    }

    void CheckOffset(Transform transform, float offset, bool shouldBePositive)
    {
        offsetDist = CheckSign(offset, facingRight);
        Vector3 offsetVector = Vector3.right * offset;
        offsetPosition = transform.position += offsetVector;
    }

    //AI wrote this
    float CheckSign(float value, bool shouldBePositive)
    {
        return shouldBePositive ? value : -value;
    }

    private IEnumerator GroundAttack01()
    {
        CheckOffset(transform, offsetDist, facingRight);
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

            yield return new WaitForSeconds(attack01Time);

            stateManager.ChangeStateString("idle");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attack01Radius);
    }

    private void OnDisable()
    {
        playerMovement.AttackEnd();
    }
}