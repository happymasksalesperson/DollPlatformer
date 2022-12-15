using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class StateManager : MonoBehaviour
    {
        public MonoBehaviour _startingState;
        public MonoBehaviour _currentState;
        
        public MonoBehaviour _patrolState;

        public MonoBehaviour _attackState;

        private void Start()
        {
            ChangeState(_startingState);
        }

        //Cam's change state stuff:
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

            _currentState = newState;
        }
        
        
    }