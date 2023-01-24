using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC05_MoveState : MonoBehaviour
{
    // on enable fires a single raycast
    // raycast hit is stored as a Vector3 destination
    // MovesTowards destination

    private StatsComponent stats;

    private float moveSpeed;
    private bool hasArrived = false;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float sightDistance;

    private RaycastHit hit;
    private Vector3 destination;

    private void OnEnable()
    {
        stats = GetComponent<StatsComponent>();
        moveSpeed = stats.MyMoveSpeed();
        sightDistance = stats.MySightDistance();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, sightDistance, groundLayer))
        {
            destination = hit.point;
            StartCoroutine(Move());
        }

    }

    private IEnumerator Move()
    {
        while (!hasArrived)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            if (transform.position == destination)
            {
                hasArrived = true;
            }

            yield return null;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.down * sightDistance);
    }
}