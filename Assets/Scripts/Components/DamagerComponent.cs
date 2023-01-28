using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamagerComponent : MonoBehaviour
{
    private StatsComponent stats;

    private bool isAlive=true;

    [Header("BODY SPHERE STATS")]
    [SerializeField] private float sphereRadius;
    [SerializeField] private int sphereAttackPower;

    [SerializeField] private float checkInterval;
    
    private void Start()
    {
        stats = GetComponent<StatsComponent>();
        sphereAttackPower = stats.bodyHitboxPower;

        StartCoroutine(CheckForPlayer());
    }

    private void Update()
    {
        isAlive = stats.isAlive;
    }

    private IEnumerator CheckForPlayer()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(checkInterval);
            Collider[] hits = new Collider[10];

            int numHits = Physics.OverlapSphereNonAlloc(transform.position, sphereRadius, hits);

            for (int i = 0; i < numHits; i++)
            {
                IPlayer player = hits[i].GetComponent<IPlayer>();
                if (player != null)
                {
                    ITakeDamage damageable = hits[i].GetComponent<ITakeDamage>();
                    if (damageable != null)
                    {
                        damageable.ChangeHP(sphereAttackPower);
                    }
                }
            }
        }
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawSphere(transform.position, sphereRadius);
    // }

}
