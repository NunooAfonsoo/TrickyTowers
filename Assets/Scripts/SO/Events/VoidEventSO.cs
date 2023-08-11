using System;
using UnityEngine;

[CreateAssetMenu(fileName = "VoidEvent", menuName = "ScriptableObjects/Events/VoidEventSO")]
public class VoidEventSO : ScriptableObject
{
    public event Action OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}