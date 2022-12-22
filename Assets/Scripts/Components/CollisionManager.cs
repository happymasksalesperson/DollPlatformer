using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A generic manager script
/// handles multiple colliders/ triggers on one object
/// </summary>
public class CollisionManager : MonoBehaviour
{
    public event Action<Collision> OnCollisionEnterEvent;
    public event Action<Collider> OnTriggerEnterEvent;
    public event Action<Collision> OnCollisionExitEvent;
    public event Action<Collider> OnTriggerExitEvent;

    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionEnterEvent?.Invoke(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(other);
    }

    private void OnCollisionExit(Collision collision)
    {
        OnCollisionExitEvent?.Invoke(collision);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExitEvent?.Invoke(other);
    }
}
