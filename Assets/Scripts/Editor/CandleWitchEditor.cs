using Candlewitch;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CandlewitchBrain))]
public class CandleWitchEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CandlewitchBrain candleWitch = (CandlewitchBrain)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Change State"))
        {
            candleWitch.TestChangeState();
        }

        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}