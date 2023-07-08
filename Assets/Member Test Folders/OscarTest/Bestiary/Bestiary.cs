using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bestiary : MonoBehaviour
{
    public static List<GameObject> encounteredCharacters = new List<GameObject>();
    
    public static void addCharacterToBestiary(GameObject characterFound)
    {
        if (!encounteredCharacters.Contains(characterFound))
        {
            encounteredCharacters.Add(characterFound);
        }
    }
}
