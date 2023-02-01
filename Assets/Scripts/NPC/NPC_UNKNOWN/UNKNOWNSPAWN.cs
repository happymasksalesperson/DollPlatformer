using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class UNKNOWNSPAWN : MonoBehaviour
{
    //unknown spawns, moves toward spot, idles

    private StateManager stateManager;

    public float minValue;
    public float maxValue;
    private float randomWaitTime;

    public float hangTime;
    public float hangAmount;

    public float travelTime;
    public float moveSpeed;
    public float radius;
    private float randomAngle;

    private List<Vector3> spawnPoints = new List<Vector3>();

    private Vector3 randomCircleTransform;

    public Vector3 centrePoint;
    public Vector3 secondCentrePoint;

    private Vector3 circlePoint;

    private Vector3 originalPoint;

    //
    public SpriteRenderer spr;
    public float fadeSpeed;
    public float fadeDuration;

    private void OnEnable()
    {
        stateManager = GetComponent<StateManager>();

        spawnPoints.Add(circlePoint);
        spawnPoints.Add(centrePoint);

        int randomIndex = Random.Range(0, spawnPoints.Count);

        int coinFlip = Random.Range(0, 2);

        Vector3 revealPoint;
        
        if(coinFlip==0)
        {
            revealPoint = secondCentrePoint;
            spr.flipX = false;
        }

        else
        {
            revealPoint = centrePoint;
        }

        //refactorLater
        if (randomIndex == 1)
        {
            randomAngle = Random.Range(0.0f, 360.0f);
            if (randomAngle <= 180)
                spr.flipX = false;
            
            circlePoint = transform.position + new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0) * radius;
            transform.position = circlePoint;
            StartCoroutine(Move());
        }

        else if (randomIndex == 0)
        {
            transform.position = revealPoint;
            StartCoroutine(Reveal());
        }
    }

    private IEnumerator Move()
    {
        spr.color = new Color(1, 1, 1, 255);

        randomWaitTime = Random.Range(minValue, maxValue);
        yield return new WaitForSeconds(randomWaitTime);

        float startTime = Time.time;
        float journeyLength = Vector3.Distance(transform.position, centrePoint);
        while (transform.position != centrePoint)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, centrePoint, fracJourney);
            yield return null;
        } 
        
        stateManager.ChangeStateString("idle");
    }

    private IEnumerator Reveal()
    {
        spr.color = new Color(1, 1, 1, 0);

        randomWaitTime = Random.Range(minValue / 2, maxValue / 2);

        yield return new WaitForSeconds(randomWaitTime);

        float elapsedTime = 0f;

        Color originalColor = spr.color;
        Color targetColor = originalColor;
        targetColor.a = 255f;

        float fadeAmountPerSecond = 255f * fadeSpeed / fadeDuration;

        while (elapsedTime < fadeDuration)
        {
            spr.color = Color.Lerp(originalColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            targetColor.a = Mathf.Min(255f, originalColor.a + fadeAmountPerSecond * elapsedTime);
            yield return null;
        }

        spr.color = targetColor;
        
        stateManager.ChangeStateString("idle");
    }

    
}