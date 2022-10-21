using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NPC01PatrolState : MonoBehaviour
{
    // // // // // //
    // STATS
    [SerializeField] private float _sightDistance;
    [SerializeField] private float _speed;

    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask wallMask;
    
    RaycastHit hit;

    private Animator _anim;
    
    private Rigidbody _rb;

    private bool isAttacking=false;
    private bool takingDamage=false;

    private void Update()
    {
        _anim.SetBool("TakingDamage", takingDamage);
        _anim.SetBool("Attacking", isAttacking);
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
    
    // // // // // //
    // MOVEMENT

    private void HandleMovement()
    {
        if (Physics.Raycast(_rb.transform.position, _rb.transform.TransformDirection(Vector3.left), out hit, _sightDistance,
                playerMask))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * _sightDistance, Color.white);
        }
    }
    
    // // // // // //
    // SIGHT
    
    // // // // // //
    // ATTACK
    public void Attack()
    {
        StartCoroutine(Attacking());
    }

    private IEnumerator Attacking()
    {
        isAttacking = true;
        yield return new WaitForSeconds(.75f);
        isAttacking = false;
    }

    // // // // // //
    // TAKEDAMAGE
    public void TakeDamage()
    {
        StartCoroutine(TakingDamage());
    }
    
    private IEnumerator TakingDamage()
    {
        takingDamage = true;
        yield return new WaitForSeconds(.75f);
        takingDamage = false;
    }

    // // // // // //
    // ON ENABLE / DISABLE

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        
        
    }

    private void OnDisable()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.left) * _sightDistance;
        Gizmos.DrawRay(transform.position, direction);
    }
}
