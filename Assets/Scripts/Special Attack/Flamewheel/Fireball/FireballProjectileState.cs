using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireballProjectileState : FireballStateBase
{
    public Transform targetTransform;

    public Rigidbody rb;

    public float shootForce;

    public int damageAmount;

    private void OnEnable()
    {
        targetTransform = brain.targetTransform;

        damager.HitITakeDamageEvent += HitVictim;
        damager.HitEnvironmentEvent += HitEnvironment;

        //shoots rb at target times shootForce
        brain.transform.position = brain.shootPointTransform.position;
        Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;
        float xSign = Mathf.Sign(directionToTarget.x);
        if (xSign > 0)
        {
            brain.FacingRight(true);
        }
        else
        {
            brain.FacingRight(false);
        }
        rb.AddForce(directionToTarget * shootForce, ForceMode.VelocityChange);
    }

    private void HitVictim()
    {
        brain.ChangeState(FireballBrain.FireballStateEnum.Death);
    }

    private void HitEnvironment()
    {
        brain.ChangeState(FireballBrain.FireballStateEnum.Linger);
    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        damager.HitITakeDamageEvent -= HitVictim;
        damager.HitEnvironmentEvent -= HitEnvironment;
    }
}