using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Oscar
{
    public class Move : MonoBehaviour
    {
        public float speed;
        public float speedIncreaseMultiplyer;
        public Rigidbody rb;
        void Update()
        {
            float decidedSpeed = speed * speedIncreaseMultiplyer;
            rb.AddRelativeForce(Vector3.forward * decidedSpeed,ForceMode.Acceleration);
        }
    }
}

