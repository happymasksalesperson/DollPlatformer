using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class UNKNOWNIDLE : MonoBehaviour, ITalk 
{
    //refactor Dialogue later
    [SerializeField] private Dialogue dialogue;

    public float moveDist;

    public float moveTime;

    public float hangTime;

    private Vector3 origPos;

    private Vector3 circlePoint;

    private void OnEnable()
    {
        LevelEventManager.LevelEventInstance.OnCanTalk(true);
        origPos = transform.position;
        
        StartCoroutine(Bob());
    }

    private IEnumerator Bob()
    {
        while (true)
        {
            float randomAngle;
            randomAngle = Random.Range(0.0f, 360.0f);
            circlePoint = origPos + new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0) * moveDist;

            float elapsedTime = 0f;
            while (elapsedTime < moveTime)
            {
                transform.position = Vector3.Lerp(transform.position, circlePoint, elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(hangTime);

            elapsedTime = 0f;
            while (elapsedTime < moveTime)
            {
                transform.position = Vector3.Lerp(transform.position, origPos, elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(hangTime);
        }
    }

    public void Talk()
    {
        DialogueManager.DialogueInstance.ShowDialogue(dialogue);
    }

    private void OnDisable()
    {
        LevelEventManager.LevelEventInstance.OnCanTalk(false);
    }
}
