using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Helpers
{
    private static Camera camera = Camera.main;

    public static Vector3 GetWorldPosition(Vector3 cursorPosition)
    {
        if (camera == null)
        {
            camera = Camera.main;
        }

        return camera.ScreenToWorldPoint(cursorPosition);
    }

    private static PointerEventData eventDataCurrentPosition;
    private static List<RaycastResult> results;

    public static bool IsOverUIElement(Vector3 cursorPosition)
    {
        eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = cursorPosition };
        results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
}
