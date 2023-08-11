using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PieceConfigSO))]
public class OpenPieceSOWindow : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Open Config Window"))
        {
            PieceSOWindow.ShowWindow();
        }
    }
}
