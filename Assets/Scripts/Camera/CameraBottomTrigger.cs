using UnityEngine;

public class CameraBottomTrigger : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Layers.LANDED_PIECES_LAYER)
        {
            CameraManager.Instance.Move(MovementDirection.Down);
        }
    }
}
