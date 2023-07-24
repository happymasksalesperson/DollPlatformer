using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    public float slideForce;

    public float crouchTime;

    public PlayerStateManager stateManager;

    public PlayerControls controls;

    public PlayerControlsMiddleMan middleMan;

    public Rigidbody rb;

    public bool facingRight;

    private bool isSliding;

    private float slideStartTime;

    public void OnEnable()
    {
        slideStartTime = 0;
        facingRight = middleMan.facingRight;
        Slide();
    }

    private void Update()
    {
        if (!middleMan.grounded)
            stateManager.ChangeState(PlayerStates.Fall);
    }

    private void Slide()
    {
        if (isSliding)
            return;

        isSliding = true;
        slideStartTime = Time.time;

        float horizontalForce = facingRight ? slideForce : -slideForce;
        rb.AddForce(horizontalForce,0,0);

        StartCoroutine(CrouchSlide());
    }

    private IEnumerator CrouchSlide()
    {
        while (Time.time < slideStartTime + crouchTime)
        {
            yield return null;
        }

        isSliding = false;

        if (!middleMan.grounded)
            stateManager.ChangeState(PlayerStates.Fall);
        else if (middleMan.grounded && controls.aimInput < 0)
            stateManager.ChangeState(PlayerStates.Crouch);
        else
        {
            stateManager.ChangeState(PlayerStates.Idle);
        }
    }
}