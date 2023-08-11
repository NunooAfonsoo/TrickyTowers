using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelConfigSO))]
public class OpenLevelConfigSOWindow : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Open Config Window"))
        {
            LevelConfigSOWindow.ShowWindow();
        }
    }
}
