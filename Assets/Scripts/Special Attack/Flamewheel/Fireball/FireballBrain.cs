using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBrain : MonoBehaviour
{
    public bool facingRight;

    public Transform targetTransform;

    public Transform shootPointTransform;

    public enum FireballStateEnum
    {
        Summon,
        Projectile,
        Linger,
        Death
    }

    public FireballStateEnum state;

    public FireballStateEnum testState = FireballStateEnum.Projectile;

    public GameObjectStateManager stateManager;

    public GameObject summonState;

    public GameObject projectileState;

    public GameObject lingerState;

    public GameObject deathState;

    public event Action<FireballStateEnum> DeclareStateEvent;

    public event Action<bool> DeclareFacingRight;

    public Dictionary<FireballStateEnum, GameObject> fireballStateDictionary =
        new Dictionary<FireballStateEnum, GameObject>();

    public void OnEnable()
    {
        if (fireballStateDictionary.Count == 0)
        {
            fireballStateDictionary.Add(FireballStateEnum.Summon, summonState);
            fireballStateDictionary.Add(FireballStateEnum.Projectile, projectileState);
            fireballStateDictionary.Add(FireballStateEnum.Linger, lingerState);
            fireballStateDictionary.Add(FireballStateEnum.Death, deathState);

            ChangeState(FireballStateEnum.Summon);
        }
    }

    public void FacingRight(bool input)
    {
        facingRight = input;
        DeclareFacingRight?.Invoke(input);
    }

    public void TestChangeState()
    {
        ChangeState(testState);
    }

    public void ChangeState(FireballStateEnum newState)
    {
        if (fireballStateDictionary.TryGetValue(newState, out GameObject newStateObject))
        {
            stateManager.ChangeState(newStateObject);
            state = newState;
            DeclareStateEvent?.Invoke(newState);
        }
        else
        {
            Debug.LogWarning("State not found in dictionary: " + newState);
        }
    }
}