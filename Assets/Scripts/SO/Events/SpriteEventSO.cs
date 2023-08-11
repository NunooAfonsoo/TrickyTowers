using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteEvent", menuName = "ScriptableObjects/Events/SpriteEventSO")]
public class SpriteEventSO : ScriptableObject
{
    public event Action<Sprite> OnEventRaised;

    public void RaiseEvent(Sprite sprite)
    {
        OnEventRaised?.Invoke(sprite);
    }
}