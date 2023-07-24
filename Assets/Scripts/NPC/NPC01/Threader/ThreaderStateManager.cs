using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThreaderStateManager : MonoBehaviour
{
    public GameObject currentObj;
    public NPC01States currentState;

    public GameObject idle;
    public GameObject reelin_left;
    public GameObject reelin_right;
    public GameObject melee_left;
    public GameObject melee_right;
    public GameObject throw_right;

    public NPC01ModelView modelView;
    
    public Dictionary<NPC01States, GameObject> stateDictionary;

    private void Awake()
    {
        modelView = GetComponentInChildren<NPC01ModelView>();
        
        stateDictionary = new Dictionary<NPC01States, GameObject>();
        stateDictionary.Add(NPC01States.Idle, idle);
        stateDictionary.Add(NPC01States.ReelinLeft, reelin_left);
        stateDictionary.Add(NPC01States.ReelinRight, reelin_right);
        stateDictionary.Add(NPC01States.MeleeAttackLeft, melee_left);
        stateDictionary.Add(NPC01States.MeleeAttackRight, melee_right);
        stateDictionary.Add(NPC01States.Throw, throw_right);

        ChangeState(NPC01States.Idle);
    }

    public void ChangeState(NPC01States key)
    {
        if (key == currentState)
            return;

        GameObject newState;
        if (stateDictionary.TryGetValue(key, out newState))
        {
            currentObj.SetActive(false);

            newState.SetActive(true);
            currentState = key;
            currentObj = newState;
        }
        else
        {
            Debug.LogError("State not found in the stateDictionary.");
        }
    }
}
