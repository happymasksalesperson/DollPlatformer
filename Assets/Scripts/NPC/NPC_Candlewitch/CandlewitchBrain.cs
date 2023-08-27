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
            Death
        }

        public CandlewitchStateEnum testState;

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

        [ReadOnly(true)]
        public int numberOfFireballs;

        public int startFightFireballs;
        public int midFightFireballs;
        public int endFightFireballs;

        public HealthModelView health;

        public Dictionary<CandlewitchStateEnum, GameObject> candlewitchStateDictionary =
            new Dictionary<CandlewitchStateEnum, GameObject>();



        public void OnEnable()
        {
            if (candlewitchStateDictionary.Count == 0)
            {
                candlewitchStateDictionary.Add(CandlewitchStateEnum.StartFight, startFightState);
                candlewitchStateDictionary.Add(CandlewitchStateEnum.Vanish, vanishState);
                candlewitchStateDictionary.Add(CandlewitchStateEnum.Teleport, teleportState);
                candlewitchStateDictionary.Add(CandlewitchStateEnum.ShootFireball, shootFireballState);
                candlewitchStateDictionary.Add(CandlewitchStateEnum.FirePillar, summonFirePillarState);
                candlewitchStateDictionary.Add(CandlewitchStateEnum.Death, deathState);
            }

            numberOfFireballs = startFightFireballs;
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
                numberOfFireballs = endFightFireballs;
                return;
            }

            if (healthVal <= 75)
            {
                numberOfFireballs = midFightFireballs;
                return;
            }

            numberOfFireballs = startFightFireballs;
        }
    }
}