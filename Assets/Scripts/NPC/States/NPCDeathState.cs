using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCDeathState : MonoBehaviour
{
    private StatsComponent stats;

    private NPCModelView modelView;

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

    //change direction depending on facing direction
    //change to direction of incoming hit?
    [SerializeField] private int hitDir;

    [SerializeField] private bool facingDir;
    
    private Gravity gravity;

    private void OnEnable()
    {
        stats = GetComponent<StatsComponent>();
        
        modelView = GetComponentInChildren<NPCModelView>();
        
        modelView.OnDeath();
        
        rb = GetComponent<Rigidbody>();

        //detects colliders and turns them off
        //here I leave an intentional null reference...? investigate further
        //have to add a separate check for spheres vs boxes
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

        facingDir = stats.facingDirection;

        if (facingDir)
        {
            horizontalDist = -horizontalDist;
        }

        if (Random.value < 0.5f)
        {
            hitDir = -1;
        }

        else
            hitDir = 1;


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
        yield return new WaitForSeconds(5);

        if (!rend.isVisible)
        {
            LevelManager.levelManager.SFX.Unsubscribe(gameObject);
            Destroy(gameObject);
        }

        else StartCoroutine(Die());
    }
}
