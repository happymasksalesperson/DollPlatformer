using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class Separation : MonoBehaviour
    {
        public Rigidbody rb;
        public Neighbours neighbours;

        public float force;

        void FixedUpdate()
        {
            //Calculate the separation movement
            Vector3 targetDirection = CalculateMove(neighbours.overallList);

            rb.AddForce(targetDirection * force);
        }

        public Vector3 CalculateMove(List<Transform> FriendsPos)
        {
            if (FriendsPos.Count == 0)
                return Vector3.zero;

            Vector3 separationMove = Vector3.zero;

            //Direction AWAY from EACH neighbour and add to the final direction
            foreach (Transform item in FriendsPos)
            {
                Vector3 awayDirection = transform.position - item.position;
                separationMove += awayDirection.normalized;
            }

            //Direction to push the guy
            separationMove /= FriendsPos.Count;

            return separationMove.normalized;
        }
    }
}
