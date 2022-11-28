using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;

    public bool isGrounded;

    private void OnTriggerStay(Collider col)
    {
        isGrounded = col != null && (((1 << col.gameObject.layer) * _groundLayer != 0));
    }

    private void OnTriggerExit(Collider col)
    {
        isGrounded = false;
    }
}
