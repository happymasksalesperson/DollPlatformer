using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Vector3 LOSPos;
    public float POSYValue;
    public Vector3 LOSExtents;
    public LayerMask groundLayer;

    public bool grounded;

    private void Start()
    {
        StartCoroutine(GroundScan());
    }

    private IEnumerator GroundScan()
    {
        while (true)
        {
            Collider[] hits = Physics.OverlapBox(new Vector3(transform.position.x, transform.position.y-POSYValue, transform.position.z), LOSExtents / 2, Quaternion.identity, groundLayer);
            grounded = hits.Length > 0;
            yield return new WaitForFixedUpdate();
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y - POSYValue, transform.position.z), LOSExtents);
    }
    /*[SerializeField] private LayerMask groundLayer;

    public bool grounded;

    private void OnTriggerStay(Collider col)
    {
        grounded = col != null && (((1 << col.gameObject.layer) * groundLayer != 0));
    }

    private void OnTriggerExit(Collider col)
    {
        grounded = false;
    }*/
}