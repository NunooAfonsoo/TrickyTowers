using UnityEngine;
using UnityEngine.UI;

public class MoveableTexture : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private RawImage image;
    private Rect imageUvRect;

    private void Awake()
    {
        image = GetComponent<RawImage>();
        imageUvRect = image.uvRect;
    }

    private void Update()
    {
        imageUvRect.x -= moveSpeed * Time.deltaTime;
        image.uvRect = imageUvRect;
    }
}
