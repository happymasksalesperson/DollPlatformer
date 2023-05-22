using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponScriptableObject", menuName = "ScriptableObjects/WeaponScriptableObject")]
public class WeaponScriptableObject : ScriptableObject
{
    [Header("Required Components")] 
    
    [SerializeField]public bool equipped;

    [SerializeField] public Transform equippedTransform;

    [SerializeField]public bool facingRight;

    [SerializeField]public SphereCollider pickupSphereCollider;
    
    [SerializeField]public Rigidbody rigidbody;
    [SerializeField]public SpriteRenderer spriteRenderer;

    [SerializeField]public string description;

    public void ChangeEquip(bool newEquipped, Transform newEquipTransform)
    {
        equipped = newEquipped;
        if (equipped)
        {
            equippedTransform = newEquipTransform;
            pickupSphereCollider.enabled = false;
        }

        else
        {
            pickupSphereCollider.enabled = true;
        }
        
        ChangeNeedleState(newEquipTransform, newEquipped, !equipped, facingRight);
    }
    
    public void ChangeNeedleState(Transform transform, bool newEquipped, bool pickupState, bool newFacingRight)
    {
        equippedTransform = transform;

        equipped = newEquipped;
        
        pickupSphereCollider.enabled = pickupState;

        facingRight = newFacingRight;
    }
}