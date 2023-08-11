using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner: MonoBehaviour
{
    [SerializeField] protected PieceConfigSO[] piecesSOs;

    [SerializeField] private VoidEventSO piecePlacedEvent;
    [SerializeField] private BoolEventSO pieceDestroyedEvent;

    [SerializeField] protected Transform piecesContainer;
    [SerializeField] protected Transform spawnerLocation;

    protected Vector2 spawnPosition;
    protected IObjectPool<PieceController> piecesPool;

    protected virtual void Awake()
    {
        piecesPool = new ObjectPool<PieceController>(
                                    SpawnNewPiece,
                                    OnGet,
                                    OnRelease);
    }

    protected virtual void OnEnable()
    {
        piecePlacedEvent.OnEventRaised += OnPiecePlaced;
        pieceDestroyedEvent.OnEventRaised += OnPieceDestroyed;
    }

    protected virtual void OnDisable()
    {
        piecePlacedEvent.OnEventRaised -= OnPiecePlaced;
        pieceDestroyedEvent.OnEventRaised -= OnPieceDestroyed;
    }

    private void OnPiecePlaced()
    {
        piecesPool.Get();
    }

    private void OnPieceDestroyed(bool spawnNewPiece)
    {
        if (spawnNewPiece)
        {
            piecesPool.Get();
        }
    }

    protected void UpdateSpawnPosition()
    {
        spawnPosition = Helpers.GetWorldPosition(Constants.SPAWN_POSITION);
    }


    private void OnGet(PieceController piece)
    {

        piece.gameObject.SetActive(true);
        piece.transform.position = spawnPosition;
        piece.Reset();
    }

    private void OnRelease(PieceController piece)
    {
        piece.gameObject.SetActive(false);
    }

    protected abstract PieceController SpawnNewPiece();
}
