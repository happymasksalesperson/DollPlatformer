using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC01_AttackState : MonoBehaviour
{
    private StateManager _stateManager;

    private NPCEventManager eventManager;

    public NPCPatrolState _patrolState;

    private StatsComponent _stats;

    private float _attackTime;
    private float _attackPower;
    
    private void OnEnable()
    {
        _stateManager = GetComponent<StateManager>();
        _stats = GetComponent<StatsComponent>();
        _attackTime = _stats.MyAttackTime();
        
        StartCoroutine(Jab());
    }

    private IEnumerator Jab()
    {
        eventManager.OnAttack01();
        
        yield return new WaitForSeconds(_attackTime);
        
        _stateManager.ChangeState(_patrolState);
    }

    private void OnDisable()
    {
        
    }
}
