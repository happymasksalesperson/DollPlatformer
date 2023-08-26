using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public LayerMask targetLayers;

    public int damageAmount;

    public bool active = true;

    public float timeBetweenDamageTicks;

    private void OnCollisionEnter(Collision collision)
    {
        if (active)
        {
            if ((targetLayers.value & (1 << collision.gameObject.layer)) != 0)
            {
                if (collision.gameObject.GetComponent<ITakeDamage>() != null)
                {
                    ITakeDamage victim = collision.gameObject.GetComponent<ITakeDamage>();
                    victim.ChangeHP(damageAmount);
                    StartCoroutine(ActiveCooldown());
                }
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