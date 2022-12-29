using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    private NPCEventManager eventManager;
    
    private SpriteRenderer _rend;

    private Animator _anim;
    
    private void OnEnable()
    {
        _rend = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        eventManager = GetComponentInChildren<NPCEventManager>();

        eventManager.Attack01 += Attack;
        eventManager.TakeDamage += TakeDamage;
        eventManager.Patrol += Patrolling;
    }

    private void Attack()
    {
        _anim.SetBool("Attacking", true);
        _anim.SetBool("Patrolling", false);
    }

    private void TakeDamage()
    {
        _anim.SetBool("TakingDamage", true);
    }
    
    private void Patrolling()
    {
        _anim.SetBool("Patrolling", true);
        _anim.SetBool("Attacking", false);
    }

    private void OnDisable()
    {
        eventManager.Attack01 -= Attack;
        eventManager.TakeDamage -= TakeDamage;
        eventManager.Patrol -= Patrolling;
    }
}
