using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BoolEvent", menuName = "ScriptableObjects/Events/BoolEventSO")]
public class BoolEventSO : ScriptableObject
{
    public event Action<bool> OnEventRaised;

    public void RaiseEvent(bool value)
    {
        OnEventRaised?.Invoke(value);
    }
}