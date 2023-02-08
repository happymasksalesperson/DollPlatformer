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

    private StateManager stateManager;

    public float moveDist;

    public float moveTime;

    public float hangTime;

    private Vector3 origPos;

    private Vector3 circlePoint;

    [Header("BODY PARTS")] public GameObject bodyParts;

    public bool canTalk;

    private void OnEnable()
    {
        canTalk = true;
        
        bodyParts.SetActive(true);
        
        stateManager = GetComponent<StateManager>();
        
       // LevelEventManager.LevelEventInstance.OnCanTalk(true);

        LevelEventManager.LevelEventInstance.StopTalk += () =>
        {
            StopTalk();
        };
        
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
        if (canTalk)
            DialogueManager.DialogueInstance.ShowDialogue(dialogue);
    }

    private void StopTalk()
    {
        stateManager.ChangeStateString("death");
        canTalk = false;
    }

    private void OnDisable()
    {
        //LevelEventManager.LevelEventInstance.StopTalk -= StopTalk;
        bodyParts.SetActive(false);
        StopAllCoroutines();
    }
}
