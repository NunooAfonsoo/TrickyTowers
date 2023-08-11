using UnityEngine;
using UnityEditor;

public class LevelConfigSOWindow : EditorWindow
{
    private LevelConfigSO levelConfigSO;
    private Vector2 scrollPosition = Vector2.zero;
    private int space = 25;

    [MenuItem("Tools/Level Config Editor")]
    public static void ShowWindow()
    {
        GetWindow<LevelConfigSOWindow>("Level Config Editor");

        LevelConfigSO selectedLevelConfig = Selection.activeObject as LevelConfigSO; // Automatically assigns the selected LevelConfigSO
        if (selectedLevelConfig != null)
        {
            GetWindow<LevelConfigSOWindow>("Level Config Editor").levelConfigSO = selectedLevelConfig;
        }
    }

    private void OnGUI()
    {
        GUILayout.Space(space);

        levelConfigSO = EditorGUILayout.ObjectField("Level Config Scriptable Object", levelConfigSO, typeof(LevelConfigSO), false) as LevelConfigSO;

        if (levelConfigSO == null)
        {
            EditorGUILayout.HelpBox("Please assign a LevelConfigSO.", MessageType.Error);
            return;
        }

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        DrawDefaultInspectorWindow();

        EditorGUILayout.EndScrollView();
    }

    private void DrawDefaultInspectorWindow()
    {
        SerializedObject serializedObject = new SerializedObject(levelConfigSO);
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