using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementDirectionEvent", menuName = "ScriptableObjects/Events/MovementDirectionEventSO")]
public class MovementDirectionEventSO : ScriptableObject
{
    public event Action<MovementDirection> OnEventRaised;

    public void RaiseEvent(MovementDirection movementDirection)
    {
        OnEventRaised?.Invoke(movementDirection);
    }
}