using UnityEngine;

public class NormalPieceController : PieceController
{
    [SerializeField] private MovementDirectionEventSO pieceMovedEvent;
    [SerializeField] private VoidEventSO pieceStopedEvent;
    [SerializeField] private MovementDirectionEventSO pieceRotatedEvent;

    private ParticleSystem[] particles;

    protected override void Awake()
    {
        base.Awake();

        particles = GetComponentsInChildren<ParticleSystem>();
    }

    private void Start()
    {
        GameManager.Instance.SetActivePiece(this);
    }

    private void OnEnable()
    {
        pieceMovedEvent.OnEventRaised += Move;
        pieceStopedEvent.OnEventRaised += StopMove;
        pieceRotatedEvent.OnEventRaised += Rotate;
    }

    private void OnDisable()
    {
        pieceMovedEvent.OnEventRaised -= Move;
        pieceStopedEvent.OnEventRaised -= StopMove;
        pieceRotatedEvent.OnEventRaised -= Rotate;
    }

    public override void Place()
    {
        base.Place();

        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }

        pieceMovedEvent.OnEventRaised -= Move;
        pieceStopedEvent.OnEventRaised -= StopMove;
        pieceRotatedEvent.OnEventRaised -= Rotate;
    }

    public override void Reset()
    {
        base.Reset();

        GameManager.Instance.SetActivePiece(this);
    }
}
