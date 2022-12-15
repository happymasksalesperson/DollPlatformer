using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEventManager : MonoBehaviour
{
    //
    public event Action Attack01;

    public void OnAttack01()
    {
        Attack01?.Invoke();
    }

    //event for NPC taking damage
    public event Action TakeDamage;

    public void OnTakeDamage()
    {
        TakeDamage?.Invoke();
    }

    //event for NPC01 patrolling
    public event Action Patrol;

    public void OnPatrol()
    {
        Patrol?.Invoke();
    }
}