using UnityEngine;

public class CameraTopTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Layers.LANDED_PIECES_LAYER)
        {
            CameraManager.Instance.Move(MovementDirection.Up);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Layers.LANDED_PIECES_LAYER)
        {
            CameraManager.Instance.StopMove();
        }
    }
}
