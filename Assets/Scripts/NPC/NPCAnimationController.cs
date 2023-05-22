using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    private NPCModelView modelView;

    private HealthModelView healthModelView;

    private SpriteRenderer rend;
    
    private Animator anim;

    [ReadOnly]
    public bool facingRight;

    private void OnEnable()
    {
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        modelView = GetComponentInParent<NPCModelView>();

        modelView.Attack01 += Attack;
        modelView.Attack01Windup += Attack01Windup;
        modelView.TakeDamage += TakeDamage;
        modelView.Patrol += Patrolling;
        modelView.FacingRight += FlipSpriteX;
    }

    public void FlipSpriteX(bool newFacingRight)
    {
        if (newFacingRight)
        {
            rend.flipX = true;
        }

        else
        {
            rend.flipX = false;
        }

        facingRight = newFacingRight;
    }

    private void Attack()
    {
        anim.Play("Attack01");
    }

    private void Attack01Windup()
    {
        anim.Play("Attack01Windup");
    }

    private void TakeDamage()
    {
        anim.Play("TakeDamage");
    }

    private void Patrolling()
    {
        anim.Play("Patrol");
    }

    private void OnDisable()
    {
        modelView.Attack01 -= Attack;
        modelView.Attack01Windup -= Attack01Windup;
        modelView.TakeDamage -= TakeDamage;
        modelView.Patrol -= Patrolling;
        modelView.FacingRight -= FlipSpriteX;
    }
}