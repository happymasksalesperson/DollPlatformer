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

        if (originTransform == transform)
        {
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Raycast hit: " + hit.transform.name);
                Shoot();
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
                transform.LookAt(targetTransform);
            }

            else
            {
                Debug.Log("Raycast didn't hit anything.");
                Debug.DrawRay(ray.origin, ray.direction, Color.green);
            }
        }
    }

    public void Shoot()
    {
        Vector3 direction = targetTransform.position - originTransform.position;

        GameObject newBullet = Instantiate(projectile, originTransform.position, Quaternion.identity);
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();
        
        NPC01Needle needle = newBullet.GetComponent<NPC01Needle>();
        if (needle != null)
        {
            if (direction.x >= 0)
            {
                needle.FacingRight(true);
            }
        }

        rb.AddForce(direction * shootForce, ForceMode.VelocityChange);
    }
}
