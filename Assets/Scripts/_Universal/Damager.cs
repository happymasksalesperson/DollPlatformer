using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public LayerMask targetLayers;

    public bool multiHit = false;

    public int damageAmount;

    public bool active = true;

    public float timeBetweenDamageTicks;

    public event Action HitITakeDamageEvent;

    public event Action HitEnvironmentEvent;

    private void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleCollision(other);
    }

    private void HandleCollision(Collider col)
    {
        if (active)
        {
            if ((targetLayers.value & (1 << col.gameObject.layer)) != 0)
            {
                if (col.GetComponent<ITakeDamage>() != null)
                {
                    ITakeDamage damageTaker = col.GetComponent<ITakeDamage>();
                    damageTaker.ChangeHP(-damageAmount);
                    HitITakeDamageEvent?.Invoke();
                }
                else
                {
                    HitEnvironmentEvent?.Invoke();
                }

                if (multiHit)
                    StartCoroutine(ActiveCooldown());
            }
        }
    }

    private IEnumerator ActiveCooldown()
    {
        active = false;

        yield return new WaitForSeconds(timeBetweenDamageTicks);

        active = true;
    }
}