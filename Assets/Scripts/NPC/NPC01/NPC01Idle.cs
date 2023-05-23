using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC01Idle : MonoBehaviour
{
    public NPC01Brain brain;

    public void OnEnable()
    {
        brain = GetComponentInParent<NPC01Brain>();
        
        brain.modelView.OnIdle();
    }

    public void BreakIdle()
    {
        brain.idle = false;
    }
}
