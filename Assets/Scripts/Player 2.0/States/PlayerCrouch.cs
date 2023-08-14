using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
   public PlayerControls controls;

   public PlayerStateManager stateManager;

   public PlayerControlsMiddleMan middleMan;

   public void OnEnable()
   {
       middleMan.canRangeAttack = false;
   }

   public void OnDisable()
   {
       middleMan.canRangeAttack = true;
   }

   private void Update()
   {
      if (controls.aimInput >= 0)
      {
         stateManager.ChangeState(PlayerStates.Idle);
      }
   }
}
