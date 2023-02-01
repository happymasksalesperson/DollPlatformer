using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEventManager : MonoBehaviour
{
    public static LevelEventManager LevelEventInstance { get; private set; }

    private void Awake()
    {
        LevelEventInstance = this;
    }
    
    public event Action<bool> CanTalk;

    public void OnCanTalk(bool x)
    {
        CanTalk?.Invoke(x);
    }
    
    public event Action StopTalk;

    public void OnStopTalk()
    {
        StopTalk?.Invoke();
    }
}
