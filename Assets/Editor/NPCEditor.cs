using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NPC01PatrolState))]

public class NPC01PatrolStateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if (GUILayout.Button("Attack"))
        {
            (target as NPC01PatrolState)?.Attack();
        }

        if (GUILayout.Button("Take Damage") && Application.isPlaying)
        {
            (target as NPC01PatrolState)?.TakeDamage();
        }
        
        
    }
}