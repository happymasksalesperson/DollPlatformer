using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class Feeler : MonoBehaviour
    {
        public Rigidbody rb;
        public RaycastHit hitInfo;
        public float distance = 5f;
        public float feelerAmount;
        
        private void FixedUpdate()
        {
            for (int i = 0; i < feelerAmount; i++)
            {
                Vector3 direction = Quaternion.Euler(0f, i, 0f) * transform.forward;
                if (Physics.Raycast(rb.transform.localPosition, direction, out hitInfo, distance, 255, QueryTriggerInteraction.UseGlobal))
                {
                    
                }
            }
            
        }
        public RaycastHit GetHitInfo()
        {
            return hitInfo;
        }
    }
}

