using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NPCPatrolState : MonoBehaviour
{
    private StateManager _stateManager;
    //

    RaycastHit hitInfo;
    private RaycastHit wallInfo;

    private Rigidbody _rb;

    // // // // // //
    // STATS
    private StatsComponent _stats;

    //floats for determining distance from Player
    //NPC01 speeds up when spotting the Player and attacks once within range
    private float _distanceToPlayer;
    public float _minDist;

    private bool _moving;

    private float _speed;
    private float _maxSpeed;

    private float _sightDistance;

    private float _patrolTime;
    private float _idleTime;

    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask wallMask;

    //bool for which direction NPC is facing (false left, true right)
    private bool _facingDir = false;
    private Vector3 _facingDirVector;

    public NPCModelView modelView;

    // // // // // //
    // ON ENABLE / DISABLE

    private void OnEnable()
    {
        _moving = true;

        _rb = GetComponent<Rigidbody>();
        _stateManager = GetComponent<StateManager>();

        modelView = GetComponentInChildren<NPCModelView>();
        modelView.OnPatrol();
        
        _stats = GetComponent<StatsComponent>();
        _stats.vulnerable = true;

        _maxSpeed = _stats.maxSpeed;
        _speed = _stats.moveSpeed;
        _idleTime = _stats.idleTime;
        _patrolTime = _stats.patrolTime;
        _minDist = _stats.minDist;

        _sightDistance = _stats.sightDistance;

        StartCoroutine(Patrolling());
    }

    private void FixedUpdate()
    {
        HandleSight();

        if (_moving)
        {
            HandleMovement();
        }
    }

    // // // // // //
    // MOVEMENT

    private void HandleMovement()
    {
        if (_moving)
        {
            if (!_facingDir)
            {
                _facingDirVector = new Vector3(-1, 0, 0);
                transform.eulerAngles = new Vector2(0, 0);
            }

            else if (_facingDir)
            {
                _facingDirVector = new Vector3(1, 0, 0);
                transform.eulerAngles = new Vector2(0, 180);
            }

            _rb.velocity = new Vector3(_facingDirVector.x * _speed, 0f, 0f);

            if (_rb.velocity.magnitude > _maxSpeed)
            {
                _rb.velocity = _rb.velocity.normalized * _maxSpeed;
            }
        }
    }

    private IEnumerator Patrolling()
    {
        _moving = true;

        yield return new WaitForSecondsRealtime(_patrolTime);

        _moving = false;
        _rb.velocity = Vector3.zero;

        yield return new WaitForSecondsRealtime(_idleTime);

        float x = UnityEngine.Random.Range(1f, 10f);
        if (x < 5)
        {
            Flip();
        }

        StartCoroutine(Patrolling());
    }

    // // // // // //
    // SIGHT
    //
    // flips facing bool
    private void Flip()
    {
        _facingDir = !_facingDir;
        _stats.ChangeFacingDirection(_facingDir);
    }

    private void HandleSight()
    {
        hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(new Vector3(transform.position.x, transform.position.y-1, transform.position.z), _facingDirVector, out hitInfo, _sightDistance, playerMask);

        if (hit)
        {
            GameObject playerObj = hitInfo.transform.gameObject;
            Debug.DrawRay(transform.position, _facingDirVector * _sightDistance, Color.red);
            
            IPlayer player = playerObj.GetComponent<IPlayer>();
            _distanceToPlayer = Vector3.Distance(transform.position, hitInfo.transform.position);
            if (_distanceToPlayer < _minDist)
            {
                _stateManager.ChangeStateString("attack01");
            }
        }

        wallInfo = new RaycastHit();
        bool wallHit = Physics.Raycast(transform.position, _facingDirVector, out wallInfo, _minDist, wallMask);
        if (wallHit)
        {
            Flip();
        }

        else
        {
            Debug.DrawRay(transform.position, _facingDirVector * _sightDistance, Color.white);
        }   
    }

    // // // // // //
    // 
    private void OnDisable()
    {
        _rb.velocity = new Vector3(0, 0, 0);
        _moving = false;
    }
}