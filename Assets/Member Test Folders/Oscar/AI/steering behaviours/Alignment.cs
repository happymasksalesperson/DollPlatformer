using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oscar;

namespace Oscar
{
    public class Alignment : MonoBehaviour
    {
        public Rigidbody rb;
        public Neighbours neighbours;

        public float force;

        void FixedUpdate()
        {
            // Some are Torque, some are Force		
            Vector3 targetDirection = CalculateMove(neighbours.overallList);
		
            // Cross will take YOUR direction and the TARGET direction and turn it into a rotation force vector
            Vector3 cross = Vector3.Cross(transform.forward, targetDirection);

            rb.AddTorque(cross * force);
        }

        public Vector3 CalculateMove(List<Transform> FriendsPos)
        {
            if (FriendsPos.Count == 0)
                return Vector3.zero;

            Vector3 alignmentMove = Vector3.zero;

            // Average of all neighbours directions
            foreach (Transform item in FriendsPos)
            {
                alignmentMove += item.transform.forward;
            }

            alignmentMove /= FriendsPos.Count;

            return alignmentMove;
        }
       
    }
}
