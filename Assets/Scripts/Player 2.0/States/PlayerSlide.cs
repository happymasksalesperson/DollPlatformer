using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    public float slideForce;

    public float slideTime;

    public PlayerStateManager stateManager;

    public PlayerControls controls;

    public PlayerControlsMiddleMan middleMan;

    public Rigidbody rb;

    public bool facingRight;

    private bool isSliding;

    private float slideStartTime;

    public void OnEnable()
    {
        middleMan.inControl = false;
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
        isSliding = true;
        slideStartTime = Time.time;

        float horizontalForce = facingRight ? slideForce : -slideForce;
        rb.AddForce(horizontalForce,0,0, ForceMode.VelocityChange);

        StartCoroutine(CrouchSlide());
    }

    private IEnumerator CrouchSlide()
    {
        while (Time.time < slideStartTime + slideTime)
        {
            yield return null;
        }

        isSliding = false;

        middleMan.inControl = true;

        if (!middleMan.grounded)
            stateManager.ChangeState(PlayerStates.Fall);

        else if (middleMan.grounded && controls.aimInput < 0)
            stateManager.ChangeState(PlayerStates.Crouch);
        
        else
            stateManager.ChangeState(PlayerStates.Idle);
    }
}