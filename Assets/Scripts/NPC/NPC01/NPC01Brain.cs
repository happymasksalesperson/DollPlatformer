using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class NPC01Brain : MonoBehaviour
{
    public NPCType myType;
    
    public GameObject currentState;
    public StateGameObjectManager stateManager;

    public GameObject spawnState;
    public GameObject agitatedState;
    public GameObject rangeAttackState;
    public GameObject meleeAttackState;
    public GameObject idleState;
    public GameObject jumpState;
    public GameObject jumpRangeAttackState;

    public GroundCheck groundCheck;
    public Gravity gravity;

    public Transform myTransform;
    public Transform targetTransform;

    public Vector3 lastPointOfInterest;

    public float pursueTimer;
    
    public enum allNPC01States
    {
        spawnState,
        idleState,
        patrolState,
        jumpState,
        jumpAttackState,
        agitatedState,
        meleeAttackState,
        rangeAttackState
    }

    public allNPC01States currentGameObjectState;
    
    public float checkStateTime = 0.3f;

    public bool seesPlayer;
    public bool heardSound;

    public GameObject weaponGO;
    public WeaponScriptableObject weaponSO;
    public NPC01Needle needle;
    public bool weaponEquipped;

    public NPCModelView modelView;

    public bool facingRight;

    public bool spawned = false;
    
    public Dictionary<allNPC01States, GameObject> stateDictionary = new Dictionary<allNPC01States, GameObject>();

    private void OnEnable()
    {
        stateDictionary[allNPC01States.spawnState] = spawnState;
        stateDictionary[allNPC01States.idleState] = idleState;
        stateDictionary[allNPC01States.meleeAttackState] = meleeAttackState;
        stateDictionary[allNPC01States.rangeAttackState] = rangeAttackState;
        stateDictionary[allNPC01States.agitatedState] = agitatedState;
        stateDictionary[allNPC01States.jumpState] = jumpState;

        myTransform = transform;
        
        modelView = GetComponentInChildren<NPCModelView>();

        stateManager = GetComponent<StateGameObjectManager>();

        currentGameObjectState = allNPC01States.spawnState;
        
        StartCoroutine(CheckStateCoroutine());
    }

    public void EquipWeapon(WeaponScriptableObject myWeaponSO, GameObject weaponGameObject)
    {
        weaponGO = weaponGameObject;
        weaponSO = myWeaponSO;
        weaponSO.ChangeEquip(true, transform);
        weaponEquipped = true;
    }

    public void UnequipWeapon()
    {
        weaponGO = null;
        weaponSO.ChangeEquip(false, null);
        weaponSO = null;
        weaponEquipped = false;
    }
    
    public bool rangeAttack;
    public bool meleeAttack;

    public bool idle;
    public bool agitated;
    public bool patrolling;
    public bool jumping;

    private IEnumerator CheckStateCoroutine()
    {
        while (true)
        {
            CheckState();
            yield return new WaitForSeconds(checkStateTime);
        }
    }
    
    public void CheckState()
    {
        if (spawned)
        {
            if (groundCheck.grounded)
            {
                gravity.enabled = false;
            }

            if (weaponEquipped)
            {
                if (rangeAttack)
                {
                    currentGameObjectState = allNPC01States.rangeAttackState;
                }
                
                else if (meleeAttack)
                {
                    currentGameObjectState = allNPC01States.meleeAttackState;
                }
            }

            if (jumping)
            {
                currentGameObjectState = allNPC01States.jumpState;
            }

            if (!rangeAttack && !meleeAttack && !patrolling &&!jumping)
            {

                if (agitated)
                    currentGameObjectState = allNPC01States.agitatedState;

                else if (idle)
                {
                    currentGameObjectState = allNPC01States.idleState;
                }
            }
        }

        SetNewCurrentGameObject(currentGameObjectState);
    }

    private void SetNewCurrentGameObject(allNPC01States enumValue)
    {
        GameObject result;
        if (stateDictionary.TryGetValue(enumValue, out result))
        {
            currentState = result;
        }
        stateManager.ChangeState(currentState);
    }

    public void FacingRight(bool value)
    {
        facingRight = value;
        modelView.OnFacingRight(value);

        if (weaponEquipped)
        {
            needle = weaponGO.GetComponent<NPC01Needle>();

            needle.FacingRight(facingRight);
        }
    }
}
