using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;

    public bool isGrounded=true;

    private void OnTriggerStay(Collider col)
    {
        isGrounded = col != null && (((1 << col.gameObject.layer) * _groundLayer != 0));
    }

    private void OnTriggerExit(Collider col)
    {
        isGrounded = false;
    }
    
    public LayerMask groundLayer;

    
    //CHAT GPT WROTE THE BELOW CODE :)
    //
    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionStay(Collision collision) {
        // Check if the collision is with a game object on the ground layer
        if (((1 << collision.gameObject.layer) & groundLayer) != 0) {
            // Disable gravity on the Rigidbody
            rb.useGravity = false;

            // Alternatively, you can set the Rigidbody's velocity to zero to stop it from falling
            // rb.velocity = Vector3.zero;
        }
    }

    void OnCollisionExit(Collision collision) {
        // Check if the collision is with a game object on the ground layer
        if (((1 << collision.gameObject.layer) & groundLayer) != 0) {
            // Enable gravity on the Rigidbody
            rb.useGravity = true;
        }
    }
}
