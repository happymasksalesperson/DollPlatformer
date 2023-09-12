using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public PlayerBrain model;
    public IdleState idleState;
    public MoveState moveState;
    public CrouchState crouchState;
    public JumpState jumpState;
    public FallState fallState;
    public AttackState attackState;

    public Quaternion left;
    public Quaternion right;
    
    public Animator anim;
    
    public enum PlayerAnimationState
    {
        Idle,
        Crouch,
        Slide,
        Run,
        StandMeleeAttack01,
        StandRangeAttack01,
        Jump,
        Fall,
        TakeDamage
    }
    
    public PlayerAnimationState currentState;
    
    public void ChangeState(PlayerAnimationState newState)
    {
        currentState = newState;
        anim.Play(newState.ToString().ToLower());
    }

    private void Update()
    {
        if (model.movementFloat < 0)
        {
            transform.rotation = left;
        }

        if (model.movementFloat > 0)
        {
            transform.rotation = right;
        }
    }

    private void Start()
    {
        left = new Quaternion(0, 0, 0, 0);
        right = new Quaternion(0, 180, 0, 0);
        
        idleState.IdleEvent += IdleStateOnIdleEvent;
        moveState.move += MoveStateOnmove;
        crouchState.crouchEvent += CrouchStateOncrouchEvent;
        jumpState.JumpEvent += JumpStateOnJumpEvent;
        fallState.FallEvent += FallStateOnFallEvent;
        attackState.actionEvent += AttackStateOnactionEvent;
        
    }

    private void AttackStateOnactionEvent()
    {
        //attack animation
        ChangeState(PlayerAnimationState.StandMeleeAttack01);
    }

    private void FallStateOnFallEvent()
    {
        // Trigger fall animation
        ChangeState(PlayerAnimationState.Fall);
    }

    private void JumpStateOnJumpEvent()
    {
        // Trigger jump animation
        ChangeState(PlayerAnimationState.Jump);
    }

    private void CrouchStateOncrouchEvent()
    {
        // Trigger crouch animation
        ChangeState(PlayerAnimationState.Crouch);
    }

    private void MoveStateOnmove()
    {
        // Trigger move animation
        ChangeState(PlayerAnimationState.Run);
    }

    private void IdleStateOnIdleEvent()
    {
        // Trigger idle animation
        ChangeState(PlayerAnimationState.Idle);
    }
}
