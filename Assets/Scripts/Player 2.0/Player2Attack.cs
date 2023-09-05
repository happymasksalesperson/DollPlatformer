using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Attack : MonoBehaviour
{
    public PlayerStateManager stateManager;

    public PlayerControlsMiddleMan middleMan;

    public PlayerControls controls;

    public PlayerRangeAttack rangeAttack;

    public bool facingRight;

    public float attackDetectionDistance;

    public LayerMask damageLayer;

    public float xOffset;

    public int attackDamage;

    public bool canSlide=true;

    public void Start()
    {
        controls.AttackEvent += Attack;
    }

    public void Attack()
    {
        facingRight = middleMan.facingRight;

        if (stateManager.currentState == PlayerStates.Crouch && canSlide)
        {
            stateManager.ChangeState(PlayerStates.Slide);
        }

        else if (stateManager.currentState == PlayerStates.Idle || stateManager.currentState == PlayerStates.Run || stateManager.currentState == PlayerStates.Jump || stateManager.currentState == PlayerStates.Fall)
        {
            Vector3 boxPosition = transform.position + new Vector3(xOffset * (facingRight ? 1 : -1), 0f, 0f);
            Vector3 boxHalfExtents = new Vector3(attackDetectionDistance, attackDetectionDistance, attackDetectionDistance);

            RaycastHit[] hits = new RaycastHit[10];
            int numCollisions = Physics.BoxCastNonAlloc(boxPosition, boxHalfExtents, Vector3.forward, hits, Quaternion.identity, 0f, damageLayer, QueryTriggerInteraction.Collide);

            if (numCollisions > 0)
            {
                for (int i = 0; i < numCollisions; i++)
                {
                    RaycastHit hit = hits[i];
                    ITakeDamage damageReceiver = hit.collider.GetComponent<ITakeDamage>();
                    if (damageReceiver != null)
                    {
                        damageReceiver.ChangeHP(-attackDamage);
                        stateManager.ChangeState(PlayerStates.StandMeleeAttack01);
                        return;
                    }
                }
            }

            rangeAttack.OnAttack();
        }
    }

    public void OnDisable()
    {
        controls.AttackEvent -= Attack;
    }
}
