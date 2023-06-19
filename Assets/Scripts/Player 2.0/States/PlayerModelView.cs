using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerModelView : MonoBehaviour
{
    public event Action<PlayerStates> ChangeStateEvent;

    public void OnChangeState(PlayerStates newState)
    {
        ChangeStateEvent?.Invoke(newState);
    }
    
    public event Action<bool> FacingRight;

    public void OnFacingRight(bool facingRight)
    {
        FacingRight?.Invoke(facingRight);
    }
}
