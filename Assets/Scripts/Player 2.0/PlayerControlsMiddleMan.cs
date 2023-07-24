using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlsMiddleMan : MonoBehaviour
{
    public PlayerControls playerControls;

    public PlayerStateManager stateManager;

    public void OnEnable()
    {
        playerControls.JumpEvent += JumpState;
    }

    public void JumpState()
    {
        stateManager.ChangeState(PlayerStates.Jump);
    }

    public void OnDisable()
    {
        playerControls.JumpEvent -= JumpState;
    }
}
