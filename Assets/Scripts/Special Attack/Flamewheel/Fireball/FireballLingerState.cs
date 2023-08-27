using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballLingerState : FireballStateBase
{
    public float lingerTime;

    public void OnEnable()
    {
        damager.multiHit = true;
        StartCoroutine(Linger());
    }

    private IEnumerator Linger()
    {
        yield return new WaitForSeconds(lingerTime);

        brain.ChangeState(FireballBrain.FireballStateEnum.Death);
    }

}
