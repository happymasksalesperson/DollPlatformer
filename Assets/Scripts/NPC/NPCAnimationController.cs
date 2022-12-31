using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    private NPCModelView modelView;

    private HealthModelView healthModelView;

    private SpriteRenderer _rend;

    private Animator _anim;

    private void OnEnable()
    {
        _rend = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        modelView = GetComponentInParent<NPCModelView>();

        modelView.Attack01 += Attack;
        modelView.Attack01Windup += Attack01Windup;
        modelView.TakeDamage += TakeDamage;
        modelView.Patrol += Patrolling;
    }

    private void Attack()
    {
        _anim.SetTrigger("Attack01");
    }

    private void Attack01Windup()
    {
        _anim.ResetTrigger("Patrol");
        _anim.SetTrigger("Attack01Windup");
    }

    private void TakeDamage()
    {
        _anim.ResetTrigger("Patrol");
        _anim.SetTrigger("TakeDamage");
    }

    private void Patrolling()
    {
        _anim.ResetTrigger("TakeDamage");
        _anim.ResetTrigger("Attack01");
        _anim.ResetTrigger("Attack01Windup");
        _anim.SetTrigger("Patrol");
    }

    private void OnDisable()
    {
        modelView.Attack01 -= Attack;
        modelView.Attack01Windup -= Attack01Windup;
        modelView.TakeDamage -= TakeDamage;
        modelView.Patrol -= Patrolling;
    }
}