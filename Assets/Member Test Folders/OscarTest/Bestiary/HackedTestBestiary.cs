using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackedTestBestiary : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BestiaryCharacter>())
        {
            Bestiary.instance.DisplayCharacterToBestiary(other.GetComponent<BestiaryCharacter>());
        }
    }
    
    //do a raycast because gravity not working to test it D:
}
