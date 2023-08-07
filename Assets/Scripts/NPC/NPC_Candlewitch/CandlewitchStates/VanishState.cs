using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Candlewitch
{
    public class VanishState : CandlewitchStateBase
    {

        public void OnEnable()
        {
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            modelView.OnFadeInFadeOut(false, brain.fadeTime);

            yield return new WaitForSeconds(brain.fadeTime);

            brain.stateManager.ChangeState(brain.teleportState);
        }
    }
}