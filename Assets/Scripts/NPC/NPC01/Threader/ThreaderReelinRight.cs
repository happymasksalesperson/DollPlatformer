using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreaderReelinRight : MonoBehaviour
{
    public NPC01ModelView modelView;

    public void OnEnable()
    {
        modelView.OnChangeState(NPC01States.ReelinRight);
    }
}
