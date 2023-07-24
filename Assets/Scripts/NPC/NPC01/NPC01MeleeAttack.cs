using System;
using System.Collections;
using System.Collections.Generic;
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

    public bool jabbing=false;

    public int jabAttackPower;

    public Vector3 attackBox;

    public void OnEnable()
    {
        rb = GetComponentInParent<Rigidbody>();
        brain = GetComponentInParent<NPC01Brain>();
        modelView = brain.modelView;

        facingRight = brain.facingRight;
        brain.FacingRight(facingRight);

        modelView.OnAttack01();

        StartCoroutine(JabWindup());
    }

    private void Update()
    {
        if (jabbing)
        {
            Collider[] hits = new Collider[100];
            int hitsCount = Physics.OverlapBoxNonAlloc(transform.position, attackBox, hits, Quaternion.identity);

            for (int i = 0; i < hitsCount; i++)
            {
                Collider collider = hits[i];

                ITakeDamage damageable = collider.GetComponent<ITakeDamage>();
                if (damageable != null)
                {
                    Debug.Log("Hit " + collider.gameObject + " for " + jabAttackPower);

                    damageable.ChangeHP(jabAttackPower);
                }
            }
        }
    }

    private IEnumerator JabWindup()
        {
            yield return new WaitForSeconds(windupTime);
            StartCoroutine(Jab());
        }

        private IEnumerator Jab()
        {
            jabbing = true;
            
            if (facingRight)
                rb.AddForce(Vector3.right * pushForce, ForceMode.Impulse);
            else
                rb.AddForce(Vector3.left * pushForce, ForceMode.Impulse);

            yield return new WaitForSeconds(attackTime);

            Finish();
        }

        private void Finish()
        {
            brain.meleeAttack = false;

            jabbing = false;

            brain.agitated = true;
        }
    }