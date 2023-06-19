using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public GameObject currentObj;
    public PlayerStates currentState;

    public GameObject idle;
    public GameObject run;
    public GameObject standMeleeAttack01;
    public GameObject standRangeAttack01;
    public GameObject crouch;
    public GameObject slide;
    public GameObject takeDamage;
    public GameObject jump;

    public PlayerModelView modelView;

    [ShowInInspector]
    public Dictionary<PlayerStates, GameObject> stateDictionary;

    private void Awake()
    {
        modelView = GetComponentInChildren<PlayerModelView>();
        
        stateDictionary = new Dictionary<PlayerStates, GameObject>();
        stateDictionary.Add(PlayerStates.Idle, idle);
        stateDictionary.Add(PlayerStates.Run, run);
        stateDictionary.Add(PlayerStates.StandMeleeAttack01, standMeleeAttack01);
        stateDictionary.Add(PlayerStates.StandRangeAttack01, standRangeAttack01);
        stateDictionary.Add(PlayerStates.Crouch, crouch);
        stateDictionary.Add(PlayerStates.Slide, slide);
        stateDictionary.Add(PlayerStates.TakeDamage, takeDamage);
        stateDictionary.Add(PlayerStates.Jump, jump);
        
        ChangeState(PlayerStates.Idle);
    }

    [Button]
    public void ChangeState(PlayerStates key)
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
