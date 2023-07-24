using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Attack : MonoBehaviour
{
    public PlayerStateManager stateManager;

    public PlayerControls controls;

    public void Start()
    {
        controls.AttackEvent += Attack;
    }

    private void Attack()
    {
        if (stateManager.currentState == PlayerStates.Crouch)
        {
            stateManager.ChangeState(PlayerStates.Slide);
        }
        
        else if (stateManager.currentState == PlayerStates.Idle)
        {
            stateManager.ChangeState(PlayerStates.StandRangeAttack01);
        }
    }

    public void OnDisable()
    {
        controls.AttackEvent -= Attack;
    }
}
