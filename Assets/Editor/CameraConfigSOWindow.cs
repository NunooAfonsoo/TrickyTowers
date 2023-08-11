using UnityEngine;
using UnityEditor;

public class CameraConfigSOWindow : EditorWindow
{
    private CameraConfigSO cameraConfigSO;
    private Vector2 scrollPosition = Vector2.zero;
    private int space = 25;

    [MenuItem("Tools/Camera Config Editor")]
    public static void ShowWindow()
    {
        GetWindow<CameraConfigSOWindow>("Camera Config Editor");

        CameraConfigSO selectedCameraConfig = Selection.activeObject as CameraConfigSO; // Automatically assigns the selected LevelConfigSO
        if (selectedCameraConfig != null)
        {
            GetWindow<CameraConfigSOWindow>("Camera Config Editor").cameraConfigSO = selectedCameraConfig;
        }
    }

    private void OnGUI()
    {
        GUILayout.Space(space);

        cameraConfigSO = EditorGUILayout.ObjectField("Camera Config Scriptable Object", cameraConfigSO, typeof(CameraConfigSO), false) as CameraConfigSO;

        if (cameraConfigSO == null)
        {
            EditorGUILayout.HelpBox("Please assign a CameraConfigSO.", MessageType.Error);
            return;
        }

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        DrawDefaultInspectorWindow();

        EditorGUILayout.EndScrollView();
    }

    private void DrawDefaultInspectorWindow()
    {
        SerializedObject serializedObject = new SerializedObject(cameraConfigSO);
        SerializedProperty iterator = serializedObject.GetIterator();
        bool enterChildren = true;

        while (iterator.NextVisible(enterChildren))
        {
            enterChildren = false;
            if (iterator.name == "m_Script") // We don't want to change the SO script reference
            {
                continue;
            }

            EditorGUILayout.PropertyField(iterator, true);
        }
        serializedObject.ApplyModifiedProperties();
    }
}