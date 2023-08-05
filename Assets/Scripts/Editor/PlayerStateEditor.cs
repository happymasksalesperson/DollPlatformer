using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerStateManager))]
public class PlayerStateEditor : Editor
{
    private PlayerStates inputState;

    private PlayerMoments inputMoment;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlayerStateManager stateManager = (PlayerStateManager)target;

        GUILayout.Space(10);

        EditorGUILayout.LabelField("Select State:", EditorStyles.boldLabel);

        inputState = (PlayerStates)EditorGUILayout.EnumPopup("Player States", inputState);

        if (GUILayout.Button("Change to Selected State"))
        {
            stateManager.ChangeState(inputState);
        }

        EditorGUILayout.LabelField("Select Moment:", EditorStyles.boldLabel);

        inputMoment = (PlayerMoments)EditorGUILayout.EnumPopup("Player Moments", inputMoment);

        if (GUILayout.Button("Activate Selected Moment"))
        {
            stateManager.DeclareMoment(inputMoment);
        }
    }
}