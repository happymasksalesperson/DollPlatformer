using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public ObjectPool owner;
    public Transform spawnPosition;
    public bool active;
    public Rigidbody rb;

    public void SetOwner(ObjectPool newOwner)
    {
        owner = newOwner;
    }

    public void ChangeActive(bool newActive)
    {
        active = newActive;

        if (!active)
            owner.AddObjectBackToPool(gameObject);

            gameObject.SetActive(active);
    }
}
