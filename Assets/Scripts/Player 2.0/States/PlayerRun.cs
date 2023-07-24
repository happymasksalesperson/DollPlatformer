using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    public PlayerStateManager stateManager;
    
    public GroundCheck groundCheck;

    private bool grounded;

    public void Update()
    {
        grounded = groundCheck.grounded;

        if (!grounded)
            stateManager.ChangeState(PlayerStates.Fall);
    }
}
