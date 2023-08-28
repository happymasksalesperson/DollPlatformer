using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bestiary : MonoBehaviour, IDataPersistence
{
    public List<BestiaryCharacter> bestiaryEntries = new List<BestiaryCharacter>();
    
    public static Bestiary instance;

    public GameObject bestiaryView;
    private bool viewDisplayed;

    public void Awake()
    {
        InitiateSingleton();
    }

    private void InitiateSingleton()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public event Action<List<BestiaryCharacter>> bestiaryEvent;
    public void DisplayCharacterToBestiary(BestiaryCharacter characterFound)
    {
        if (!bestiaryEntries.Contains(characterFound))
        {
            bestiaryEntries.Add(characterFound);
            bestiaryEvent?.Invoke(bestiaryEntries);
        }
    }

    public void LoadData(GameData data)
    {
        bestiaryEntries = data.bestiaryCharacters;
    }

    public void SaveData(ref GameData data)
    {
        foreach (BestiaryCharacter beast in bestiaryEntries)
        {
            data.bestiaryCharacters.Add(beast);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OpenBestiary();
        }
    }

    public void OpenBestiary()
    {
        if (viewDisplayed)
        {
            bestiaryView.SetActive(false);
            viewDisplayed = false;
        }
        else
        {
            bestiaryView.SetActive(true);
            viewDisplayed = true;
            bestiaryEvent?.Invoke(bestiaryEntries);
        }
    }
}
