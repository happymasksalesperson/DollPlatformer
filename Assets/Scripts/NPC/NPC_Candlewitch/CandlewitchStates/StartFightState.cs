using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Candlewitch
{
    public class StartFightState : CandlewitchStateBase
    {
        public float intimidateTime;

        public void OnEnable()
        {
            healthModel.Resurrect();

            modelView.OnFadeInFadeOut(true, brain.fadeTime);

            StartCoroutine(Intimidate());
        }

        public IEnumerator Intimidate()
        {
            yield return new WaitForSeconds(brain.fadeTime);

            yield return new WaitForSeconds(intimidateTime);

            brain.stateManager.ChangeState(brain.vanishState);
        }

    }
}