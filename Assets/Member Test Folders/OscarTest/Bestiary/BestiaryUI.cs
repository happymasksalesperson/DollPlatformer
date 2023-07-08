using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BestiaryUI : MonoBehaviour
{
    
    /// Idk if GPT is helping me here but the UI aspect is hard.
    /// I have a general idea but creating new UI for every item is difficult.
    /// Might just need a set amount so that the bestiary can just enable the access to the new one,
    /// that method is a bit hard coded but might be the better method otherwise its very convoluted...
    /// will need to talk to the rest of the group.
    
    public GameObject bestiaryPanel; // Reference to the main panel containing the bestiary UI
    public GameObject characterEntryPrefab; // Prefab for individual character entries

    private List<GameObject> characterEntries; // List to keep track of created character entries

    // Method to display the bestiary UI
    public void ShowBestiary()
    {
        bestiaryPanel.SetActive(true);
        GenerateCharacterEntries();
    }

    // Method to hide the bestiary UI
    public void HideBestiary()
    {
        bestiaryPanel.SetActive(false);
        ClearCharacterEntries();
    }

    // Method to generate character entries based on encountered characters
    private void GenerateCharacterEntries()
    {
        ClearCharacterEntries();

        foreach (GameObject character in Bestiary.encounteredCharacters)
        {
            // Instantiate a new character entry from the prefab
            GameObject entry = Instantiate(characterEntryPrefab, bestiaryPanel.transform);
            
            // Set character information in the entry (e.g., name, image, etc.)
            // ...
            
            // Add event listener for hover effect
            EventTrigger trigger = entry.GetComponent<EventTrigger>();
            EventTrigger.Entry entryEvent = new EventTrigger.Entry();
            entryEvent.eventID = EventTriggerType.PointerEnter;
            entryEvent.callback.AddListener((eventData) => { OnCharacterEntryHover(entry); });
            trigger.triggers.Add(entryEvent);
            
            characterEntries.Add(entry);
        }
    }

    // Method to clear existing character entries
    private void ClearCharacterEntries()
    {
        foreach (GameObject entry in characterEntries)
        {
            Destroy(entry);
        }

        characterEntries.Clear();
    }

    // Method called when hovering over a character entry
    private void OnCharacterEntryHover(GameObject entry)
    {
        // Implement the desired hover effect, such as displaying character details in a tooltip or highlighting the entry
    }
}
