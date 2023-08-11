using UnityEngine;

public class NormalSpawner : Spawner
{
    [SerializeField] private SpriteEventSO spawnNewPieceEvent;
    [SerializeField] private VoidEventSO cameraStoppedEvent;

    private int nextPieceIndex;

    protected override void Awake()
    {
        base.Awake();

        UpdateSpawnPosition();

        spawnPosition = spawnerLocation.position;

        nextPieceIndex = GetNextPieceIndex();
    }

    private void Start()
    {
        piecesPool.Get();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        cameraStoppedEvent.OnEventRaised += UpdateSpawnPosition;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        cameraStoppedEvent.OnEventRaised -= UpdateSpawnPosition;
    }

    protected override PieceController SpawnNewPiece()
    {
        PieceController piece = Instantiate(piecesSOs[nextPieceIndex].prefab, spawnPosition, Quaternion.identity, piecesContainer).GetComponent<PieceController>();
        piece.SetPool(piecesPool);

        nextPieceIndex = GetNextPieceIndex();

        spawnNewPieceEvent.RaiseEvent(piecesSOs[nextPieceIndex].pieceSprite);

        return piece;
    }

    private int GetNextPieceIndex()
    {
        return Random.Range(0, piecesSOs.Length);
    }
}
