using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Oscar
{
    public class Neighbours : MonoBehaviour
    {
        public List<Transform> overallList = new List<Transform>();

        public List<GameObject> aiList = new List<GameObject>();
        public List<GameObject> playerList = new List<GameObject>();
        
        private void OnTriggerEnter(Collider other)
        {
            if (!overallList.Contains(other.transform))
            {
                overallList.Add(other.transform);
            }
            
            if (other.GetComponent<DynamicObject>() != null)
            {
                if (other.GetComponent<DynamicObject>().isAI)
                {
                    if (!aiList.Contains(other.gameObject))
                    {
                        aiList.Add(other.gameObject);
                    }
                }
                if (other.GetComponent<DynamicObject>().isPlayer)
                {
                    if (!playerList.Contains(other.gameObject))
                    {
                        playerList.Add(other.gameObject);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (overallList.Contains(other.transform))
            {
                overallList.Remove(other.transform);
            }
            
            if (other.GetComponent<DynamicObject>() != null)
            {
                if (aiList.Contains(other.gameObject))
                {
                    aiList.Remove(other.gameObject);
                }
                if (playerList.Contains(other.gameObject))
                {
                    playerList.Remove(other.gameObject);
                }
            }
        }
    }
}

