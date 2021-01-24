using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChipModule))]
public class ChipEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //serializedObject.
        //ChipModule module = target as ChipModule;
        //EditorGUILayout.IntSlider()

        //TODO: Figure out how to manage creation of inputs and outputs

        base.DrawDefaultInspector();
    }

    private void OnSceneGUI()
    {
        //Handles.ScaleValueHandle(1f, target.)
        //Handles.ConeHandleCap
    }
}
