using UnityEngine;

[CreateAssetMenu(fileName = "CameraConfig", menuName = "ScriptableObjects/CameraConfigSO")]
public class CameraConfigSO : BaseScriptableObject
{
    public float Speed;
    public float ShakeDuration;
    public float ShakeIntensity;
}