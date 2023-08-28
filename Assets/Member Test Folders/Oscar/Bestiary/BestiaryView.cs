using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BestiaryView : MonoBehaviour
{
    private Bestiary bestiaryModel;

    public List<Button> buttons;
    
    public GameObject titleoObj;
    private TextMeshPro title;

    public GameObject extraInfoObj;
    private TextMeshPro extraInfo;

    public GameObject descriptionObj;
    private TextMeshPro description;
    
    public Image image;
    public AudioSource sounds;

    //gpt variables
    private Dictionary<Button, BestiaryCharacter> buttonToCharacterMap = new Dictionary<Button, BestiaryCharacter>();
    
    private void Start()
    {
        bestiaryModel = Bestiary.instance; // Reference to your Bestiary script.
        
        bestiaryModel.bestiaryEvent += BestiaryModelOnbestiaryEvent;
        
        title = titleoObj.GetComponent<TextMeshPro>();
        extraInfo = extraInfoObj.GetComponent<TextMeshPro>();
        description = descriptionObj.GetComponent<TextMeshPro>();

        UpdateView();

        // Initially, hide the information fields.
        HideCreatureInfo();
    }

    private void UpdateView()
    {
        // Create a mapping between buttons and characters.
        for (int i = 0; i < buttons.Count; i++)
        {
            if (i < bestiaryModel.bestiaryEntries.Count)
            {
                buttonToCharacterMap[buttons[i]] = bestiaryModel.bestiaryEntries[i];
                //UpdateButtonLabel(buttons[i], bestiaryModel.bestiaryEntries[i].ToString());
                // You can add code here to change the appearance of the button to indicate it's discovered.
                buttons[i].onClick.AddListener(() => DisplayCreatureInfo(bestiaryModel.bestiaryEntries[i]));
            }
            else
            {
                // Disable buttons for undiscovered creatures.
                buttons[i].interactable = false;
            }
        }
    }

    private void BestiaryModelOnbestiaryEvent(List<BestiaryCharacter> obj)
    {
        UpdateView();
    }

    // Display information for the selected creature.
    private void DisplayCreatureInfo(BestiaryCharacter character)
    {
        BestiaryCharacter creature = GetCreatureInfo(character);

        // Update UI elements with creature information.
        title.text = creature.name;
        description.text = creature.description;
        extraInfo.text = creature.dificultyLevel.ToString();
        // You can add code to display the creature image here if you have one.

        // Show the information fields.
        ShowCreatureInfo();
    }

    // Hide creature information.
    private void HideCreatureInfo()
    {
        titleoObj.SetActive(false);
        descriptionObj.SetActive(false);
        extraInfoObj.SetActive(false);
        // Hide the creature image if you displayed one.
        image.gameObject.SetActive(false);
    }

    // Show creature information.
    private void ShowCreatureInfo()
    {
        titleoObj.SetActive(true);
        descriptionObj.SetActive(true);
        extraInfoObj.SetActive(true);
        // Hide the creature image if you displayed one.
        image.gameObject.SetActive(true);
    }

    // Get the BestiaryCharacter data for a specific character.
    private BestiaryCharacter GetCreatureInfo(BestiaryCharacter character)
    {
        // Find and return the creature data in the bestiary.
        return bestiaryModel.bestiaryEntries.Find(entry => entry == character);
    }

    // Update the label text of a button.
    private void UpdateButtonLabel(Button button, string label)
    {
        button.GetComponentInChildren<Text>().text = label;
    }
}
