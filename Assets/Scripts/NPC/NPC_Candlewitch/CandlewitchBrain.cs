using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static FireballBrain;

namespace Candlewitch
{
    public class CandlewitchBrain : MonoBehaviour
    {
        public bool testing;

        public enum CandlewitchStateEnum
        {
            StartFight,
            Vanish,
            Teleport,
            TakeDamage,
            ShootFireball,
            FirePillar,
            Death,

            Phase01,
            Phase02,
            Phase03
        }

        public CandlewitchStateEnum testState;

        public CandlewitchStateEnum currentPhase;

        public GameObjectStateManager stateManager;

        public List<Transform> teleportPositions = new List<Transform>();

        public Transform selectedTransform;

        public Transform playerTransform;

        public bool facingRight = false;

        public float fadeTime;

        public GameObject currentState;

        public GameObject startFightState;

        public GameObject shootFireballState;

        public GameObject summonFirePillarState;

        public GameObject teleportState;

        public GameObject vanishState;

        public GameObject deathState;

        public HealthModelView health;

        public Dictionary<CandlewitchStateEnum, GameObject> candlewitchStateDictionary =
            new Dictionary<CandlewitchStateEnum, GameObject>();

        public void OnEnable()
        {
            currentPhase = CandlewitchStateEnum.Phase01;

            if (candlewitchStateDictionary.Count == 0)
            {
                candlewitchStateDictionary.Add(CandlewitchStateEnum.StartFight, startFightState);
                candlewitchStateDictionary.Add(CandlewitchStateEnum.Vanish, vanishState);
                candlewitchStateDictionary.Add(CandlewitchStateEnum.Teleport, teleportState);
                candlewitchStateDictionary.Add(CandlewitchStateEnum.ShootFireball, shootFireballState);
                candlewitchStateDictionary.Add(CandlewitchStateEnum.FirePillar, summonFirePillarState);
                candlewitchStateDictionary.Add(CandlewitchStateEnum.Death, deathState);
            }
            
            health.ChangeHealth += UpdateHealth;

            if(!testing)
            ChangeState(CandlewitchStateEnum.StartFight);
        }

        public void OnDisable()
        {
            health.ChangeHealth -= UpdateHealth;
        }

        public void TestChangeState()
        {
            ChangeState(testState);
        }

        public void ChangeState(CandlewitchStateEnum newState)
        {
            if (candlewitchStateDictionary.TryGetValue(newState, out GameObject newStateObject))
            {
                stateManager.ChangeState(newStateObject);
            }
            else
            {
                Debug.LogWarning("State not found in dictionary: " + newState);
            }
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

        public void UpdateHealth(int healthVal)
        {
            //changes certain actions and values depending on remaining health

            if (healthVal <= 0)
            {
                stateManager.ChangeState(deathState);
                return;
            }

            if (healthVal <= 25)
            {
                currentPhase = CandlewitchStateEnum.Phase03;
                return;
            }

            if (healthVal <= 75)
            {
                currentPhase = CandlewitchStateEnum.Phase02;
                return;
            }

            currentPhase = CandlewitchStateEnum.Phase01;
        }
    }
}