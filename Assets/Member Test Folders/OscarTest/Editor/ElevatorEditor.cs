using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Elevator))]
public class ElevatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Take The Elevator Up?") && Application.isPlaying)
        {
            (target as Elevator)?.ElevatorUp();
        }
        if (GUILayout.Button("Take The Elevator Down?") && Application.isPlaying)
        {
            (target as Elevator)?.ElevatorDown();
        }
    }
}
