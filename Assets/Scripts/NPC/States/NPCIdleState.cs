using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleState : MonoBehaviour
{
    //play idle anims

    private NPCModelView modelView;

    private void OnEnable()
    {
        modelView = GetComponentInChildren<NPCModelView>();
        modelView.OnIdle();
    }
}
