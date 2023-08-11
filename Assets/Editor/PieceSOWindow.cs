using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PieceSOWindow : EditorWindow
{
    private PieceConfigSO pieceSO;
    private int space = 25;

    private static List<PieceConfigSO> pieces = new List<PieceConfigSO>();

    [MenuItem("Tools/Pieces Editor")]
    public static void ShowWindow()
    {
        GetWindow<PieceSOWindow>("Pieces Editor");

        PieceConfigSO selectedLevelConfig = Selection.activeObject as PieceConfigSO; // Automatically assigns the selected PieceSO
        if (selectedLevelConfig != null)
        {
            GetWindow<PieceSOWindow>("Pieces Editor").pieceSO = selectedLevelConfig;
        }

        pieces = GetAllPieces();
    }

    private void OnGUI()
    {
        GUILayout.Space(space);

        pieceSO = EditorGUILayout.ObjectField("Piece Scriptable Object", pieceSO, typeof(PieceConfigSO), false) as PieceConfigSO;

        if (pieceSO == null)
        {
            EditorGUILayout.HelpBox("Please assign a PieceSO.", MessageType.Error);
            return;
        }

        DrawDefaultInspectorWindow();

        if (pieces.Count > 1)
        {
            if (GUILayout.Button("Set Mass of All Pieces"))
            {
                bool confirmedChanges = EditorUtility.DisplayDialog("Set Mass of All Pieces", "Are you sure you want to set the mass for all pieces?", "Yes", "No");

                if (confirmedChanges)
                {
                    foreach (PieceConfigSO piece in pieces)
                    {
                        piece.Mass = pieceSO.Mass;
                    }
                }
            }
        }
    }

    private void DrawDefaultInspectorWindow()
    {
        SerializedObject serializedObject = new SerializedObject(pieceSO);
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

    private static List<PieceConfigSO> GetAllPieces()
    {
        string[] guids = AssetDatabase.FindAssets("t:PieceConfigSO");
        List<PieceConfigSO> pieces = new List<PieceConfigSO>();

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            PieceConfigSO pieceSO = AssetDatabase.LoadAssetAtPath<PieceConfigSO>(assetPath);
            pieces.Add(pieceSO);
        }

        return pieces;
    }
}