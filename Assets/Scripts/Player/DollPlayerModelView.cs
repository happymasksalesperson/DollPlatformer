using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollPlayerModelView : MonoBehaviour
{
    //attack 01 implies the existence of attack 02 etc later
    public event Action Attack01;

    public void OnAttack01()
    {
        Attack01?.Invoke();
    }

    //attack 01 wind up
    public event Action Attack01Windup;

    public void OnAttack01Windup()
    {
        Attack01Windup?.Invoke();
    }

    //idle
    public event Action Idle;

    public void OnIdle()
    {
        Idle?.Invoke();
    }

    //event for NPC taking damage
    public event Action TakeDamage;

    public void OnTakeDamage()
    {
        TakeDamage?.Invoke();
    }
    
    //Death
    public event Action Death;

    public void OnDeath()
    {
        Death?.Invoke();
    }
}