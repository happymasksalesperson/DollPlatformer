using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreaderMeleeRight : MonoBehaviour
{
    public NPC01ModelView modelView;

    public void OnEnable()
    {
        modelView.OnChangeState(NPC01States.MeleeAttackRight);
    }
}