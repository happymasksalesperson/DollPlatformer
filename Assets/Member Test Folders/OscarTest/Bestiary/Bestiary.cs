using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bestiary : MonoBehaviour
{
    public List<BestiaryCharacter.Character> bestiaryEntries = new List<BestiaryCharacter.Character>();

    public void DisplayCharacterToBestiary(BestiaryCharacter characterFound)
    {
        if (!bestiaryEntries.Contains(characterFound.typeOfCharacter))
        {
            bestiaryEntries.Add(characterFound.typeOfCharacter);
        }
    }
}
