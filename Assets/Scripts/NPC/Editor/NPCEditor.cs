using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StateManager))]

public class StateManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if (GUILayout.Button("Attack"))
        {
            (target as StateManager)?.ChangeStateString("attack"); 
        }

        if (GUILayout.Button("Take Damage"))
        {
            (target as StateManager)?.ChangeStateString("takeDamage"); 
        }
        
        if (GUILayout.Button("Death"))
        {
            (target as StateManager)?.ChangeStateString("death");
        }
    }
}