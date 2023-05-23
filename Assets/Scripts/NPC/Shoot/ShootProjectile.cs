using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public Transform originTransform;
    public Transform targetTransform;

    public GameObject projectile;

    public float shootForce;

    public Rigidbody rb;
    
    [Button]
    private void Aim()
    {
        Ray ray = new Ray(originTransform.position, targetTransform.position - originTransform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Raycast hit: " + hit.transform.name);
            Shoot();
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
        }

        else
        {
            Debug.Log("Raycast didn't hit anything.");
            Debug.DrawRay(ray.origin, ray.direction, Color.green);
        }
    }

    public void Shoot()
    {
        Vector3 direction = targetTransform.position - originTransform.position;
        direction.Normalize();

        GameObject newBullet = Instantiate(projectile, originTransform.position, Quaternion.identity);
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();

        NPC01Needle needle = newBullet.GetComponent<NPC01Needle>();
        if (needle != null)
        {
            if (direction.x >= 0)
            {
               // brain.FacingRight(true);
                needle.FacingRight(true);
            }
            else
            {
               // brain.FacingRight(false);
                needle.FacingRight(false);
            }

            needle.active = true;
        }

        rb.rotation.SetLookRotation(direction);
        rb.AddForce(direction * shootForce, ForceMode.VelocityChange);
    }

}
