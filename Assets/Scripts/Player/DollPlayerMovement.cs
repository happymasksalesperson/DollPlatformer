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
    
    public BoxCollider _boxCollider;

    private GroundCheck _groundCheck;
    
    private PlayerActions _playerActions;
    private Vector2 _movement;

    private DollPlayerStats playerStats;

    private DollPlayerAnimationStates _animStates;
    
    //tracks player facing dir
    private bool _facingRight;

    public bool FacingRight()
    {
        return _facingRight;
    }

    // // // // // //
    // AIMING
    //
    private Vector2 _aimVector;
    
    // // // // // //
    // ATTACKING
    //
    private float _attackTime;

    private bool _isAttack;

    public bool IsAttack()
    {
        return _isAttack;
    }
    
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
    public enum PlayerState
    {
        Idling,
        Crouching,
        Running,
        Attacking,
        Jumping,
        TakeDamage,
    }
    public PlayerState currentState;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _groundCheck = GetComponentInChildren<GroundCheck>();

        currentState = PlayerState.Idling;

        _animStates = GetComponentInChildren<DollPlayerAnimationStates>();

        _animStates.ChangeMoveInt(0);
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
        _rb.velocity = new Vector3(_movement.x * _runSpeed, _movement.y);
        
        if (_rb.velocity.magnitude > _maxSpeed)
        {
            _rb.velocity = _rb.velocity.normalized * _maxSpeed;
        } 
        
        if(_jumping)
                 _animStates.ChangeMoveInt(3);

        if (_movement.x > 0)
        {
            _animStates.ChangeMoveInt(2);
            _facingRight = false;
        }
        else if (_movement.x < 0)
        {
            _animStates.ChangeMoveInt(2);
            _facingRight = true;
        }

        else if (IsGrounded() && _aimVector == Vector2.down)
            Crouch();

        else if (IsGrounded() && _movement.y == 0)
        {
            _animStates.ChangeMoveInt(0);
        }
        
       
    }
    
    // // // // // //
    // CROUCHING
    //
    // add crouch attack? idle animations? etc
    private void Crouch()
    {
        _animStates.ChangeMoveInt(1);
    }

    // // // // // //
    // ATTACKING
    //
    public void Attack(InputAction.CallbackContext context)
    {
        _isAttack = true;
        if (_aimVector == Vector2.up)
        {
            _animStates.ChangeAttackInt(1);
        }
        
        else if  (_aimVector == Vector2.down)
        {
            _animStates.ChangeAttackInt(2);
        }
        
        else
        _animStates.ChangeAttackInt(0);
        
        StartCoroutine(Attacking());
    }
    private IEnumerator Attacking()
    {
        Debug.Log("Hiyah");
        
        currentState = PlayerState.Attacking;
        
        _animStates.ChangeMoveInt(4);

        yield return new WaitForSeconds(_attackTime);

        _isAttack = false;
    }
    
    // // // // // //
    // JUMPING
    //
    public void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            
            _rb.AddForce(Vector3.up*_jumpForce);
            _jumping = true;
            StartCoroutine(GroundCheckAfterJump());
        }
    }

    private bool IsGrounded()
    {
        //Debug.Log(_groundCheck.isGrounded);
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
        if (_groundCheckEnabled && IsGrounded())
        {
            _jumping = false;
        }

        else if (_jumping && _rb.velocity.y > 0f)
        {
            _gravity.ChangeGravity(_defaultGravity * _jumpFallGravityMultiply); 
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
        _runSpeed = playerStats.runSpeed;
        _maxSpeed = playerStats.maxSpeed;
        _jumpForce = playerStats.jumpForce;
        _attackTime = playerStats.attack01Time;

    }

    private void OnDisable()
    {
      //  _playerActions.InGamePlayer.Disable();
        
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
