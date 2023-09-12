using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : HelpfulFunctions
{
    public event Action crouchEvent;
    private void OnEnable()
    {
        crouchEvent?.Invoke();
    }

    private void Update()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
