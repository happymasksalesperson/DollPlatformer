using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;

public class DollPlayerMovement : MonoBehaviour, IPlayer
{
    //
    // Player controls
    private PlayerActions _playerActions;
    private Vector2 _movement;

    private DollPlayerStats playerStats;

    private DollPlayerModelView modelView;

    private StateManager stateManager;

    // // // // // //
    // PLAYER OBJECT
    private Rigidbody _rb;
    
    //
    // player layer
    public LayerMask playerLayer;
    public LayerMask groundLayer;

    //
    // change size during different states
    private BoxCollider _boxCollider;

    //
    // Player can only jump when grounded thru GroundCheck.cs (on child)
    private GroundCheck _groundCheck;
    private bool _lastCheck;

    public bool grounded;

    //tracks player facing dir
    public bool facingRight;

    //Player States
    public enum PlayerState
    {
        idle,
        
        talk,

        run,
        crouch,
        jump,

        attack01,
        crouchAttack,
        jumpAttack,
        
        disabled
    }

    public PlayerState currentState;

    // // // // // //
    // AIMING
    //
    public Vector2 _aimVector;

    // // // // // //
    // ATTACKING
    //
    public bool attacking = false;

    // // // // // //
    // RUNNING
    //
    public bool running;
    private float _runSpeed;

    private float _maxSpeed;

    // // // // // //
    // JUMPING
    //
    public bool jumping;

    private Gravity _gravity;
    
    /*[SerializeField] private float _groundCheckHeight;

    [SerializeField] private LayerMask _groundMask;*/
    
    //tracks if the jump button is being held down by Player
    public bool holdingJump;

    //"disable Ground Check Time"
    [SerializeField] private float _disableGCTime;

    private float _defaultGravity;

    [SerializeField] private float _jumpFallMultiply;

    public bool _groundCheckEnabled = true;

    // // // // // //
    // CROUCHING
    //
    public bool crouching;

    // // // // // //
    // TALKING -- dialogue
    //
    public bool talking;
    public bool canTalk;
    public bool talkTarget;
    
    RaycastHit hitInfo;
    
    // // // // // //
    // DISABLED
    // perhaps due to takeDamage
    public bool disabled=false;
    // invincibility after an attack
    public float gracePeriod;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _groundCheck = GetComponentInChildren<GroundCheck>();

        _gravity = GetComponent<Gravity>();
        _defaultGravity = _gravity.CurrentGravity();

        _boxCollider = GetComponent<BoxCollider>();

        _playerActions = new PlayerActions();

        _playerActions.InGamePlayer.Jump.performed += Jump;
        _playerActions.InGamePlayer.Jump.canceled += JumpCancel;

        _playerActions.InGamePlayer.Crouch.started += Crouch;
        _playerActions.InGamePlayer.Crouch.canceled += CrouchCancel;

        _playerActions.InGamePlayer.Attack.performed += Attack01;

        _playerActions.InGamePlayer.Talk.performed += Talk;

        _playerActions.InGamePlayer.Enable();

        //get stats from PlayerStats
        playerStats = GetComponent<DollPlayerStats>();
        _runSpeed = playerStats.runSpeed;
        _maxSpeed = playerStats.maxSpeed;
        gracePeriod = playerStats.takeDamageGracePeriod;

        modelView = GetComponentInChildren<DollPlayerModelView>();

        stateManager = GetComponent<StateManager>();

        ChangeMovementState(currentState);
        // _defaultGravity = 
    }

    private void OnEnable()
    {
        LevelEventManager.LevelEventInstance.CanTalk += CanTalk;

        //event closes dialogue box and gives control back to player
        LevelEventManager.LevelEventInstance.StopTalk += () =>
        {
            if (talking)
            {
                talking = false;
                currentState = PlayerState.idle;
                ChangeMovementState(currentState);
            }
        };
    }

    private void CanTalk(bool talkAllowed)
    {
        canTalk = talkAllowed;
    }

    private void Talk(InputAction.CallbackContext context)
    {
        if (canTalk && talkTarget &&!talking)
        { 
            talking = true;
            _rb.velocity=Vector3.zero;
           
            currentState = PlayerState.talk;

            ITalk talk = playerStats.talkerObj.GetComponent<ITalk>();
            talk.Talk();
            currentState = PlayerState.talk;
            ChangeMovementState(currentState);
        }

        else if (talking)
        {
            DialogueManager.DialogueInstance.AdvanceDialogue();
        }
    }

    private void ChangeMovementState(PlayerState state)
    {
        string stateStr = currentState.ToString();
        stateManager.ChangeStateString(stateStr);
    }

    // // // // // //
    // UPDATE
    //
    private void Update()
    {
        _movement = _playerActions.InGamePlayer.Movement.ReadValue<Vector2>();

        _aimVector = _playerActions.InGamePlayer.AimVector.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (_groundCheckEnabled)
        {
            grounded = IsGrounded();
        }
        else
        {
            grounded = _lastCheck;
        }

        if(!disabled && !talking)
        HandleMovement();

        HandleGravity();
    }

    // // // // // //
    // Running
    //
    private void HandleMovement()
    {
        if (!grounded && !attacking)
            modelView.OnFall();
        
        if (grounded && !attacking && !crouching && !jumping)
        {
            _rb.velocity = new Vector3(_movement.x * _runSpeed, _movement.y);

            if (_rb.velocity.magnitude > _maxSpeed)
            {
                _rb.velocity = _rb.velocity.normalized * _maxSpeed;
            }

            if (_movement.x > 0)
            {
                running = true;
                currentState = PlayerState.run;
                modelView.OnRun();
                facingRight = false;
            }

            else if (_movement.x < 0)
            {
                running = true;
                currentState = PlayerState.run;
                modelView.OnRun();
                facingRight = true;
            }

            else if (_movement.x == 0)
            {
                running = false;
                currentState = PlayerState.idle;
                modelView.OnIdle();
            }
        }

        ChangeMovementState(currentState);
        modelView.OnFacingRight(facingRight);
    }

    // // // // // //
    // JUMPING
    //
    public void Jump(InputAction.CallbackContext context)
    {
        if (!disabled)
        {
            holdingJump = true;

            if (currentState == PlayerState.idle || currentState == PlayerState.run)
            {
                grounded = false;
                _lastCheck = grounded;

                jumping = true;

                StartCoroutine(GroundCheckAfterJump());

                currentState = PlayerState.jump;
                ChangeMovementState(currentState);
            }
        }
    }

    public void JumpCancel(InputAction.CallbackContext context)
    {
        holdingJump = false;
    }

    public void JumpEnd()
    {
        jumping = false;
        _lastCheck = grounded;

        if (running)
            currentState = PlayerState.run;

        else
        currentState = PlayerState.idle;
        ChangeMovementState(currentState);
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        // // // // // //
        // CROUCH
        //
        // Crouch by holding down or S through PlayerActions
        //
        if (currentState == PlayerState.idle && IsGrounded())
        {
            crouching = true;
            currentState = PlayerState.crouch;
        }
    }

    private bool IsGrounded()
    {
        grounded = _groundCheck.grounded;
        _lastCheck = grounded;
        return _groundCheck.grounded;
    }

    private IEnumerator GroundCheckAfterJump()
    {   
            int playerLayerValue = Mathf.Clamp(playerLayer.value, 0, 31);
            int groundLayerValue = Mathf.Clamp(groundLayer.value, 0, 31);
    
            Physics.IgnoreLayerCollision(playerLayerValue, groundLayerValue, true);
    
            _groundCheckEnabled = false;
            yield return new WaitForSeconds(_disableGCTime);
            jumping = false;
            _groundCheckEnabled = true;

            Physics.IgnoreLayerCollision(playerLayerValue, groundLayerValue, false);
        }

    public void CrouchCancel(InputAction.CallbackContext context)
    {
            crouching = false;
            if(grounded && !attacking)
            currentState = PlayerState.idle;
    }

    private void HandleGravity()
    {
        _rb.useGravity = true;

        if (_groundCheckEnabled && IsGrounded())
        {
            _rb.useGravity = false;
            jumping = false;
        }

        else if (jumping && _rb.velocity.y > 0f)
        {
            _gravity.ChangeGravity(_defaultGravity * _jumpFallMultiply);
        }
        else
        {
            _gravity.ChangeGravity(_defaultGravity);
        }
    }

    private void Attack01(InputAction.CallbackContext context)
    {
        attacking = true;

        currentState = PlayerState.attack01;
    }

    public void AttackEnd()
    {
        attacking = false;
        currentState = PlayerState.idle;
    }

    public void TakeDamageGracePeriod()
    {
        disabled = false;
        StartCoroutine(TakeDamageGracePeriodTimer());
        IEnumerator TakeDamageGracePeriodTimer()
        {
            yield return new WaitForSeconds(gracePeriod);
            playerStats.vulnerable = true;
        }
    }

    // // // // // //
    // NPC DETECTION
    public void DetectHP()
    {
        //return current HP
    }

    public void DetectPosition()
    {
        //return transform.position;
    }

    private void OnDisable()
    {
        LevelEventManager.LevelEventInstance.CanTalk -= CanTalk;
    }
}