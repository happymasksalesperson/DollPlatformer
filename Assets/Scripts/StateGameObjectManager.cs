using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGameObjectManager : MonoBehaviour
{
    public GameObject currentState;

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
