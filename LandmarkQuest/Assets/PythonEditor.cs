using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Scripting.Python;

[CustomEditor(typeof(PythonManager))]
public class PythonEditor : Editor
{
    PythonManager targetManager;

    private string scriptPath = "/Python/EyeTracking1/EyeTracking1/EyeTracking1.py";

    private void OnEnable()
    {
        targetManager = (PythonManager)target;
    }

    public override void OnInspectorGUI() {
        EditorGUILayout.BeginHorizontal();
        scriptPath = EditorGUILayout.TextField("Python Script Path", scriptPath);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Launch Python Script", GUILayout.Height(35))) {
            string path = Application.dataPath + scriptPath;
            PythonRunner.RunFile(path);
        }
           

    }
}
