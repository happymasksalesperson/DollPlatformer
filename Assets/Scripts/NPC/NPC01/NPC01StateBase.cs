using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC01StateBase : MonoBehaviour
{
    public NPC01Brain brain;

    public Rigidbody rb;

    public Gravity gravity;

    public bool enabled = false;

    public virtual void OnEnable()
    {
        brain = GetComponentInParent<NPC01Brain>();

        gravity = brain.gravity;
        
        rb = GetComponentInParent<Rigidbody>();
    }

    public void StartState()
    {
        enabled = true;
    }

    public void OnDisable()
    {
        enabled = false;
    }
}
