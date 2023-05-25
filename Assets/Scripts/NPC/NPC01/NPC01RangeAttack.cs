using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class NPC01RangeAttack : MonoBehaviour
{
    /// <summary>
    /// NPC01 should only enter this state if he has his needle projectile equipped
    /// 
    /// fires a raycast between shootFromTransform and targetTransform
    /// the length of the direction magnitude
    ///
    /// adds force to projectile and unequips it
    /// 
    /// </summary>
    
    public Transform originTransform;

    public Transform targetTransform;

    public GameObject projectile;

    public float shootForce;

    public Rigidbody rb;

    public float aimTime;
    public float aimTimeThresh;

    public bool counting;

    public NPC01Brain brain;
    
    public Vector3 targetVector;

    public void OnEnable()
    {
        brain = GetComponentInParent<NPC01Brain>();
        originTransform = brain.myTransform;
        targetTransform = brain.oscarVision.PlayerTransform();

        Aim();
        StartTimer();
    }

    [Button]
    public void Aim()
    {
        if (targetTransform == null)
        {
            targetVector = brain.lastPointOfInterest;
        }
        else
        {
            targetVector = targetTransform.position;
        }
        Ray ray = new Ray(originTransform.position, targetVector - originTransform.position);
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
        Vector3 direction = targetVector - originTransform.position;
        direction.Normalize();

        GameObject newBullet = Instantiate(projectile, originTransform.position, Quaternion.identity);
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();

        NPC01Needle needle = newBullet.GetComponent<NPC01Needle>();
        if (needle != null)
        {
            if (direction.x >= 0)
            {
                brain.FacingRight(true);
                needle.FacingRight(true);
            }
            else
            {
                brain.FacingRight(false);
                needle.FacingRight(false);
            }

            needle.active = true;
        }

        rb.rotation.SetLookRotation(direction);
        rb.AddForce(direction * shootForce, ForceMode.VelocityChange);
    }

    public void StartTimer()
    {
        counting = true;
        StartCoroutine(AimTimer());
    }

    private IEnumerator AimTimer()
    {
        while (counting)
        {
            yield return new WaitForSeconds(1);
            aimTime++;

            if (aimTime >= aimTimeThresh)
                EndState();
        }
    }

    private void EndState()
    {
        counting = false;
        StopAllCoroutines();
        brain.rangeAttack = false;
        
        if(targetTransform!=null)
        brain.lastPointOfInterest = targetTransform.position;
        
        brain.agitated = true;
    }
    
    private void OnDisable()
    {
        
    }
}