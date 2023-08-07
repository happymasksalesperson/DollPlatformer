using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Candlewitch
{
    public class CandlewitchView : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;

        public CandlewitchModelView modelView;

        public float spriteFadeTime;

        public void OnEnable()
        {
            modelView.FadeInFadeOutEvent += StartFade;
            modelView.FlipFacingRightEvent += FlipFacingRight;
        }

        public void OnDisable()
        {
            modelView.FadeInFadeOutEvent -= StartFade;
            modelView.FlipFacingRightEvent -= FlipFacingRight;
        }

        public void StartFade(bool input, float time)
        {
            spriteFadeTime = time;
            StartCoroutine(SpriteFade(input));
        }

        public IEnumerator SpriteFade(bool fadeInput)
        {
            float elapsedTime = 0f;
            Color32 currentColor = spriteRenderer.color;
            byte startAlpha = fadeInput ? (byte)0 : (byte)255;
            byte targetAlpha = fadeInput ? (byte)255 : (byte)0;

            while (elapsedTime < spriteFadeTime)
            {
                elapsedTime += Time.deltaTime;
                byte alpha = (byte)(Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / spriteFadeTime));
                Color32 newColor = new Color32(currentColor.r, currentColor.g, currentColor.b, alpha);
                spriteRenderer.color = newColor;

                yield return null;
            }

            Color32 finalColor = new Color32(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
            spriteRenderer.color = finalColor;
        }

        public void FlipFacingRight(bool facingRight)
        {
            spriteRenderer.flipX = facingRight;
        }
    }
}