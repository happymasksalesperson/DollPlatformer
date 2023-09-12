using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : MonoBehaviour
{
    public PlayerBrain playerModel;

    public event Action move;
    
    private void OnEnable()
    {
        move?.Invoke();
    }

    private void Update()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
