using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectIdle : MonoBehaviour, ITakeDamage
{
    private StateManager stateManager;

    private void OnEnable()
    {
        stateManager = GetComponent<StateManager>();
    }

    public void ChangeHP(int x)
    {
        stateManager.ChangeStateString("death");
    }
}
