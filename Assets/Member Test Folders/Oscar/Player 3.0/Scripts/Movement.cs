using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : HelpfulFunctions
{
    public PlayerBrain playerModel;

    private float moveDirFloat;
    private void Update()
    {
        if (playerModel.attackBool == false)
        {
            moveDirFloat = playerModel.movementFloat;
            Movement(moveDirFloat);

            if (playerModel.jumpBool && playerModel.canJump && playerModel.fallBool == false)
            {
                Jump();
                playerModel.fallBool = true;
                playerModel.canJump = false;
            }
        }
    }
}
