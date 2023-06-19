using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class NPC01Brain : DynamicObject
{
    public NPCType myType;

    public bool seesPlayer;
    public GameObject visionPivot;
    public OscarVision oscarVision;
    
    public GameObject currentState;
    public StateGameObjectManager stateManager;

    public GameObject spawnState;
    public GameObject agitatedState;
    public GameObject rangeAttackState;
    public GameObject meleeAttackState;
    public GameObject idleState;
    public GameObject jumpState;
    public GameObject jumpAttackState;
    public GameObject patrolState;

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
        agitatedState,
        meleeAttackState,
        rangeAttackState,
        jumpAttackState
    }

    public allNPC01States currentGameObjectState;
    
    public float checkStateTime = 0.3f;

    public bool heardSound;

    public GameObject weaponGO;
    public WeaponScriptableObject weaponSO;
    public NPC01Needle needle;
    public bool weaponEquipped;

    public NPCModelView modelView;

    public bool facingRight;

    public bool spawned = false;
    
    public Dictionary<allNPC01States, GameObject> stateDictionary = new Dictionary<allNPC01States, GameObject>();
    
    public bool rangeAttack;
    public bool meleeAttack;

    public bool idle;
    public bool agitated;
    public bool patrolling;
    public bool jumping;
    public bool jumpAttacking;

    private void OnEnable()
    {
        isPlayer = false;
        isAI = true;
        
        stateDictionary[allNPC01States.spawnState] = spawnState;
        stateDictionary[allNPC01States.idleState] = idleState;
        stateDictionary[allNPC01States.meleeAttackState] = meleeAttackState;
        stateDictionary[allNPC01States.rangeAttackState] = rangeAttackState;
        stateDictionary[allNPC01States.agitatedState] = agitatedState;
        stateDictionary[allNPC01States.jumpState] = jumpState;
        stateDictionary[allNPC01States.patrolState] = patrolState;
        stateDictionary[allNPC01States.jumpAttackState] = jumpAttackState;

        myTransform = transform;
        
        modelView = GetComponentInChildren<NPCModelView>();

        stateManager = GetComponent<StateGameObjectManager>();

        currentGameObjectState = allNPC01States.spawnState;
        
        StartCoroutine(CheckStateCoroutine());
    }

    private void Update()
    {
        seesPlayer = oscarVision.seesPlayer;

        if (!groundCheck.grounded)
            gravity.enabled = true;

        else
            gravity.enabled = false;
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
            if (seesPlayer)
            {
                agitated = true;
            }

            if (agitated)
            {
                if (jumpAttacking)
                {
                    currentGameObjectState = allNPC01States.jumpState;
                }
                else if (rangeAttack)
                {
                    currentGameObjectState = allNPC01States.rangeAttackState;
                }
                
                else if (meleeAttack)
                {
                    currentGameObjectState = allNPC01States.meleeAttackState;
                }
                else currentGameObjectState = allNPC01States.agitatedState;
            }

            if (patrolling)
            {
                currentGameObjectState = allNPC01States.patrolState;
            }

            if (jumping)
            {
                currentGameObjectState = allNPC01States.jumpState;
            }


            if (!rangeAttack && !meleeAttack && !patrolling &&!jumping && !patrolling)
            {
                if (idle)
                {
                    currentGameObjectState = allNPC01States.idleState;
                }
            }
        }

        SetNewCurrentState(currentGameObjectState);
    }

    public void SetNewCurrentState(allNPC01States enumValue)
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

        Vector3 eulerRotation = new Vector3(0f, facingRight ? 0f : 180f, 90f);
        visionPivot.transform.rotation = Quaternion.Euler(eulerRotation);
    }
}
