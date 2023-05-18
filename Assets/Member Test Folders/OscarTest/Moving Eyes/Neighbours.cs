using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Oscar
{
    public class Neighbours : MonoBehaviour
    {
        public List<GameObject> player;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IPlayer>() != null)
            {
                if (!player.Contains(other.gameObject))
                {
                    player.Add(other.gameObject);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (player.Contains(other.gameObject))
            {
                player.Remove(other.gameObject);
            }
        }
    }
}
