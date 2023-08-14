using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerStandRangeAttack01 : MonoBehaviour
{
    public PlayerStateManager stateManager;
    
    public PlayerModelView modelView;

    public PlayerControlsMiddleMan middleMan;

    public PlayerControls controls;

    public float castTime;

    public float projectileForce;

    public ObjectPool pool;

    public Transform shootTransform;

    public GameObject projectile;

    public Rigidbody rb;

    public bool buffered = false;

    public void OnEnable()
    {
        middleMan.inControl = false;
        modelView = stateManager.modelView;
        modelView.OnChangeState(PlayerStates.StandRangeAttack01);

        controls.AttackEvent += BufferAttack;

        StartCoroutine(RangeAttack());
    }

    private void OnDisable()
    {
        controls.AttackEvent -= BufferAttack;
    }

    private void BufferAttack()
    {
        buffered = true;
    }

    private IEnumerator RangeAttack()
    { 
        projectile = pool.GetPooledObject();

        projectile.transform.position = shootTransform.position;

        rb = projectile.GetComponent<Rigidbody>();

        if(middleMan.facingRight)
            rb.AddForce(projectileForce, 0,0);

        else
        {
            rb.AddForce(-projectileForce, 0, 0);
        }

        yield return new WaitForSeconds(castTime);

        if (buffered)
        {
            buffered = false;
            StartCoroutine(RangeAttack());
            yield break;
        }

        middleMan.inControl = true;

        if(middleMan.grounded)
            stateManager.ChangeState(PlayerStates.Idle);

        else
        {
            stateManager.ChangeState(PlayerStates.Fall);
        }
    }
}
