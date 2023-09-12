using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class StateMachine : MonoBehaviour
    {
        /// <StateMachineRules>
        /// 
        /// You could have a list of states to choose from however having that in the
        /// state manager script makes the state manager hard coded so all states
        /// should be listed and changed in a different script calling this script
        /// to change it.
        /// 
        /// It also uses GameObjects now instead of strings
        /// 
        /// </StateMachineRules>
        
        public GameObject currentState;
        public GameObject startingState;
        
        private void Start()
        {
            ChangeState(startingState);
        }

        public void ChangeState(GameObject newState)
        {
            if (newState == currentState)
            {
                return;
            }

            if (currentState != null)
            {
                currentState.SetActive(false);
            }

            newState.SetActive(true);

            currentState = newState;
        }
    }
}