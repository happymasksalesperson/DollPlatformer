using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandRangeAttack01 : MonoBehaviour
{
    public PlayerStateManager stateManager;
    
    public PlayerModelView modelView;
    
    public void Start()
    {
        stateManager = GetComponentInParent<PlayerStateManager>();
    }
    
    public void OnEnable()
    {
        modelView = stateManager.modelView;
        modelView.OnChangeState(PlayerStates.StandRangeAttack01);
    }
}
