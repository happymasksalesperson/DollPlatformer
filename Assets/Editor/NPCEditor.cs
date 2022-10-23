using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NPC01_PatrolState))]

public class NPC01PatrolStateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if (GUILayout.Button("Attack"))
        {
            (target as NPC01_PatrolState)?.Attack();
        }

        if (GUILayout.Button("Take Damage") && Application.isPlaying)
        {
            (target as NPC01_PatrolState)?.TakeDamage();
        }
        
        
    }
}