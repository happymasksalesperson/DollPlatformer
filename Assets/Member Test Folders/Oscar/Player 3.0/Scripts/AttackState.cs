using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : HelpfulFunctions
{
    public event Action actionEvent;
    private void OnEnable()
    {
        actionEvent?.Invoke();
    }

    private void Update()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
