using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollPlayerTalkState : MonoBehaviour
{
    private DollPlayerStats playerStats;

    private DollPlayerModelView modelView;
    
    private DollPlayerMovement playerMovement;

    private Vector3 talkerPos;

    private bool facingRight;
    private void OnEnable()
    {
        playerStats = GetComponent<DollPlayerStats>();
        talkerPos = playerStats.talkerObj.transform.position;

        playerMovement = GetComponent<DollPlayerMovement>();
        playerMovement.talking = true;

        modelView = GetComponentInChildren<DollPlayerModelView>();
        modelView.OnIdle(); 

        FaceTowards(talkerPos);
    }
    
    private void FaceTowards(Vector3 talkerPos)
    {
        if (talkerPos != Vector3.zero)
        {
            float distance = talkerPos.x - transform.position.x;
            facingRight = distance <= 0;
            modelView.OnFacingRight(facingRight);
        }
    }

    private void OnDisable()
    {
        playerMovement.talking = false;
    }
}
