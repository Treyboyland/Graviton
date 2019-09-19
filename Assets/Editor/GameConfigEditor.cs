using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameConfigReader))]
[CanEditMultipleObjects]
public class GameConfigEditor : Editor
{
    SerializedProperty config;
    SerializedProperty defaultConfig;

    GameConfigReader configReader;

    private void OnEnable()
    {
        config = serializedObject.FindProperty("configuration");
        defaultConfig = serializedObject.FindProperty("defaultConfig");

        configReader = (GameConfigReader)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUI.enabled = false;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"), true);
        GUI.enabled = true;

        EditorGUILayout.PropertyField(config, true);
        EditorGUILayout.PropertyField(defaultConfig, true);

        EditorGUILayout.Space();

        if (GUILayout.Button("Save Configuration", EditorStyles.miniButton))
        {
            configReader.Configuration.Save(Application.streamingAssetsPath + "/Config.xml");
        }

        serializedObject.ApplyModifiedProperties();
    }
}
