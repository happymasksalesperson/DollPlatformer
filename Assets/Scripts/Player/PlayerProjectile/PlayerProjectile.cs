using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerProjectile : IPooledObject
{
    public void Start()
    {
        Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Player"), true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleCollision(other);
    }

    private void HandleCollision(Collider col)
    {
        if (col.GetComponent<ITakeDamage>() != null)
        {
            ITakeDamage damageTaker = col.GetComponent<ITakeDamage>();
            damageTaker.ChangeHP(-1);
        }

        //Debug.Log("Hit something: " + col.gameObject.name);
        OnHit();
    }

    private void OnHit()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        owner.AddObjectBackToPool(gameObject);
    }

    public void SetObjectPoolOwner(ObjectPool owner)
    {

    }

    public void SetActiveObject(bool active)
    {
        throw new System.NotImplementedException();
    }
}