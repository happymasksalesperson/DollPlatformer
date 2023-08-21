using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCheckpointModel : MonoBehaviour, ITakeDamage
{
    private DataPersistenceManager saveManager;

    private void Start()
    {
        saveManager = DataPersistenceManager.instance;
    }

    public event Action checkpointEvent;
    public void ChangeHP(int x)
    {
        saveManager.SaveGame();
        checkpointEvent?.Invoke();
    }
}
