using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerMovement : MonoBehaviour, IPlayer
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

        run,
        crouch,
        jump,

        attack01,
        crouchAttack,
        jumpAttack
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

    [SerializeField] private float _groundCheckHeight;

    [SerializeField] private LayerMask _groundMask;


    //tracks if the jump button is being held down by Player
    public bool holdingJump;

    //"disable Ground Check Time"
    [SerializeField] private float _disableGCTime;

    private float _defaultGravity;

    private float _jumpFallMultiply;

    private bool _groundCheckEnabled = true;

    private WaitForSeconds _jumpWait;

    // // // // // //
    // CROUCHING
    //
    public bool crouching;

    // // // // // //
    // TALKING -- dialogue
    //
    private bool talking;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _groundCheck = GetComponentInChildren<GroundCheck>();

        _gravity = GetComponent<Gravity>();
        _defaultGravity = _gravity.CurrentGravity();

        _boxCollider = GetComponent<BoxCollider>();

        _jumpWait = new WaitForSeconds(_disableGCTime);

        _playerActions = new PlayerActions();

        _playerActions.InGamePlayer.Jump.performed += Jump;
        _playerActions.InGamePlayer.Jump.canceled += JumpCancel;

        _playerActions.InGamePlayer.Attack.performed += Attack01;

        _playerActions.InGamePlayer.Enable();

        //get stats from PlayerStats
        playerStats = GetComponent<DollPlayerStats>();
        _runSpeed = playerStats.runSpeed;
        _maxSpeed = playerStats.maxSpeed;

        modelView = GetComponentInChildren<DollPlayerModelView>();

        stateManager = GetComponent<StateManager>();

        ChangeMovementState(currentState);
        // _defaultGravity = 
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

        HandleMovement();

        HandleGravity();
    }

    // // // // // //
    // Running
    //
    private void HandleMovement()
    {
        if (grounded && !attacking && !crouching && !jumping && !talking)
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
            }

            // // // // // //
            // CROUCH
            //
            // Crouch by holding down or S through PlayerActions
            //
            if (!running && _aimVector.y < 0)
            {
                crouching = true;
                currentState = PlayerState.crouch;
                modelView.OnCrouch();
            }
        }

        if (!running)
        {
            currentState = PlayerState.idle;
            modelView.OnIdle();
        }

        ChangeMovementState(currentState);
        modelView.OnFacingRight(facingRight);
    }

    // // // // // //
    // JUMPING
    //
    public void Jump(InputAction.CallbackContext context)
    {
        holdingJump = true;

        if (grounded && !jumping && !attacking)
        {
            jumping = true;

            StartCoroutine(GroundCheckAfterJump());

            modelView.OnJump();

            currentState = PlayerState.jump;
        }
    }

    public void JumpCancel(InputAction.CallbackContext context)
    {
        holdingJump = false;
    }

    private bool IsGrounded()
    {
        grounded = _groundCheck.grounded;
        _lastCheck = grounded;
        return _groundCheck.grounded;
    }

    private IEnumerator GroundCheckAfterJump()
    {   
        _groundCheckEnabled = false;
        yield return _jumpWait;
        jumping = false;
        _groundCheckEnabled = true;
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
        if (!attacking)
        {
            attacking = true;
            stateManager.ChangeStateString("attack01");
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
}