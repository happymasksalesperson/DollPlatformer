using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bestiary : MonoBehaviour
{
    public List<BestiaryCharacter.Character> bestiaryEntries = new List<BestiaryCharacter.Character>();
    
    #region Instance Stuff To Avoid Duplicates
    
    public static Bestiary instance;

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
    #endregion
    
    public void DisplayCharacterToBestiary(BestiaryCharacter characterFound)
    {
        if (!bestiaryEntries.Contains(characterFound.typeOfCharacter))
        {
            bestiaryEntries.Add(characterFound.typeOfCharacter);
        }
    }
}
