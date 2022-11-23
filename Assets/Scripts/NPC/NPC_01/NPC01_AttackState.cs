using System;
using System.Collections;
using System.Collections.Generic;
using NPC01;
using UnityEngine;

public class NPC01_AttackState : MonoBehaviour
{
    private NPC01.StateManager _stateManager;

    public NPC01_PatrolState _patrolState;

    private StatsComponent _stats;

    private float _attackTime;
    private float _attackPower;
    
    private void OnEnable()
    {
        _stateManager = GetComponent<NPC01.StateManager>();
        _stats = GetComponent<StatsComponent>();
        _attackTime = _stats.MyAttackTime();
        
        StartCoroutine(Jab());
    }

    private IEnumerator Jab()
    {
        NPC01.EventManager.NPC01Attack01Function();
        
        yield return new WaitForSeconds(_attackTime);
        
        _stateManager.ChangeState(_patrolState);

    }

    private void OnDisable()
    {
        
    }
}
