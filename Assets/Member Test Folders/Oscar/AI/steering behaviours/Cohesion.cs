using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class Cohesion : MonoBehaviour
    {
        public Rigidbody rb;
        public Neighbours neighbours;

        public float force;

        void FixedUpdate()
        {
            //Calculate the direction towards the position of the neighbors
            Vector3 targetDirection = CalculateMove(neighbours.overallList);

            rb.AddForce(targetDirection * force);
        }

        public Vector3 CalculateMove(List<Transform> FriendsPos)
        {
            if (FriendsPos.Count == 0)
                return Vector3.zero;

            Vector3 cohesionMove = Vector3.zero;

            //Average position of the neighbors
            foreach (Transform item in FriendsPos)
            {
                cohesionMove += item.position;
            }

            cohesionMove /= FriendsPos.Count;
            
            //Direction to push the guy
            cohesionMove -= transform.position;
            cohesionMove = Vector3.Normalize(cohesionMove);

            return cohesionMove;
        }
    }
}
