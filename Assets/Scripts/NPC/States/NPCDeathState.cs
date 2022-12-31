using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDeathState : MonoBehaviour
{
    private HealthModelView modelView;
    
    private Rigidbody rb;

    private BoxCollider box;

    private SphereCollider sphere;

    private SpriteRenderer rend;

    //how much rb spins
    [SerializeField] private float torque;
    
    //how much NPC "jumps" up on Death
    [SerializeField] private float verticalDist;
    
    //how far NPC travels horizontally
    //tie to hitDir
    [SerializeField] private float horizontalDist;
    
    //change direction depending on hit direction
    //how to? read Player facing info? NPC facing info?
    [SerializeField] private int hitDir;

    private Gravity gravity;

    private void OnEnable()
    {
        modelView = GetComponentInChildren<HealthModelView>();
        
        rb = GetComponent<Rigidbody>();

        //detects colliders and turns them off
        //here I leave an intentional null reference...? investigate further
        //have to add a seperate check for spheres vs boxes
        box = GetComponent<BoxCollider>();
        if(box)
            box.enabled = false;
        else
        {
            sphere = GetComponent<SphereCollider>();
            if (sphere!=null)
                sphere.enabled = false;
        }

        rend = GetComponentInChildren<SpriteRenderer>();

        gravity = GetComponent<Gravity>();
        
        rb.AddForce(new Vector3(horizontalDist, verticalDist, 0), ForceMode.Impulse);
        
        //unlocks faster spin
        rb.maxAngularVelocity = 100000f;
        
        //transform.hitDir (see above)
        rb.AddTorque(transform.forward * torque * hitDir);

        gravity.enabled = true;
        StartCoroutine(Die());
    }

    //destroys gameobject once renderer is out of sight
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(1);

        if (!rend.isVisible)
        {
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }

        else StartCoroutine(Die());
    }
}
