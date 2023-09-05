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

        public List<Vector3> spawnedPositions = new List<Vector3>();


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
                Vector3 spawnPosition = GetValidSpawnPosition();

                GameObject spawnedObject = pool.GetPooledObject();
                spawnedObject.transform.position = spawnPosition;

                spawnedPositions.Add(spawnPosition);
            }

            StartCoroutine(Vulnerable());
        }

        Vector3 GetValidSpawnPosition()
        {
            Vector3 spawnPosition;
            bool isValidPosition = false;

            do
            {
                float randomX = Random.Range(leftPoint.position.x + distanceApart,
                    rightPoint.position.x - distanceApart);
                spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);

                // Check if the new position is far enough from existing positions
                isValidPosition = IsPositionValid(spawnPosition);
            }
            while (!isValidPosition);

            return spawnPosition;
        }

        bool IsPositionValid(Vector3 position)
        {
            foreach (Vector3 existingPosition in spawnedPositions)
            {
                // Check the distance between the new position and existing positions
                if (Vector3.Distance(position, existingPosition) < distanceApart)
                {
                    return false; // Position is too close to an existing one
                }
            }
            return true; // Position is valid
        }

        private IEnumerator Vulnerable()
        {
            yield return new WaitForSeconds(vulnerableTime);

            brain.ChangeState(CandlewitchBrain.CandlewitchStateEnum.Vanish);
        }
    }
}