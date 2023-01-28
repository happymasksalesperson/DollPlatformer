using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NPC05_HangState : MonoBehaviour
{
    // on enable fires a single raycast
    // raycast hit is stored as a Vector3 destination
    // MovesTowards destination

    // else moves back to original pos

    private BoxCollider hurtboxCollider;
    private Vector3 hurtboxColliderSize;
    private float hurtboxHeight;

    [SerializeField] private float checkInterval;

    private StatsComponent stats;

    [SerializeField] private float moveSpeed;

    public bool playerSpotted = false;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float sightDistance;

    // Line Of Sight box collider
    [SerializeField] private Vector3 LOSExtents;


    public float extentsY;
    public Vector3 LOSPosition;

    public LayerMask playerLayer;

    private RaycastHit hit;

    public Vector3 spawnPoint;
    public Vector3 groundPoint;
    public Vector3 destination;

    public bool active;

    private BoxCollider boxCollider;
    public float size;

    private void OnEnable()
    {
        active = true;

        stats = GetComponent<StatsComponent>();
        stats.vulnerable = true;
        moveSpeed = stats.MyMoveSpeed();
        sightDistance = stats.MySightDistance(); 

        spawnPoint = transform.position;

        LOSExtents = new Vector3(sightDistance/2, sightDistance, 5);

        boxCollider = GetComponent<BoxCollider>();
        size = boxCollider.size.y;

        RaycastHit hit;
        if (Physics.Raycast(spawnPoint, Vector3.down, out hit, sightDistance*2, groundLayer))
        {
            groundPoint = new Vector3(hit.point.x, hit.point.y + size * 40, hit.point.z);
        }

        StartCoroutine(CheckForPlayer());
        StartCoroutine(Move());
    }

    // // // //
    // SCANS FOR PLAYER
    // pops a overlapboxnonalloc every checkInterval
    // if IPlayer detected, playerSpotted=true
    private IEnumerator CheckForPlayer()
    {
        while (active)
        {
            yield return new WaitForSeconds(checkInterval);
            
            LOSPosition = transform.position + new Vector3(0, -(sightDistance / 2), 0);

            //Debug.Log("scanning");

            Collider[] hits = new Collider[10];
            int numHits =
                Physics.OverlapBoxNonAlloc(LOSPosition, LOSExtents/2, hits, Quaternion.identity, playerLayer);

            bool foundPlayer = false;
            for (int i = 0; i < numHits; i++)
            {
                IPlayer player = hits[i].GetComponent<IPlayer>();
                if (player != null)
                {
                    foundPlayer = true;
                    break;
                }
            }

            if (foundPlayer)
            {
                //Debug.Log("SPOTTED!");
                playerSpotted = true;
            }
            else
            {
                //Debug.Log("nothing here");
                playerSpotted = false;
            }
        }
    }

    private IEnumerator Move()
    {
        bool isMoving;
        if (transform.position != destination)
            isMoving = true;

        else
        {
            isMoving = false;
        }

        while (isMoving)
        {
            if (playerSpotted)
                destination = groundPoint;
            else
                destination = spawnPoint;

            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDisable()
    {
        StopCoroutine(CheckForPlayer());
        StopCoroutine(Move());
        active = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(LOSPosition, LOSExtents);
    }
}