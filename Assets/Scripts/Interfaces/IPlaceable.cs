using UnityEngine;

public interface IPlaceable 
{
    bool HasBeenPlaced { get; }
    void Place();
}