using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraConfigSO))]
public class OpenCameraConfigSOWindow : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Open Config Window"))
        {
            CameraConfigSOWindow.ShowWindow();
        }
    }
}
