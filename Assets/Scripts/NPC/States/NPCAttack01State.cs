using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttack01State : MonoBehaviour
{
    private StateManager _stateManager;

    private NPCModelView modelView;
    private StatsComponent _stats;

    [SerializeField] private float attack01Time;

    [SerializeField] private float windup01Time;

    private void OnEnable()
    {
        _stateManager = GetComponent<StateManager>();
        _stats = GetComponent<StatsComponent>();
        attack01Time = _stats.MyAttackTime();

        modelView = GetComponentInChildren<NPCModelView>();
        
        StartCoroutine(Jab());
    }

    private IEnumerator Jab()
    {
        modelView.OnAttack01Windup();

        yield return new WaitForSeconds(windup01Time);

        modelView.OnAttack01();
        
        yield return new WaitForSeconds(attack01Time);
        
        _stateManager.ChangeStateString("patrol");
    }
}
