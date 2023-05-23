using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class NPC01MeleeAttack : MonoBehaviour
{
    public NPC01Brain brain;

    public NPCModelView modelView;

    public bool facingRight;

    public Rigidbody rb;

    [Header("How far jab forward motion goes")]
    public float pushForce;

    public float windupTime;
    public float attackTime;

    public void OnEnable()
    {
        rb = GetComponentInParent<Rigidbody>();
        brain = GetComponentInParent<NPC01Brain>();
        modelView = brain.modelView;

        facingRight = brain.facingRight;
        
        modelView.OnAttack01();

        StartCoroutine(JabWindup());
    }

    private IEnumerator JabWindup()
    {
        yield return new WaitForSeconds(windupTime);
        StartCoroutine(Jab());
    }

    private IEnumerator Jab()
    {
        if (facingRight)
            rb.AddForce(Vector3.right * pushForce, ForceMode.Impulse);
        else
            rb.AddForce(Vector3.left * pushForce, ForceMode.Impulse);

        yield return new WaitForSeconds(attackTime);

        Finish();
    }

    private void Finish()
    {
        rb.velocity = Vector3.zero;
        brain.meleeAttack = false;
        
        //change to agitated later
        brain.idle = true;
    }
}
