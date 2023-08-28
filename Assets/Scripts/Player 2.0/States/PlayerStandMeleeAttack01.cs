using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandMeleeAttack01 : MonoBehaviour
{
    public PlayerStateManager stateManager;

    public PlayerModelView modelView;

    public float attackTime;

    public PlayerControls controls;

    public PlayerControlsMiddleMan middleman;

    public bool attackBuffered;

    public Player2Attack attack;

    public void OnEnable()
    {
        attackBuffered = false;
        controls.AttackEvent += Buffer;
        middleman.canRangeAttack = false;
        middleman.canJump = false;
        middleman.inControl = false;
        modelView = stateManager.modelView;
        StartCoroutine(Attack());
    }

    private void Buffer()
    {
        middleman.canRangeAttack = false;
        attackBuffered = true;
    }


    private IEnumerator Attack()
    {
        modelView.OnChangeState(PlayerStates.StandMeleeAttack01);

        yield return new WaitForSeconds(attackTime);

        End();
    }

    private void End()
    {
        if (!middleman.grounded)
            stateManager.ChangeState(PlayerStates.Fall);

        else if (middleman.moving)
            stateManager.ChangeState(PlayerStates.Run);

        else
            stateManager.ChangeState(PlayerStates.Idle);
    }

    private void OnDisable()
    {
        controls.AttackEvent -= Buffer;
        middleman.canRangeAttack = true;
        middleman.canJump = true;
        middleman.inControl = true;

        if (attackBuffered)
        {
            attack.Attack();
            attackBuffered = false;
        }
    }
}