using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
   public PlayerControls controls;

   public PlayerStateManager stateManager;

   public PlayerControlsMiddleMan middleMan;

   public Player2Attack attack;

   public GroundCheck groundCheck;

   public float yDistance;

   public float moveTime;

   private bool jumpingDown;

   public Transform doll;

   private Vector3 initialPosition;

   private Vector3 targetPos;

   private IEnumerator jumpDownCoroutine;

   public void OnEnable()
   {
       middleMan.canJump = false;

       initialPosition = transform.position;
       targetPos = initialPosition - new Vector3(0, yDistance, 0);

       jumpDownCoroutine = JumpDownCoroutine();

       controls.JumpEvent += JumpDown;
   }

   private void JumpDown()
   {
       if (!groundCheck.trueGround)
           StartCoroutine(jumpDownCoroutine);
   }

   private IEnumerator JumpDownCoroutine()
   {
       attack.canSlide = false;
       jumpingDown = true;
       middleMan.canJump = false;
       middleMan.canRangeAttack = false;

       float elapsedTime = 0f;

       while (elapsedTime < moveTime)
       {
           float t = elapsedTime / moveTime;
           doll.transform.position = Vector3.Lerp(initialPosition, targetPos, t);
           elapsedTime += Time.deltaTime;
           yield return null;
       }

       jumpingDown = false;
       
       doll.transform.position = targetPos;

       stateManager.ChangeState(PlayerStates.Fall);
    }

   public void OnDisable()
   {
       middleMan.canRangeAttack = true;
       middleMan.canJump = true;
       attack.canSlide = true;
       controls.JumpEvent -= JumpDown;
   }

   private void Update()
   {
      if (controls.aimInput >= 0 && !jumpingDown)
      {
         stateManager.ChangeState(PlayerStates.Idle);
      }
   }
}
