using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FireballBrain))]
public class GameObjectStateManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        FireballBrain fireballBrain = (FireballBrain)target;

        DrawDefaultInspector();

        EditorGUILayout.LabelField("Current Test State: " + fireballBrain.testState);

        if (GUILayout.Button("Change State"))
        {
            fireballBrain.TestChangeState();
        }

        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}

