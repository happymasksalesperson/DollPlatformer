using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Oscar
{
    public class Wonder : MonoBehaviour
    {
        public Rigidbody rb;

        private float perlin;
        private float scale = 10f;
        private float zoomX = 1.15f;
        private float zoomZ = 1.15f;
        
        
        private float randomness;

        private void Start()
        {
            zoomX = Random.Range(-0.5f, 0.5f);
            zoomZ = Random.Range(-0.5f, 0.5f);
        }

        void FixedUpdate()
        {
            float x = zoomX + Time.time;// * scale;
            float z = zoomZ + Time.time;// * scale;
            
            perlin = Mathf.PerlinNoise(x,z)*2-1;
            
            rb.AddRelativeTorque(0,perlin,0);
        }
        
    }
}

