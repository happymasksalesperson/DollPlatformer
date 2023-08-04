using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class Spawner : MonoBehaviour
    {
        public GameObject theGuy;
        private Vector3 spawnLoc;
        public float spawnInterval;
        
        public bool spawnGuys;
        private void Start()
        {
            spawnLoc = new Vector3(transform.position.x, transform.position.y,transform.position.z);
        }

        private void Update()
        {
            if (spawnGuys)
            {
                StartCoroutine(SpawnCoroutine());
            }
        }

        IEnumerator SpawnCoroutine()
        {
            Quaternion rotation = Quaternion.Euler(0f, Time.deltaTime, 0f);
            Instantiate(theGuy, spawnLoc, rotation);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

}
