using System.Collections;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>, IMoveable
{
    [SerializeField] private CameraConfigSO cameraConfig;

    [SerializeField] private VoidEventSO cameraStoppedEvent;
    [SerializeField] private VoidEventSO piecePlacedEvent;

    private float currentSpeed;
    private Vector3 originalCameraPosition;
    private Coroutine cameraShakeCoroutine;

    protected override void Awake()
    {
        base.Awake();

        currentSpeed = 0;
    }

    private void OnEnable()
    {
        piecePlacedEvent.OnEventRaised += ShakeCamera;
    }

    private void OnDisable()
    {
        piecePlacedEvent.OnEventRaised -= ShakeCamera;
    }

    private void Update()
    {
        if (transform.position.y <= CameraConsts.LOWEST_Y_POSITION && currentSpeed < 0)
        {
            StopMove();
        }

        transform.position += Vector3.up * currentSpeed * Time.deltaTime;
    }

    public void ShakeCamera()
    {
        originalCameraPosition = Camera.main.transform.position;

        if (cameraShakeCoroutine != null)
        {
            StopCoroutine(cameraShakeCoroutine);
        }

        cameraShakeCoroutine = StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsedTime = 0;

        while (elapsedTime < cameraConfig.ShakeDuration)
        {
            Vector3 shakeOffset = Random.insideUnitCircle * cameraConfig.ShakeIntensity;
            Camera.main.transform.position = originalCameraPosition + new Vector3(shakeOffset.x, shakeOffset.y, 0);

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        Camera.main.transform.position = originalCameraPosition;
    }

    public void Move(MovementDirection movementDirection)
    {
        switch (movementDirection)
        {
            case MovementDirection.Left:
            case MovementDirection.Right:
                break;
            case MovementDirection.Up:
                if (currentSpeed >= 0)
                {
                    currentSpeed = cameraConfig.Speed;
                }
                break;
            case MovementDirection.Down:
                if (currentSpeed <= 0)
                {
                    currentSpeed = -cameraConfig.Speed;
                }
                break;
        }
    }

    public void StopMove()
    {
        currentSpeed = 0;

        cameraStoppedEvent.RaiseEvent();
    }
}