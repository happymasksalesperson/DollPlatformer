using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NPC02ConjoinedState : MonoBehaviour
{
    public NPCModelView modelView;
    
    public GameObject drumTop;
    public StateManager stateMan01;

    public GameObject drumMid;
    public StateManager stateMan02;

    public GameObject drumBot;
    public StateManager stateMan03;
    //public NPC02ConjoinedState conjoinedState;

    private void OnEnable()
    {
        stateMan01 = drumTop.GetComponent<StateManager>();
        
        stateMan02 = drumMid.GetComponent<StateManager>();
        
        stateMan03 = GetComponent<StateManager>();

        //conjoinedState = GetComponent<NPC02ConjoinedState>();
        
        modelView = stateMan03.GetComponentInChildren<NPCModelView>();

        modelView.Attack01 += ConjoinedAttack;
    }

    private void ConjoinedAttack()
    {
        StartCoroutine(ConjoinedAttack01());
        
        IEnumerator ConjoinedAttack01()
        {
            //stateMan03.ChangeStateString("attack");

            //should wait for attack time from stats?
            //yield return new WaitForSeconds(1);
            
            stateMan02.ChangeStateString("attack");

            yield return new WaitForSeconds(1);
            
            stateMan01.ChangeStateString("attack");
        }
    }

    private void OnDisable()
    {
        modelView.Attack01 -= ConjoinedAttack;
    }
}
