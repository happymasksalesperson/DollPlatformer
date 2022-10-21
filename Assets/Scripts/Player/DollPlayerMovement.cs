using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DollPlayerMovement : MonoBehaviour
{
    // // // // // //
    // PLAYER OBJECT
    private Rigidbody _rb;
    
    private BoxCollider _boxCollider;
    private Vector2 _boxCenter;
    private Vector2 _boxSize;
    private float _circleSize;
    
    private PlayerActions _playerActions;
    private Vector2 _movement;

    private DollPlayerStats playerStats;
    
    // // // // // //
    // AIMING
    //
    private Vector2 _aimVector;
   
    
    // // // // // //
    // RUNNING
    //
    private float _runSpeed;
    
    private float _maxSpeed;
    
    // // // // // //
    // JUMPING
    //
    private Gravity _gravity;
    
    private float _jumpForce;

    [SerializeField]
    [Range(1f, 5f)]
    private float _jumpFallGravityMultiply;

    [SerializeField]private float _groundCheckHeight;

    [SerializeField]private LayerMask _groundMask;

    //"disable Ground Check Time"
    [SerializeField] private float _disableGCTime;

    private bool _jumping;

    private float _defaultGravity;

    private bool _groundCheckEnabled = true;

    private WaitForSeconds _jumpWait;
    
    
    // // // // // //
    // PLAYER STATES
    // move this to a state machine later?
    private enum PlayerState
    {
        Walking,
        Attacking,
        Jumping,
    }
    private PlayerState currentState;

    private void Awake()
    {
        _rb = this.GetComponent<Rigidbody>();
        
       // _defaultGravity = 
    }

    // // // // // //
    // UPDATE
    //
    private void Update()
    {
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
        _rb.velocity = new Vector3(_movement.x * _runSpeed, 0f,0f);
        
        if (_rb.velocity.magnitude > _maxSpeed)
        {
            _rb.velocity = _rb.velocity.normalized * _maxSpeed;
        }
    }

    // // // // // //
    // ATTACKING
    //
    public void Attack(InputAction.CallbackContext context)
    {
        StartCoroutine(Attacking());
    }
    private IEnumerator Attacking()
    {
        currentState = PlayerState.Attacking;
        yield break;
    }
    
    // // // // // //
    // JUMPING
    //
    public void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            _rb.velocity += Vector3.up * _jumpForce;
            _jumping = true;
            StartCoroutine(GroundCheckAfterJump());
        }
    }

    private bool IsGrounded()
    {
        _boxCenter = new Vector2(_boxCollider.bounds.center.x, _boxCollider.bounds.center.y) +
                     (Vector2.down * (_boxCollider.bounds.extents.y + _groundCheckHeight / 2f));

        _boxSize = new Vector2(_boxCollider.bounds.size.x, _groundCheckHeight);

        _circleSize = _groundCheckHeight;

       var groundBox = Physics.OverlapBox(_boxCenter, _boxSize, Quaternion.identity, _groundMask);

        //var groundCircle = Physics.OverlapSphere(_boxCenter, _circleSize, _groundMask);
        
       if (groundBox != null)
           return true;

        return false;
    }

    private IEnumerator GroundCheckAfterJump()
    {
        _groundCheckEnabled = false;
        yield return _jumpWait;
        _groundCheckEnabled = true;
    }

    private void HandleGravity()
    {
        if (_groundCheckEnabled && IsGrounded())
        {
            _jumping = false;
        }

        else if (_jumping && _rb.velocity.y < 0f)
        {
            _gravity.ChangeGravity(_defaultGravity * this._jumpFallGravityMultiply); 
        }
        else
        {
            _gravity.ChangeGravity(_defaultGravity);
        }
        
    }
    
    
    // // // // // //
    // ON ENABLE / DISABLE
    private void OnEnable()
    {
        _gravity = GetComponent<Gravity>();
        _defaultGravity = _gravity.CurrentGravity();

        _boxCollider = GetComponent<BoxCollider>();

        _jumpWait = new WaitForSeconds(_disableGCTime);
        
        _playerActions = new PlayerActions();

        _playerActions.InGamePlayer.Jump.performed += Jump;
        _playerActions.InGamePlayer.Attack.performed += Attack;
        
        _playerActions.InGamePlayer.Enable();
        
        //get stats from PlayerStats
        playerStats = this.GetComponent<DollPlayerStats>();
        _runSpeed = playerStats.MyRunSpeed();
        _maxSpeed = playerStats.MyMaxSpeed();
        _jumpForce = playerStats.MyJumpForce();

    }

    private void OnDisable()
    {
        _playerActions.InGamePlayer.Disable();
        
    }
    
    // // // // // //
    // GIZMOS BABY
    private void OnDrawGizmos()
    {
       // Gizmos.color = Color.red;

        //Gizmos.DrawSphere(_boxCenter, _circleSize);
    }
}
