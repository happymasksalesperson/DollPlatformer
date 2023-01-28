using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTakeDamageState : MonoBehaviour
{
    [SerializeField] private float takeDamageTime;

    private NPCModelView modelView;

    private StateManager stateManager;

    private StatsComponent stats;

    private void OnEnable()
    {
        modelView = GetComponentInChildren<NPCModelView>();

        stateManager = GetComponent<StateManager>();

        modelView.OnTakeDamage();

        stats = GetComponent<StatsComponent>();

        stats.vulnerable = false;

        StartCoroutine(TakeDamage());
    }

    private IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(takeDamageTime);
        
        if(stats.isAlive)
        stateManager.ChangeStateString("patrol");
    }

    private void OnDisable()
    {
        stats.vulnerable = true;
    }
}