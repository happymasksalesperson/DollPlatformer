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
        _anim.Play("attack01");
    }

    private void Attack01Windup()
    {
        _anim.Play("attack01windup");
    }

    private void TakeDamage()
    {
        _anim.Play("takeDamage");
    }

    private void Patrolling()
    {
        _anim.Play("patrol");
    }

    private void OnDisable()
    {
        modelView.Attack01 -= Attack;
        modelView.Attack01Windup -= Attack01Windup;
        modelView.TakeDamage -= TakeDamage;
        modelView.Patrol -= Patrolling;
    }
}