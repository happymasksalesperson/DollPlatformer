using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class StateManager : MonoBehaviour
    {
        public MonoBehaviour startingState;
        public MonoBehaviour currentState;

        public MonoBehaviour idleState;
        
        public MonoBehaviour patrolState;

        public MonoBehaviour attackState;

        public MonoBehaviour jumpState;

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
        
        
    }