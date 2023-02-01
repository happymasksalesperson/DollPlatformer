using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DollPlayerIdleState : MonoBehaviour
{
    //nothing goes here
    //
    //after some time, play a random idle animation

    private DollPlayerModelView modelView;

    private DollPlayerStats stats;

    private DollPlayerMovement playerMovement;

    private float sightRadius;

    [Header("HOW LONG PLAYER WAITS UNTIL IDLE ANIM")] [SerializeField]
    private float idleWait;

    private void OnEnable()
    {
        stats = GetComponent<DollPlayerStats>();

        sightRadius = stats.sightRadius;

        playerMovement = GetComponent<DollPlayerMovement>();

        modelView = GetComponentInChildren<DollPlayerModelView>();

        modelView.OnIdle();

        StartCoroutine(IdleWait());

        IEnumerator IdleWait()
        {
            yield return new WaitForSeconds(idleWait);
        }
    }

    private void FixedUpdate()
    {
        HandleTalkSight();
    }
    private void HandleTalkSight()
    {
            Collider[] hits = Physics.OverlapSphere(transform.position, sightRadius);
            GameObject talkerObj = null;
            for (int i = 0; i < hits.Length; i++)
            {
                ITalk talk = hits[i].GetComponent<ITalk>();
                if (talk != null)
                {
                    playerMovement.talkTarget = true;
                    talkerObj = hits[i].gameObject;
                    stats.talkerObj = talkerObj;
                    break;
                }
                else
                {
                    playerMovement.talkTarget = false;
                }
            }
            
    }

    private void OnDisable()
    {
    }
}