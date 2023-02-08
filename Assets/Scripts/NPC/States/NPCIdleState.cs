using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleState : MonoBehaviour
{
    //play idle anims

    private NPCModelView modelView;

    private Rigidbody rb;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        
        modelView = GetComponentInChildren<NPCModelView>();
        modelView.OnIdle();
    }
}