using UnityEditor;
using UnityEngine;

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

        if (GUILayout.Button("Shoot Fireball"))
        {
            flameWheel.Shoot();
        }

        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}