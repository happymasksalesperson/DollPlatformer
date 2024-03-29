using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC01SpawnState : MonoBehaviour
{
    public GameObject NPC01NeedlePrefab;
    public NPC01Needle needle;

    public NPC01Brain brain;

    public bool spawned=false;

    public GameObject NPC01;

    public WeaponScriptableObject myWep;

    public NPCType myType;
    
    private void OnEnable()
    {
        //initiate spawn animation
        
        brain = GetComponentInParent<NPC01Brain>();

        myType = brain.myType;

        GameObject myProjectile = Instantiate(NPC01NeedlePrefab, NPC01.transform) as GameObject;

        needle = myProjectile.GetComponent<NPC01Needle>();

        myProjectile.transform.position = NPC01.transform.position;
        myProjectile.transform.rotation = NPC01.transform.rotation;

        brain.EquipWeapon(needle.needle, needle.gameObject);
        
        needle.Equip(NPC01.transform);
        
        spawned = true;

        brain.spawned = true;

        //change idle / patrol 
        if(myType == NPCType.Idle)
        brain.idle = true;
        
        else if (myType == NPCType.Patrol)
            brain.patrolling = true;
    }
}
