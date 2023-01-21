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

    //
    private bool conjoined;

    private void OnEnable()
    {
        _stateManager = GetComponent<StateManager>();
        _stats = GetComponent<StatsComponent>();

        conjoined = _stats.conjoined;

        attack01Time = _stats.MyAttackTime();

        modelView = GetComponentInChildren<NPCModelView>();

        StartCoroutine(Jab());
    }

    private IEnumerator Jab()
    {
        modelView.OnAttack01Windup();

        yield return new WaitForSeconds(windup01Time);

        if (conjoined)
        {
            StartCoroutine(CojoinedAttack01());
            yield break;
        }

        modelView.OnAttack01();

        yield return new WaitForSeconds(attack01Time);

        _stateManager.ChangeStateString("patrol");
    }

    private IEnumerator CojoinedAttack01()
    {
        _stateManager.ChangeStateString("attack");

        modelView.OnAttack01();

        yield return new WaitForSeconds(attack01Time);

        modelView.OnIdle();

        _stateManager.ChangeStateString("idle");
    }
}