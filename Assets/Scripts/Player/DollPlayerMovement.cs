using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class DollPlayerMovement : MonoBehaviour, IPlayer
{
    // // // // // //
    // PLAYER OBJECT
    private Rigidbody _rb;

    private BoxCollider _boxCollider;

    private GroundCheck _groundCheck;

    public bool grounded;

    private PlayerActions _playerActions;
    private Vector2 _movement;

    private DollPlayerStats playerStats;

    private DollPlayerModelView modelView;

    private StateManager stateManager;

    //tracks player facing dir
    public bool facingRight;

    // // // // // //
    // AIMING
    //
    private Vector2 _aimVector;

    // // // // // //
    // ATTACKING
    //
    public bool attacking = false;

    // // // // // //
    // RUNNING
    //
    private float _runSpeed;

    private float _maxSpeed;

    // // // // // //
    // JUMPING
    //
    private Gravity _gravity;

    [SerializeField] [Range(1f, 5f)] private float _jumpFallGravityMultiply;

    [SerializeField] private float _groundCheckHeight;

    [SerializeField] private LayerMask _groundMask;

    public bool jumping;

    //"disable Ground Check Time"
    [SerializeField] private float _disableGCTime;

    private float _defaultGravity;

    private bool _groundCheckEnabled = true;

    private WaitForSeconds _jumpWait;

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
        _playerActions.InGamePlayer.Attack.performed += Attack01;

        _playerActions.InGamePlayer.Enable();

        //get stats from PlayerStats
        playerStats = GetComponent<DollPlayerStats>();
        _runSpeed = playerStats.runSpeed;
        _maxSpeed = playerStats.maxSpeed;

        modelView = GetComponentInChildren<DollPlayerModelView>();

        stateManager = GetComponent<StateManager>();
        // _defaultGravity = 
    }


    // // // // // //
    // UPDATE
    //
    private void Update()
    {
        grounded = IsGrounded();

        _movement = _playerActions.InGamePlayer.Movement.ReadValue<Vector2>();

        _aimVector = _playerActions.InGamePlayer.AimVector.ReadValue<Vector2>();

        //  Debug.Log(_movement);
    }

    private void FixedUpdate()
    {
        HandleMovement();

        HandleGravity();
    }

    // // // // // //
    // Running
    //
    private void HandleMovement()
    {
        if (IsGrounded() && !attacking && !jumping)
        {
            _rb.velocity = new Vector3(_movement.x * _runSpeed, _movement.y);

            if (_rb.velocity.magnitude > _maxSpeed)
            {
                _rb.velocity = _rb.velocity.normalized * _maxSpeed;
            }


            if (_movement.x > 0)
            {
                modelView.OnRun();
                facingRight = false;
            }
            else if (_movement.x < 0)
            {
                modelView.OnRun();
                facingRight = true;
            }

            if (IsGrounded() && _aimVector == Vector2.down)
                Crouch();

            if (IsGrounded() && _aimVector == Vector2.zero)
                stateManager.ChangeStateString("idle");
            
            
        }
        
        else if (_rb.velocity.x == 0 && IsGrounded())
        {
            _rb.velocity = Vector3.zero;
            stateManager.ChangeStateString("idle");
        }

        modelView.OnFacingRight(facingRight);
    }

    // // // // // //
    // CROUCHING
    //
    // add crouch attack? idle animations? etc
    private void Crouch()
    {
        stateManager.ChangeStateString("crouch");
    }

    // // // // // //
    // JUMPING
    //
    public void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded() && !jumping)
        {
            
            stateManager.ChangeStateString("jump");

            StartCoroutine(GroundCheckAfterJump());
            
            jumping = true;
        }
    }

    private bool IsGrounded()
    {
        grounded = _groundCheck.isGrounded;
        return _groundCheck.isGrounded;
    }

    private IEnumerator GroundCheckAfterJump()
    {
        _groundCheckEnabled = false;
        yield return _jumpWait;
        _groundCheckEnabled = true;
    }

    private void HandleGravity()
    {
        _rb.useGravity = true;

        if (_groundCheckEnabled && IsGrounded())
        {
            _rb.useGravity = false;
        }

        else if (jumping && _rb.velocity.y > 0f)
        {
            _gravity.ChangeGravity(_defaultGravity * _jumpFallGravityMultiply);
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


    // // // // // //
    // GIZMOS BABY
    private void OnDrawGizmos()
    {
    }
}