using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnPoint : MonoBehaviour
{
    private SpriteRenderer spr;

    private void OnEnable()
    {
        spr = GetComponent<SpriteRenderer>();
        spr.enabled = false;
    }
}
