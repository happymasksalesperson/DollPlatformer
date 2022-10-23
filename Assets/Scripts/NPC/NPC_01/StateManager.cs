using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace NPC01
{

    public class StateManager : MonoBehaviour
    {
        public MonoBehaviour _startingState;
        public MonoBehaviour _currentState;

        public MonoBehaviour _attackState;

        private void Start()
        {
            ChangeState(_startingState);
            Debug.Log(_currentState);
        }

        public void ChangeState(MonoBehaviour newState)
        {
            if (newState == _currentState)
            {
                return;
            }

            if (_currentState != null)
            {
                _currentState.enabled = false;
            }

            newState.enabled = true;

            // New state swap over to incoming state
            _currentState = newState;
        }
        
        
    }
}