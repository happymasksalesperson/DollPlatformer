using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollPlayerModelView : MonoBehaviour
{
    //attack 01 wind up
    public event Action Attack01Windup;

    public void OnAttack01Windup()
    {
        Attack01Windup?.Invoke();
    }

    //attack 01 implies the existence of attack 02 etc later
    public event Action Attack01;

    public void OnAttack01()
    {
        Attack01?.Invoke();
    }

    //crouch
    public event Action Crouch;

    public void OnCrouch()
    {
        Crouch?.Invoke();
    }

    //idle
    public event Action Idle;

    public void OnIdle()
    {
        Idle?.Invoke();
    }

    // // // //
    // JUMPING

    //neutral jump
    //add direction / falling later?
    public event Action Jump;

    public void OnJump()
    {
        Jump?.Invoke();
    }

    public event Action JumpUpAttack01;

    public void OnJumpUpAttack01()
    {
        JumpUpAttack01?.Invoke();
    }

    public event Action JumpNeutralAttack01;

    public void OnJumpNeutralAttack01()
    {
        JumpNeutralAttack01?.Invoke();
    }

    public event Action JumpDownAttack01;

    public void OnJumpDownAttack01()
    {
        JumpDownAttack01?.Invoke();
    }
    
    //idle
    public event Action Run;

    public void OnRun()
    {
        Run?.Invoke();
    }

    //takedamage
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
    
    //
    public event Action<bool> FacingRight; 

    public void OnFacingRight(bool facingRight)
    {
        FacingRight?.Invoke(facingRight);
    }
}