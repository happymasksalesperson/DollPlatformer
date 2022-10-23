using System;
using System.Collections;
using System.Collections.Generic;
using NPC01;
using UnityEngine;

public class NPC01_AnimationController : MonoBehaviour
{
    private SpriteRenderer _rend;

    private Animator _anim;
    
    
    private void OnEnable()
    {
        _rend = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();

        NPC01.EventManager.NPC01Attack01Event += Attack;
        NPC01.EventManager.NPC01TakingDamageEvent += TakeDamage;
        NPC01.EventManager.NPC01PatrollingEvent += Patrolling;
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
        NPC01.EventManager.NPC01Attack01Event -= Attack;
        NPC01.EventManager.NPC01TakingDamageEvent -= TakeDamage;
        NPC01.EventManager.NPC01PatrollingEvent -= Patrolling;
    }
}
