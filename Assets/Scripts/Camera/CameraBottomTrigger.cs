using UnityEngine;

public class CameraBottomTrigger : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        CameraManager.Instance.Move(MovementDirection.Down);
    }
}
