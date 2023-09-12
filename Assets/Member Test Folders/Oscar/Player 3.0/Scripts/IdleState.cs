using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour
{
    public event Action IdleEvent;
    private void OnEnable()
    {
        IdleEvent?.Invoke();
    }

    private void OnDisable()
    {
        
    }
}
