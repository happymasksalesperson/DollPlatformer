using Candlewitch;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HealthModel))]
public class HealthModelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        HealthModel healthModel = (HealthModel)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Change Health"))
        {
            healthModel.TestChangeHealth();
        }

        if (GUILayout.Button("Resurrect"))
        {
            healthModel.Resurrect();
        }

        if (GUILayout.Button("Kill"))
        {
            healthModel.Death();
        }

        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}