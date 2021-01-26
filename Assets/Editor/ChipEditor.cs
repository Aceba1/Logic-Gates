using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChipModule))]
[CanEditMultipleObjects]
public class ChipEditor : Editor
{
    bool showCore;
    public override void OnInspectorGUI()
    {
        ChipModule module = (ChipModule)target;
        //serializedObject.
        //ChipModule module = target as ChipModule;
        //EditorGUILayout.IntSlider()

        //TODO: Figure out how to manage creation of inputs and outputs

        showCore = EditorGUILayout.BeginFoldoutHeaderGroup(showCore, "Core");

        if (showCore)
        {
            LogicNode node = module.core;

            GUILayout.Label("LOGIC-Node");
            GUILayout.Label($"Cycle: {node.lastCycle} Index: {node.lastIndex}");
            GUILayout.Label($"Inputs: {node.ConnectedInputs} ({node.Inputs})");
            if (node is BasicNode basic)
            {
                GUILayout.Label("BASIC-Node");
                GUILayout.Label($"Valid Inputs: {basic.validInputs} ({node.ConnectedInputs})");
            }
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        base.DrawDefaultInspector();
    }

    private void OnSceneGUI()
    {
        //Handles.ScaleValueHandle(1f, target.)
        //Handles.ConeHandleCap
    }
}
