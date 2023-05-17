using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingLava : MonoBehaviour
{       
    public float speed;

    public int lavaDamage;

    public Vector3 lavaHalfExtents;

    public float inflictDamageTimer;
    
    private Collider[] colliders;
    private const int maxColliders = 100;


    public void OnEnable()
    {
        colliders = new Collider[maxColliders];
        StartCoroutine(DoDamage());
    }

    private IEnumerator DoDamage()
    {
        while (true)
        {
            int numColliders = Physics.OverlapBoxNonAlloc(new Vector3(transform.position.x, transform.position.y-lavaHalfExtents.y/2, transform.position.z), lavaHalfExtents, colliders);

            for (int i = 0; i < numColliders; i++)
            {
                Collider collider = colliders[i];
                Debug.Log("Detected collider: " + collider.name);

                if (collider.GetComponent<HealthModel>() != null)
                {
                   HealthModel health = collider.GetComponent<HealthModel>();
                   health.ChangeHP(lavaDamage);
                }
            }

            yield return new WaitForSeconds(inflictDamageTimer);
        }
    }

    void Update()
    {
        float verticalMovement = speed * Time.deltaTime;

        transform.Translate(Vector3.up * verticalMovement);
    }
}
