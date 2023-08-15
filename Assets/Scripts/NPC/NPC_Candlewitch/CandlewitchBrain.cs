using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Candlewitch
{
    public class CandlewitchBrain : MonoBehaviour
    {
        public GameObjectStateManager stateManager;

        public List<Transform> teleportPositions = new List<Transform>();

        public Transform selectedTransform;

        public Transform playerTransform;

        private bool facingRight = false;

        public float fadeTime;  
        
        public GameObject startFightState;

        public GameObject shootFireballState;

        public GameObject summonFirePillarState;

        public GameObject teleportState;

        public GameObject vanishState;

        public GameObject deathState;

        public void OnEnable()
        {
            stateManager.ChangeState(startFightState);
        }

        //flips facing right depending on the playerTransform position in relation to myself
        //only flips facing right if not already
        public void CalculatePlayerPosition()
        {
            float targetX = playerTransform.position.x;
            float myX = transform.position.x;

            if ((targetX > myX && !facingRight) || (targetX < myX && facingRight))
            {
                facingRight = !facingRight;
            }

            if (facingRight)
                transform.rotation = new Quaternion(0, 180, 0, 0);

            else
                transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        public Transform ChooseRandomTransform()
        {
            int randomIndex = Random.Range(0, teleportPositions.Count);
            selectedTransform = teleportPositions[randomIndex];
            return selectedTransform;
        }
    }
}