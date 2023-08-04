using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Oscar
{
    public class Avoid : MonoBehaviour
    {
        public Rigidbody rb;
        
        public Feeler leftFeel;
        public Feeler rightFeel;
        
        private float distance = 5f;
        private int direction = 1;
        private float spinTimer;

    
        public void FixedUpdate()
        {
            RaycastHit hitInfoLeft = leftFeel.GetHitInfo();
            if (hitInfoLeft.collider != null)
            {
                rb.AddRelativeTorque(Vector3.up, ForceMode.VelocityChange);
            }
            
            RaycastHit hitInfoRight = rightFeel.GetHitInfo();
            if (hitInfoRight.collider != null)
            {
                rb.AddRelativeTorque(Vector3.down, ForceMode.VelocityChange);
            }
        }
        
    }
}

