using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MonoBehaviour
{
    public event Action JumpEvent;
    private void OnEnable()
    {
        JumpEvent?.Invoke();
    }

    private void Update()
    {
        
    }

    private void OnDisable()
    {
        //playerModel.fallBool = true;
    }
}
