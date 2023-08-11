using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VoidEventSO))]
[CanEditMultipleObjects]
public class VoidEventSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        VoidEventSO boolEventSO = (VoidEventSO)target;

        if (GUILayout.Button("Raise Event"))
        {
            boolEventSO.RaiseEvent();
        }
    }
}

[CustomEditor(typeof(BoolEventSO))]
[CanEditMultipleObjects]
public class BoolEventSOEditor : Editor
{
    private bool valueToRaise = false;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BoolEventSO boolEventSO = (BoolEventSO)target;

        valueToRaise = EditorGUILayout.Toggle("Value to Raise: (Testing purposes only)", valueToRaise);

        if (GUILayout.Button("Raise Event"))
        {
            boolEventSO.RaiseEvent(valueToRaise);
        }
    }
}

[CustomEditor(typeof(IntEventSO))]
[CanEditMultipleObjects]
public class IntEventSOEditor : Editor
{
    private int valueToRaise = 0;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        IntEventSO intEventSO = (IntEventSO)target;

        valueToRaise = EditorGUILayout.IntField("Value to Raise: (Testing purposes only)", valueToRaise);

        if (GUILayout.Button("Raise Event"))
        {
            intEventSO.RaiseEvent(valueToRaise);
        }
    }
}

[CustomEditor(typeof(StringEventSO))]
[CanEditMultipleObjects]
public class StringEventSOEditor : Editor
{
    private string valueToRaise = "";

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        StringEventSO boolEventSO = (StringEventSO)target;

        valueToRaise = EditorGUILayout.TextField("Value to Raise: (Testing purposes only)", valueToRaise);

        if (GUILayout.Button("Raise Event"))
        {
            boolEventSO.RaiseEvent(valueToRaise);
        }
    }
}

[CustomEditor(typeof(MovementDirectionEventSO))]
[CanEditMultipleObjects]
public class MovementDirectionSOEditor : Editor
{
    private MovementDirection valueToRaise = MovementDirection.None;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MovementDirectionEventSO movementDirectionEventSO = (MovementDirectionEventSO)target;

        valueToRaise = (MovementDirection)EditorGUILayout.EnumPopup("Movement Direction: (Testing purposes only)", valueToRaise);

        if (GUILayout.Button("Raise Event"))
        {
            movementDirectionEventSO.RaiseEvent(valueToRaise);
        }
    }
}

[CustomEditor(typeof(SpriteEventSO))]
[CanEditMultipleObjects]
public class SpriteEventSOEditor : Editor
{
    private Sprite valueToRaise;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SpriteEventSO spriteEventSO = (SpriteEventSO)target;

        valueToRaise = (Sprite)EditorGUILayout.ObjectField("Movement Direction: (Testing purposes only)", valueToRaise, typeof(Sprite), false);

        if (GUILayout.Button("Raise Event"))
        {
            spriteEventSO.RaiseEvent(valueToRaise);
        }
    }
}