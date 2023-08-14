using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Vector3 LOSPos;
    public float POSYValue;
    public Vector3 LOSExtents;
    public LayerMask groundLayer;

    public bool grounded;
    public bool trueGround;

    private void FixedUpdate()
    {
        GroundScan();
    }

    private void GroundScan()
    {
        Collider[] hits = Physics.OverlapBox(
            new Vector3(transform.position.x, transform.position.y - POSYValue, transform.position.z), LOSExtents / 2,
            Quaternion.identity, groundLayer);
        grounded = hits.Length > 0;

        trueGround = false;

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].gameObject.layer == LayerMask.NameToLayer("UnpassableGround"))
            {
                trueGround = true;
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y - POSYValue, transform.position.z),
            LOSExtents);
    }
}