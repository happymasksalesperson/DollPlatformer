using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager DialogueInstance { get; private set; }

    private void Awake()
    {
        DialogueInstance = this;
    }

    [SerializeField] private GameObject dialogueBox;

    [SerializeField] private TMP_Text text;

    [SerializeField] private int lettersPerSecond;

    private Dialogue dialogue;
    private int currentLine = 0;
    private bool isTyping;

    public void ShowDialogue(Dialogue dialogue)
    {
        this.dialogue = dialogue;
        
        dialogueBox.SetActive(true);
        StartCoroutine((TypeDialogue(dialogue.Lines[0])));
    }

    public void AdvanceDialogue()
    {
        if (!isTyping)
        {
            ++currentLine;
            if (currentLine < dialogue.Lines.Count)
            {
                StartCoroutine(TypeDialogue(dialogue.Lines[currentLine]));
            }
            else
            {
                CloseDialogue();
            }
        }
    }
    
    public IEnumerator TypeDialogue(string line)
    {
        isTyping = true;
        text.text = "";
        foreach (var letter in line.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }

    public void CloseDialogue()
    {
        currentLine = 0;
        dialogueBox.SetActive(false);
        LevelEventManager.LevelEventInstance.OnStopTalk();
    }

    private void OnDisable()
    {
    }
}
