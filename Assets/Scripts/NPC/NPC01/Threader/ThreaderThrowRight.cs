using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreaderThrowRight : MonoBehaviour
{
    public NPC01ModelView modelView;

    public void OnEnable()
    {
        modelView.OnChangeState(NPC01States.Throw);
    }
}