using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TextOnScreen : MonoBehaviour
{
    public TextMeshProUGUI converse;
    private void Start()
    {
        StartCoroutine(ChangeText());
    }

    IEnumerator ChangeText()
    {
        converse.text = "Oh hey!";
        yield return new WaitForSeconds(2f);
        converse.text = "You're still trapped in this box!";
        yield return new WaitForSeconds(4f);
        converse.text = "Lets change that shall we ;)";
        yield return new WaitForSeconds(4f);
    }
}
