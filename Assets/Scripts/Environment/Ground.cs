using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public float pushMagnitude;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        if (rb == null)
        {
            return;
        }

        Vector3 pushDirection = rb.velocity.normalized * pushMagnitude;
        rb.AddForce(pushDirection, ForceMode.Impulse);
    }
}
    