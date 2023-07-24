using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC01ModelView : MonoBehaviour
{
    public event Action<NPC01States> ChangeStateEvent;

    public void OnChangeState(NPC01States newState)
    {
        ChangeStateEvent?.Invoke(newState);
    }
    
    public event Action<bool> FacingRight;

    public void OnFacingRight(bool facingRight)
    {
        FacingRight?.Invoke(facingRight);
    }
}