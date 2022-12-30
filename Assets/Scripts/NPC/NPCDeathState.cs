using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDeathState : MonoBehaviour
{
    private Rigidbody rb;

    private BoxCollider box;

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
        rb = GetComponent<Rigidbody>();

        box = GetComponent<BoxCollider>();

        rend = GetComponentInChildren<SpriteRenderer>();

        gravity = GetComponent<Gravity>();
        
        box.enabled = false;
        
        rb.AddForce(new Vector3(horizontalDist, verticalDist, 0), ForceMode.Impulse);
        
        //transform.hitDir (see above)
        //can't seem to make it spin very fast?
        rb.AddTorque(transform.forward * torque * hitDir);

        gravity.enabled = true;
        StartCoroutine(Die());
    }

    //destroys gameobject once renderer is out of sight
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(1);

        if (!rend.isVisible)
            Destroy(gameObject);

        else StartCoroutine(Die());
    }
}
