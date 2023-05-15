using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CogsManager))]
public class CogsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if (GUILayout.Button("STOP/START ALL COGS"))
        {
            (target as CogsManager)?.StopStartAllCogs();
        }
        
        if (GUILayout.Button("REVERSE ALL COGS"))
        {
            (target as CogsManager)?.ReverseAllCogs();
        }
        
        if (GUILayout.Button("STOP/START SPECIFIC COG"))
        {
            (target as CogsManager)?.StopStartSpecificCog();
        }
        
        if (GUILayout.Button("REVERSE SPECIFIC COG"))
        {
            (target as CogsManager)?.ReverseSpecificCog();
        }
    }
}