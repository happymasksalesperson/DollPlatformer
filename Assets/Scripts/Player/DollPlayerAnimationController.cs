using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class DollPlayerAnimationController : MonoBehaviour
{
    private DollPlayerModelView modelView;

    private HealthModelView healthModelView;

    private SpriteRenderer sprend;

    private Animator anim;

    private void OnEnable()
    {
        sprend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        modelView = GetComponentInParent<DollPlayerModelView>();

        modelView.Attack01 += Attack01;
        modelView.Attack01Windup += AttackWindup01;

        modelView.Crouch += Crouch;

        modelView.Idle += Idle;

        modelView.Jump += Jump;
        modelView.JumpUpAttack01 += JumpUpAttack01;
        modelView.JumpNeutralAttack01 += JumpNeutralAttack01;
        modelView.JumpDownAttack01 += JumpDownAttack01;

        modelView.Fall += Fall;

        modelView.Run += Run;

        modelView.TakeDamage += TakeDamage;

        modelView.FacingRight += FlipSpriteX;
    }

    public void FlipSpriteX(bool facingRight)
    {
        if (facingRight)
            sprend.flipX = true;

        else
        {
            sprend.flipX = false;
        }
    }

    private void AttackWindup01()
    {
        anim.Play("AttackWindup01");
    }

    private void Attack01()
    {
        anim.Play("Attack01");
    }

    private void Crouch()
    {
        anim.Play("Crouch");
    }

    private void Idle()
    {
        anim.Play("Idle");
    }

    private void Jump()
    {
        anim.Play("Jump");
    }

    private void JumpNeutralAttack01()
    {
        anim.Play("JumpNeutralAttack01");
    }

    private void JumpUpAttack01()
    {
        anim.Play("JumpAttack01");
    }

    private void JumpDownAttack01()
    {
        anim.Play("JumpDownAttack01");
    }

    private void Fall()
    {
        anim.Play("Jump");
    }

    private void Run()
    {
        anim.Play("Run");
    }

    private void TakeDamage()
    {
        anim.Play("TakeDamage");
    }


    private void OnDisable()
    {
        modelView.Jump -= Jump;
        modelView.JumpUpAttack01 -= JumpUpAttack01;
        modelView.JumpNeutralAttack01 -= JumpNeutralAttack01;
        modelView.JumpDownAttack01 -= JumpDownAttack01;

        modelView.Fall -= Fall;

        modelView.Attack01 -= Attack01;
        modelView.Attack01Windup -= AttackWindup01;

        modelView.TakeDamage -= TakeDamage;

        modelView.Idle -= Idle;
    }
}