using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeAttack : MonoBehaviour
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

    public int numPooledProjectiles;

    public Rigidbody rb;

    public bool buffered = false;

    public bool isAttacking = false;

    public void OnAttack()
    {
        if (!middleMan.canRangeAttack)
            return;

        if (isAttacking)
        {
            buffered = true;
            return;
        }

        if (middleMan.grounded)
        {
            middleMan.inControl = false;
        }

        isAttacking = true;

        modelView.OnChangeState(PlayerStates.StandRangeAttack01);

        StartCoroutine(RangeAttack());
    }

    private void OnEnable()
    {
        controls.AttackEvent += OnAttack;
        pool.SetPoolSizeAndCreate(projectile, numPooledProjectiles);
    }

    private void OnDisable()
    {
        controls.AttackEvent -= OnAttack;
    }

    private IEnumerator RangeAttack()
    {
        projectile = pool.GetPooledObject();

        projectile.transform.position = shootTransform.position;

        rb = projectile.GetComponent<Rigidbody>();

        if (middleMan.facingRight)
            rb.AddForce(projectileForce, 0, 0);

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
        
        isAttacking = false;

        middleMan.inControl = true;

        if (middleMan.grounded && middleMan.moving)
            modelView.OnChangeState(PlayerStates.Run);

        else if (middleMan.grounded)
            modelView.OnChangeState(PlayerStates.Idle);

        else
        {
            modelView.OnChangeState(PlayerStates.Fall);
        }
    }
}