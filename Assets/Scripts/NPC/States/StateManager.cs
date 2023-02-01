using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public MonoBehaviour startingState;
    public MonoBehaviour currentState;

    public MonoBehaviour crouchState;

    public MonoBehaviour idleState;

    public MonoBehaviour talkState;

    public MonoBehaviour patrolState;

    public MonoBehaviour attack01State;

    public MonoBehaviour crouchAttack01State;

    public MonoBehaviour jumpState;

    public MonoBehaviour takeDamageState;

    public MonoBehaviour deathState;

    private string stateString;

    private void Start()
    {
        ChangeState(startingState);
    }

    //Cam's change state stuff:
    public void ChangeState(MonoBehaviour newState)
    {
        if (newState == currentState)
        {
            return;
        }

        if (currentState != null)
        {
            currentState.enabled = false;
        }

        newState.enabled = true;

        currentState = newState;
    }

    //use for Editor + modelView
    public void ChangeStateString(string state)
    {
        switch (state)
        {
            case ("crouch"):
                ChangeState(crouchState);
                break;

            case ("idle"):
                ChangeState(idleState);
                break;
            
            case ("talk"):
                ChangeState (talkState);
                break;

            case ("patrol"):
                ChangeState(patrolState);
                break;

            case ("attack01"):
                ChangeState(attack01State);
                break;
            
            case ("crouchAttack01"):
                ChangeState(crouchAttack01State);
                break;

            case ("takeDamage"):
                ChangeState(takeDamageState);
                break;

            case ("jump"):
                ChangeState(jumpState);
                break;

            case ("death"):
                ChangeState(deathState);
                break;
        }
    }
}