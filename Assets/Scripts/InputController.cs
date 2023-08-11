using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    [SerializeField] private MovementDirectionEventSO pieceMovedEvent;
    [SerializeField] private VoidEventSO pieceStopedEvent;
    [SerializeField] private MovementDirectionEventSO pieceRotatedEvent;

    [SerializeField] private Button rotateLeft;
    [SerializeField] private Button rotateRight;

    private void Awake()
    {
        rotateRight.onClick.AddListener(() => pieceRotatedEvent.RaiseEvent(MovementDirection.Right));
        rotateLeft.onClick.AddListener(() => pieceRotatedEvent.RaiseEvent(MovementDirection.Left));
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Helpers.GetWorldPosition(touch.position);
            bool isTouchOverUI = Helpers.IsOverUIElement(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (isTouchOverUI)
                    {
                        break;
                    }
                    MovementDirection movementDirection = GetMovementDirectionFromTouchPosition(touchPosition);

                    if (movementDirection != MovementDirection.None)
                    {
                        pieceMovedEvent.RaiseEvent(movementDirection);
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    pieceStopedEvent.RaiseEvent();
                    break;
                    default: break;
            }
        }

        #if UNITY_EDITOR
        CheckForEditorInput();
        #endif
    }

    /// <summary>
    /// Returns movement direction from the touch position on screen
    /// </summary>
    /// <param name="touchPosition">World position of touch</param>
    /// <returns></returns>
    private MovementDirection GetMovementDirectionFromTouchPosition(Vector3 touchPosition)
    {
        if (touchPosition.x <= -1f)
        {
            return MovementDirection.Left;
        }
        else if (touchPosition.x >= 1f)
        {
            return MovementDirection.Right;
        }

        return MovementDirection.None;
    }

    private void CheckForEditorInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool isTouchOverUI = Helpers.IsOverUIElement(Input.mousePosition);

            if (!isTouchOverUI)
            {
                MovementDirection movementDirection = GetMovementDirectionFromTouchPosition(Helpers.GetWorldPosition(Input.mousePosition));

                if (movementDirection != MovementDirection.None)
                {
                    pieceMovedEvent.RaiseEvent(movementDirection);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            bool isTouchOverUI = Helpers.IsOverUIElement(Input.mousePosition);

            if (!isTouchOverUI)
            {
                pieceStopedEvent.RaiseEvent();
            }
        }
    }
}