using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(DialogTree))]
public class DialogTreeEditor : Editor {

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        //DrawDefaultInspector();
        GUILayout.Label("Editor for the Dialog Tree");
        GUILayout.Space(10f);
        if(GUILayout.Button("Open Editor"))
        {
            DialogTreeWindow.OpenWindow((DialogTree)target);
        }
        
        serializedObject.ApplyModifiedProperties();
    }
}
