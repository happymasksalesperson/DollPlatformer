using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollPlayerAnimationController : MonoBehaviour
{
    private DollPlayerModelView modelView;

    private HealthModelView healthModelView;

    private SpriteRenderer _rend;

    private Animator _anim;

    private void OnEnable()
    {
        _rend = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        modelView = GetComponentInParent<DollPlayerModelView>();

        modelView.Attack01 += Attack;
        modelView.Attack01Windup += Attack01Windup;
        modelView.TakeDamage += TakeDamage;
        modelView.Idle += Idle;
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

    private void Idle()
    {
        _anim.Play("idle");
    }

    private void OnDisable()
    {
        modelView.Attack01 -= Attack;
        modelView.Attack01Windup -= Attack01Windup;
        modelView.TakeDamage -= TakeDamage;
        modelView.Idle -= Idle;
    }
}
