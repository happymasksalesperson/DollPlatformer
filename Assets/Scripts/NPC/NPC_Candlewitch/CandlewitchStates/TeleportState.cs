using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Candlewitch
{
    public class TeleportState : CandlewitchStateBase
    {
        public Transform teleportSpot;

        public float yOffset;

        public void OnEnable()
        {
            teleportSpot = brain.ChooseRandomTransform();

            Vector3 transformPosition = new Vector3(teleportSpot.position.x, teleportSpot.position.y - yOffset,
                teleportSpot.position.z);
            brain.transform.position = transformPosition;

            modelView.OnFlipFacingRight(brain.CalculatePlayerPosition());

            modelView.OnFadeInFadeOut(true, brain.fadeTime);

            StartCoroutine(TeleportIn());
        }

        private IEnumerator TeleportIn()
        {
            yield return new WaitForSeconds(brain.fadeTime);

            brain.stateManager.ChangeState(brain.vanishState);
        }


    }
}