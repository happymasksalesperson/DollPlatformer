using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //for the player movement
    private Rigidbody2D rb;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Vector2 movementInput = Vector2.zero;

    [SerializeField] private float playerSpeed = 3.0f;
    [SerializeField] private float jumpHeight = 6f;
    [SerializeField] private LayerMask jumpableGround;

    private BoxCollider2D coll;
    private bool jumped = false;

    //tracks player facing dir
    private bool _facingRight;

    public bool FacingRight()
    {
        return _facingRight;
    }

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

    private DollPlayerAnimationStates _animStates;

    private void Awake()
    {
        currentState = PlayerState.Idling;

        _animStates = GetComponentInChildren<DollPlayerAnimationStates>();

        //_animStates.ChangeMoveInt(0);
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //move the player
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //if its grounded then allow the player to jump
        if (IsGrounded() == true)
        {
            jumped = true;
        }
    }

    private bool IsGrounded()
    {
        //create a box to detect whether the player is standing on the ground or not.
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    void Update()
    {
        //for the player movement left and right.
        Vector3 move = new Vector3(movementInput.x, 0, 0);
        rb.AddRelativeForce(move * playerSpeed);

        //if jumped is true then make the players rb jump.
        if (jumped == true)
        {
            rb.AddRelativeForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            jumped = false;
        }
    }
}