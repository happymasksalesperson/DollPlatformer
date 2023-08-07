using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Candlewitch
{
    public class CandlewitchModelView : MonoBehaviour
    {
        public event Action<bool, float> FadeInFadeOutEvent;

        public void OnFadeInFadeOut(bool input, float time)
        {
            FadeInFadeOutEvent?.Invoke(input, time);
        }

        public event Action<bool> FlipFacingRightEvent;

        public void OnFlipFacingRight(bool facingRight)
        {
            FlipFacingRightEvent?.Invoke(facingRight);
        }
    }
}