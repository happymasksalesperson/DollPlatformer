using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class MovingEyes : MonoBehaviour
    {
        public float moveSpeed;
        public GameObject target;
        private Vector3 targetPos;
        public Neighbours neighbours;
        public float turnSpeed;
        //public Rigidbody rb;

        void Update()
        {
            if (neighbours.player.Count <= 1)
            {
                target = neighbours.player[0];
            }
            if (target)
            {
                targetPos = target.transform.position;
            }
            
            //transform movement
            float angleX = Vector3.SignedAngle(transform.forward, targetPos - transform.position,Vector3.right);
            float angleY = Vector3.SignedAngle(transform.forward, targetPos - transform.position,Vector3.up);
            
            transform.Rotate(angleX * turnSpeed,angleY * turnSpeed,0,Space.Self);

            //RigidBody
            // rb.AddRelativeTorque(0, Vector3.SignedAngle(transform.forward, targetPos.normalized - transform.position.normalized, Vector3.up) * turnSpeed,0);
        }
    }
}