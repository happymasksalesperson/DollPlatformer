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
        
        //LevelEventManager.LevelEventInstance.OnCanTalk(false);
        
        spr = GetComponentInChildren<SpriteRenderer>();

        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        spr.color = new Color(1, 1, 1, 255);

        float elapsedTime = 0f;

        Color originalColor = spr.color;
        Color targetColor = originalColor;
        targetColor.a = 0f;

        float fadeAmountPerSecond = 255f * fadeSpeed / fadeDuration;

        while (elapsedTime < fadeDuration)
        {
            spr.color = Color.Lerp(originalColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            targetColor.a = Mathf.Max(0f, originalColor.a - fadeAmountPerSecond * elapsedTime);
            yield return null;
        }

        spr.color = targetColor;
        Destroy(gameObject);
    }
}