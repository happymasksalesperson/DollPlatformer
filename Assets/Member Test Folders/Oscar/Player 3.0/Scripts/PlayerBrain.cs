using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBrain : MonoBehaviour
{
    /// <ScriptVariables>
    /// Scripts that I may need in this script
    /// </ScriptVariables>
    public Oscar.PlayerControls controls;
    public StateMachine stateManager;

    /// <StateVariables>
    /// Here is the list of state variables
    /// </StateVariables>
    public GameObject idleState;
    public GameObject moveState;
    public GameObject attackState;
    public GameObject jumpState;
    public GameObject fallState;
    public GameObject crouchState;
    
    /// <OtherVariables>
    /// Other values and variables I will need to determine state changing.
    /// </OtherVariables>
    public float attackDuration;
    public float maxJumpDuration;

    public bool canJump;
    public bool jumpBool;
    public bool fallBool;
    public bool attackBool;
    public float movementFloat;
    public float aimFloat;

    /// <CoroutineVariables>
    /// Coroutine Variables so I can cancel variables if needed
    /// </CoroutineVariables>
    public Coroutine attackCoroutine;
    public Coroutine jumpCoroutine;

    void Start()
    {
        controls.MovementInputEvent += ControlsOnMovementInputEvent;
        controls.AimInputEvent += ControlsOnAimInputEvent;
        controls.JumpEvent += ControlsOnJumpEvent;
        controls.JumpLetGoEvent += ControlsOnJumpLetGoEvent;
        controls.AttackEvent += ControlsOnAttackEvent;
    }
    
    private void ControlsOnMovementInputEvent(float obj)
    {
        movementFloat = obj;
    }

    private void ControlsOnAimInputEvent(float obj)
    {
        aimFloat = obj;
    }

    private void ControlsOnJumpEvent()
    {
        jumpCoroutine = StartCoroutine(jumpRoutine());
    }
    
    private void ControlsOnJumpLetGoEvent()
    {
        StopCoroutine(jumpCoroutine);
        jumpBool = false;
        fallBool = true;
    }

    public IEnumerator jumpRoutine()
    {
        canJump = true;
        jumpBool = true;
        yield return new WaitForSeconds(maxJumpDuration);
        jumpBool = false;
        fallBool = true;
    }
    
    private void ControlsOnAttackEvent()
    {
        StartCoroutine(AttackRoutine());
    }

    public IEnumerator AttackRoutine()
    {
        attackBool = true;

        yield return new WaitForSeconds(attackDuration);

        attackBool = false;
    }
    
    void Update()
    {
        if (fallBool)
        {
            stateManager.ChangeState(fallState);
        }
        
        if (jumpBool && fallBool == false)
        {
         stateManager.ChangeState(jumpState);
        }

        if (jumpBool == false && fallBool == false)
        {
            if (movementFloat < 0 || movementFloat > 0)
            {
                stateManager.ChangeState(moveState);
            }
        }
        
        if (attackBool)
        {
            stateManager.ChangeState(attackState);
        }
        
        if (aimFloat < 0 && movementFloat == 0 && jumpBool == false && attackBool == false && fallBool == false)
        {
            stateManager.ChangeState(crouchState);
        }

        if (aimFloat >= 0 && movementFloat == 0 && jumpBool == false && attackBool == false && fallBool == false)
        {
            stateManager.ChangeState(idleState);
        }
    }
}
