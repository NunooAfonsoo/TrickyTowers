using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StringEvent", menuName = "ScriptableObjects/Events/StringEventSO")]
public class StringEventSO : ScriptableObject
{
    public event Action<string> OnEventRaised;

    public void RaiseEvent(string text)
    {
        OnEventRaised?.Invoke(text);
    }
}