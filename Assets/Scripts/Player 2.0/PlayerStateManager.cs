using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public GameObject currentObj;
    public PlayerStates currentState;

    public GameObject idle;
    public GameObject run;
    
    public GameObject jump;
    public GameObject fall;
    
    public GameObject standMeleeAttack01;
    public GameObject standRangeAttack01;
    
    public GameObject crouch;
    public GameObject slide;
    
    public GameObject takeDamage;

    public GameObject death;

    public PlayerModelView modelView;

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
        stateDictionary.Add(PlayerStates.Fall, fall);
        stateDictionary.Add(PlayerStates.Death, death);
        
        ChangeState(PlayerStates.Idle);
    }

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
            
            modelView.OnChangeState(key);
        }
        else
        {
            Debug.LogError("State not found in the stateDictionary.");
        }
    }

    public void DeclareMoment(PlayerMoments newMoment)
    {
        modelView.OnDeclarePlayerEvent(newMoment);
    }
}
