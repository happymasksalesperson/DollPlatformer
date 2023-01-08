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
        
        if (GUILayout.Button("Attack01"))
        {
            (target as StateManager)?.ChangeStateString("attack01"); 
        }
        
        if (GUILayout.Button("Jump"))
        {
            (target as StateManager)?.ChangeStateString("jump"); 
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