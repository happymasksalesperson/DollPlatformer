using System;
using System.Collections;
using System.Collections.Generic;
using NPC.NPC01.Needle;
using UnityEngine;

public class NPC01Needle : MonoBehaviour
{
    public WeaponScriptableObject needle;

    public NPCModelView modelView;

    public bool facingRight = false;
    private bool prevFacing;

    public bool equipped;

    public Transform equippedTransform;

    public BoxCollider damageBox;
    
    public SphereCollider pickupCollider;

    public GroundCheck groundCheck;

    public bool grounded;

    public Gravity gravity;

    public Rigidbody rb;
    
    public int needleAttackPower;

    public bool active;
    
    public LayerMask wallLayer;

    public NeedleBounce bounce;
    public void OnEnable()
    {
        modelView = GetComponentInChildren<NPCModelView>();

        bounce = GetComponent<NeedleBounce>();

        rb = GetComponent<Rigidbody>();
        rb.angularVelocity=Vector3.zero;
        
        needle.pickupSphereCollider = pickupCollider;
        
        Physics.IgnoreLayerCollision(11,11);
        Physics.IgnoreLayerCollision(11,10);


        ChangeSpriteX();
        
        if(!equipped)
            Unequip(gameObject);

        StartCoroutine(CheckState());
    }

    public void ChangeSpriteX()
    {
        if (modelView != null)
            modelView.OnFacingRight(facingRight);
    }

    public void FixedUpdate()
    {
        if (equipped)
        {
            transform.position = equippedTransform.position;
            transform.rotation = equippedTransform.rotation;
            groundCheck.enabled = false;
        }
        else
        {
            groundCheck.enabled = true;
        }
    }

    public IEnumerator CheckState()
    {
        while (true)
        {
            prevFacing = facingRight;
            if (facingRight != prevFacing)
            {
                modelView.OnFacingRight(facingRight);
                prevFacing = facingRight;
            }

            grounded = groundCheck.grounded;
            if (!equipped && !grounded && active)
            {
                gravity.enabled = true;
            }
            else if (grounded)
            {
                CeaseActive();
            }

            if (active)
                ActiveHitbox();


            yield return new WaitForSeconds(.3f);
        }
    }

    public void ActiveHitbox()
    {
        pickupCollider.enabled = false;
        damageBox.enabled = true;
            
        gravity.enabled = true;

        Collider[] hits = new Collider[100];
        int hitsCount = Physics.OverlapBoxNonAlloc(transform.position, damageBox.size, hits, Quaternion.identity);
    
        for (int i = 0; i < hitsCount; i++)
        {
            Collider collider = hits[i];

            ITakeDamage damageable = collider.GetComponent<ITakeDamage>();
            if (damageable != null)
            {
                Debug.Log("Hit "+collider.gameObject+" for " + needleAttackPower);

                damageable.ChangeHP(needleAttackPower);
                
                bounce.Bounce();
            }

            else if (wallLayer == (wallLayer | (1 << hits[i].gameObject.layer)))
                {
                    Debug.Log("Hit wall" + hitsCount);

                    CeaseActive(); 
                }
        }   
    }

    private void CeaseActive()
    {
        if (active)
        {
            Debug.Log("ceased");
            active = false;
            pickupCollider.enabled = true;
            gravity.enabled = false;

            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void Equip(Transform equipTransform)
    {
        equippedTransform = equipTransform;
        equipped = true;

        needle.ChangeEquip(true, equipTransform);

        Debug.Log(equipTransform + " equipped " + transform);

        needle.ChangeNeedleState(equipTransform, true, false, facingRight);
    }

    public void FacingRight(bool value)
    {
        facingRight = value;
        modelView.OnFacingRight(value);
    }

    public void Unequip(GameObject weapon)
    {
        equipped = false;
        equippedTransform = null;

        weapon.transform.SetParent(null);

        needle.ChangeEquip(false, null);

        Debug.Log("unequip");
    }
}