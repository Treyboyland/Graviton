using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ValidSpawnTester))]
[CanEditMultipleObjects]
public class SpawnTesterEditor : Editor
{
    SerializedProperty wallHolder;

    SerializedProperty point;

    SerializedProperty minPosition;

    SerializedProperty maxPosition;

    SerializedProperty increments;

    SerializedProperty distanceFromWall;

    ValidSpawnTester tester;

    void OnEnable()
    {
        wallHolder = serializedObject.FindProperty("wallHolder");
        point = serializedObject.FindProperty("point");
        minPosition = serializedObject.FindProperty("minPosition");
        maxPosition = serializedObject.FindProperty("maxPosition");
        increments = serializedObject.FindProperty("increments");
        distanceFromWall = serializedObject.FindProperty("distanceFromWall");

        tester = (ValidSpawnTester)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUI.enabled = false;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"), true);
        GUI.enabled = true;

        EditorGUILayout.PropertyField(wallHolder);
        EditorGUILayout.PropertyField(point);
        EditorGUILayout.PropertyField(minPosition);
        EditorGUILayout.PropertyField(maxPosition);
        EditorGUILayout.PropertyField(increments);
        EditorGUILayout.PropertyField(distanceFromWall);

        EditorGUILayout.Space();
        if (GUILayout.Button("Test Spawns", EditorStyles.miniButton))
        {
            Debug.LogWarning("Run Test Here!");
            tester.RunTest();
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (GUILayout.Button("Generate Level", EditorStyles.miniButton))
        {
            tester.CreateLevel();
        }


        serializedObject.ApplyModifiedProperties();
    }
}
