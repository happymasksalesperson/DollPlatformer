using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    private NPCModelView modelView;

    private HealthModelView healthModelView;

    private SpriteRenderer rend;
    
    private Animator anim;

    private void OnEnable()
    {
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        modelView = GetComponentInParent<NPCModelView>();

        modelView.Attack01 += Attack;
        modelView.Attack01Windup += Attack01Windup;
        modelView.TakeDamage += TakeDamage;
        modelView.Patrol += Patrolling;
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
    }
}