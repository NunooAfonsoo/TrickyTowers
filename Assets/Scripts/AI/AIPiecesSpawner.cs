using UnityEngine;

public class AIPiecesSpawner : Spawner
{
    protected override void Awake()
    {
        base.Awake();

        UpdateSpawnPosition();

        spawnPosition = spawnerLocation.position;
    }

    private void Start()
    {
        piecesPool.Get();
    }

    protected override PieceController SpawnNewPiece()
    {
        PieceController piece = Instantiate(piecesSOs[Random.Range(0, piecesSOs.Length)].prefab, spawnPosition, Quaternion.identity, piecesContainer).GetComponent<PieceController>();
        piece.SetPool(piecesPool);

        return piece;
    }
}
