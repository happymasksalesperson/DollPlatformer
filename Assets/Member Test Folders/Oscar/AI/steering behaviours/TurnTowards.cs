using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class TurnTowards : MonoBehaviour
    {
        public Rigidbody rb;
        public GameObject target;
        private Transform targetTransform;
        private Vector3 targetPos;
        
        private RaycastHit hitInfo;
        private float distance = 10f;
        private int direction = 1;
        public float turnSpeed;

        private void Start()
        {
            targetTransform = target.transform;
        }

        void Update()
        {
            if (target)
            {
                targetPos = targetTransform.position;
            }

            Vector3 targetDir = targetPos - transform.position;

            float angle = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);
            
            rb.AddRelativeTorque(0,angle * turnSpeed,0);
        }
        
    }
}

