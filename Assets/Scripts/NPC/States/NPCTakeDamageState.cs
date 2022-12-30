using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTakeDamageState : MonoBehaviour
{
    [SerializeField] private float takeDamageTime;

    private NPCModelView modelView;

    private StateManager stateManager;

    private void OnEnable()
    {
        modelView = GetComponentInChildren<NPCModelView>();

        stateManager = GetComponent<StateManager>();

        modelView.OnTakeDamage();

        StartCoroutine(TakeDamage());
    }

    private IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(takeDamageTime);

        stateManager.ChangeStateString("patrol");
    }
}