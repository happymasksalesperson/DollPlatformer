using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    public PlayerStateManager stateManager;

    public PlayerControlsMiddleMan middleMan;

    public PlayerModelView modelView;

    [Header("HOW FAR KNOCKED BACK HORIZONTAL")]
    public float horizontalDist;

    [Header("HOW FAR KNOCKED BACK VERTICAL")]
    public float verticalDist;

    public float disableTime;

    private bool facingRight;

    public Rigidbody rb;

    public void OnEnable()
    {
        modelView = stateManager.modelView;
        middleMan.inControl = false;
        modelView.OnChangeState(PlayerStates.TakeDamage);
        facingRight = middleMan.facingRight;
        StartCoroutine(Knockback());
    }

    private IEnumerator Knockback()
    {
        float dist;

        if (!facingRight)
            dist = horizontalDist * -1;

        else
            dist = horizontalDist;

        rb.AddForce(new Vector3(dist, verticalDist, 0), ForceMode.Acceleration);

        yield return new WaitForSeconds(disableTime);


        if(middleMan.grounded)
            stateManager.ChangeState(PlayerStates.Idle);

        else
        {
            stateManager.ChangeState(PlayerStates.Fall);
        }
    }

    private void OnDisable()
    {
        middleMan.inControl = true;
        if (middleMan.grounded)
            middleMan.canJump = true;
    }
}