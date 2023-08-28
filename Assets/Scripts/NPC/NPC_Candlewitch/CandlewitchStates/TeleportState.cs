using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Candlewitch
{
    //this state picks a random spot to teleport into, and then picks a random next move in DecideNextMove()
    public class TeleportState : CandlewitchStateBase
    {
        public Transform teleportSpot;

        public float yOffset;

        public float decisionTime;

        private CandlewitchAttackEnum attackEnum;

        public void OnEnable()
        {
            teleportSpot = brain.ChooseRandomTransform();

            Vector3 transformPosition = new Vector3(teleportSpot.position.x, teleportSpot.position.y - yOffset,
                teleportSpot.position.z);
            brain.transform.position = transformPosition;

            brain.CalculatePlayerPosition();

            modelView.OnFadeInFadeOut(true, brain.fadeTime);

            DecideNextMove();
        }

        private void DecideNextMove()
        {
            CandlewitchAttackEnum[] attackValues =
                (CandlewitchAttackEnum[])Enum.GetValues(typeof(CandlewitchAttackEnum));

            int randomIndex = Random.Range(0, attackValues.Length);

            CandlewitchAttackEnum randomAttack = attackValues[randomIndex];

            switch (randomAttack)
            {
                case CandlewitchAttackEnum.ShootFireball:
                    StartCoroutine(ChangeToDecidedState(brain.shootFireballState));
                    break;
                case CandlewitchAttackEnum.FloorFirePillar:
                    StartCoroutine(ChangeToDecidedState(brain.summonFirePillarState));
                    break;
                case CandlewitchAttackEnum.Teleport:
                    StartCoroutine(ChangeToDecidedState(brain.vanishState));
                    break;
            }
        }

        private IEnumerator ChangeToDecidedState(GameObject newState)
        {
            yield return new WaitForSeconds(brain.fadeTime);

            brain.stateManager.ChangeState(newState);
        }
    }
}