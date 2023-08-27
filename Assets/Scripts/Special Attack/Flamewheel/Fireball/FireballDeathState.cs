using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballDeathState : FireballStateBase
{
    //fireball Death probably has a fade away animation
    //waits deathTime, then returns the PooledObject back to the pool

    public float deathTime;

    public PooledObject pooledObj;

    private void OnEnable()
    {
        damager.active = false;
        //wait out death timer
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(deathTime);

        pooledObj.ChangeActive(false);
    }
}
