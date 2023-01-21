using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollPlayerIdleState : MonoBehaviour
{
    //nothing goes here
    //
    //after some time, play a random idle animation

    private DollPlayerModelView modelView;

    [Header("HOW LONG PLAYER WAITS UNTIL IDLE ANIM")] [SerializeField]
    private float idleWait;

    private void OnEnable()
    {
        modelView = GetComponentInChildren<DollPlayerModelView>();

        modelView.OnIdle();

        StartCoroutine(IdleWait());

        IEnumerator IdleWait()
        {
            yield return new WaitForSeconds(idleWait);
        }
    }
}