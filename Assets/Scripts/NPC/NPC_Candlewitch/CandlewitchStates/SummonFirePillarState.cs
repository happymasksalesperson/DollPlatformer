using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Candlewitch
{
    public class SummonFirePillarState : CandlewitchStateBase
    {
        public ObjectPool pool;

        public int Phase01PillarCount;
        public int Phase02PillarCount;
        public int Phase03PillarCount;
        private int numberOfPillars;

        public GameObject flamePillar;
        public Transform leftPoint;
        public Transform rightPoint;

        [Header("Distance Flame Pillars spawn apart from each and each other")]
        public float distanceApart;

        public float vulnerableTime;

        private void OnEnable()
        {
            if (brain.currentPhase == CandlewitchBrain.CandlewitchStateEnum.Phase01)
                numberOfPillars = Phase01PillarCount;

            else if (brain.currentPhase == CandlewitchBrain.CandlewitchStateEnum.Phase02)
                numberOfPillars = Phase02PillarCount;

            else
                numberOfPillars = Phase03PillarCount;

            pool.ClearPool();

            pool.SetPoolSizeAndCreate(flamePillar, numberOfPillars);
            SpawnObjects();
        }

        void SpawnObjects()
        {
            for (int i = 0; i < numberOfPillars; i++)
            {
                float randomX = Random.Range(leftPoint.position.x + distanceApart,
                    rightPoint.position.x - distanceApart);
                Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);

                GameObject spawnedObject = pool.GetPooledObject();
                spawnedObject.transform.position = spawnPosition;
            }

            StartCoroutine(Vulnerable());
        }

        private IEnumerator Vulnerable()
        {
            yield return new WaitForSeconds(vulnerableTime);

            brain.ChangeState(CandlewitchBrain.CandlewitchStateEnum.Vanish);
        }
    }
}