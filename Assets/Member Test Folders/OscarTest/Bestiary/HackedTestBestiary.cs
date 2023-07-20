using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackedTestBestiary : MonoBehaviour
{
    public Bestiary bestiary;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BestiaryCharacter>())
        {
            bestiary.DisplayCharacterToBestiary(other.GetComponent<BestiaryCharacter>());
        }
    }
    
    //do a raycast because gravity not working to test it D:
}
