using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Mission))]
public class MissionEditor : Editor
{
    public static Mission mission;

    public void OnEnable()
        => mission = target as Mission;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.BeginVertical();

        EditorGUILayout.EndVertical();
    }
}
