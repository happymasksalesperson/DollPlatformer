using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Elevator_Model))]
public class ElevatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Going Up"))
        {
            (target as Elevator_Model)?.Up();
        }

        if (GUILayout.Button("Going Down"))
        {
            (target as Elevator_Model)?.Down();
        }
    }
}