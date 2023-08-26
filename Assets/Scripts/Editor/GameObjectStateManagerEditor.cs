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

[CustomEditor(typeof(FlameWheel))]
public class FlameWheelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        FlameWheel flameWheel = (FlameWheel)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Spawn Fireballs"))
        {
            flameWheel.ChangeFireballNumber();
        }

        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}

