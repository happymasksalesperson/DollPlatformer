using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollPlayerCrouchState : MonoBehaviour
{
    private DollPlayerModelView modelView;
    
    private void OnEnable()
    {
        modelView = GetComponentInChildren<DollPlayerModelView>();
        
        modelView.OnCrouch();
    }
}
