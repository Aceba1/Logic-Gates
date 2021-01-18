using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChipModule))]
public class ChipEditor : Editor
{    
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
    }
}
