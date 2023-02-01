using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UNKOWNDESPAWN : MonoBehaviour
{
    public float fadeDuration;

    public float fadeSpeed;
    
    private SpriteRenderer spr;

    private void OnEnable()
    {
        spr = GetComponent<SpriteRenderer>();
        
        StartCoroutine(Reveal());
    }

    private IEnumerator Reveal()
    {
        spr.color = new Color(1, 1, 1, 255);

        float elapsedTime = 0f;

        Color originalColor = spr.color;
        Color targetColor = originalColor;
        targetColor.a = 0f;

        float fadeAmountPerSecond = 1f * fadeSpeed / fadeDuration;

        while (elapsedTime < fadeDuration)
        {
            spr.color = Color.Lerp(originalColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            targetColor.a = Mathf.Min(0f, originalColor.a + fadeAmountPerSecond * elapsedTime);
            yield return null;
        }

        spr.color = targetColor;
    }
}
