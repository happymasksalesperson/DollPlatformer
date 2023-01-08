using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DollPlayerAttackState : MonoBehaviour
{
    private DollPlayerModelView modelView;

    private DollPlayerStats stats;

    private StateManager stateManager;

    private float attack01Time;

    private int attackPower;

    private float attackRadius;

    [SerializeField] private Vector3 attackCenter;

    [SerializeField] private Vector3 Attack01Box;

    private void OnEnable()
    {
        modelView = GetComponentInChildren<DollPlayerModelView>();

        modelView.OnAttack01();

        stateManager = GetComponent<StateManager>();
        stats = GetComponent<DollPlayerStats>();

        attack01Time = stats.attack01Time;
        StartCoroutine(GroundAttack01());
    }

    private IEnumerator GroundAttack01()
    {
        {
            attackCenter = transform.position;

            //Collider[] hitColliders = Physics.OverlapSphereNonAlloc(attackCenter, attackRadius, Quaternion.identity, 9999, QueryTriggerInteraction.Collide);

            Collider[] hits = new Collider[10];

            // Call Physics.OverlapSphereNonAlloc and pass in the center point of the sphere, the radius of the sphere,
            // the array you declared, and an optional layer mask
            int numHits = Physics.OverlapSphereNonAlloc(transform.position, attackRadius, hits);

            // Iterate through the array of Colliders and do something with each one
            for (int i = 0; i < numHits; i++)
            {
                ITakeDamage damageable = hits[i].GetComponent<ITakeDamage>();
                if (damageable != null)
                {
                    damageable.ChangeHP(attackPower);
                }
            }

            yield return new WaitForSeconds(attack01Time);
        }
    }
}