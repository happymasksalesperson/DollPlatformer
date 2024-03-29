using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
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
        modelView.OnChangeState(PlayerStates.Jump);
    }
}
