using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;

    public bool grounded;

    private void OnTriggerStay(Collider col)
    {
        grounded = col != null && (((1 << col.gameObject.layer) * groundLayer != 0));
    }

    private void OnTriggerExit(Collider col)
    {
        grounded = false;
    }
}