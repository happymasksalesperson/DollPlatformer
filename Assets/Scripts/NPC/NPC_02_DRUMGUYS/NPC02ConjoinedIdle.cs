using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC02ConjoinedIdle : MonoBehaviour
{
    public MonoBehaviour conjoinedKnockback;

    public StateManager stateManager;
        
    private NPCModelView modelView;

    private void OnEnable()
    {
        conjoinedKnockback = GetComponent<NPC02ConjoinedKnockback>();
        
        modelView = GetComponentInChildren<NPCModelView>();
        modelView.OnIdle();

        modelView.TakeDamage += KnockedOff;
    }

    private void KnockedOff()
    {
        stateManager.ChangeState(conjoinedKnockback);
    }

    private void OnDisable()
    {
        modelView.TakeDamage -= KnockedOff;
    }
}
