using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "ScriptableObjects/LevelConfigSO")]
public class LevelConfigSO : BaseScriptableObject
{
    [TextArea]
    public string WinningMessage;
    [TextArea]
    public string LosingMessage;
    public int LevelWinningHeight;
    public int AvailableLives;
    public Vector2 Gravity;
    public bool CanSeeNextPiece;
    public bool CanSeePiecePath;

    [Header("Pieces Config")]
    public float PiecesXSpeed;
    public float PiecesYSpeed;
    public float PiecesDrag;
    public float PiecesAngularDrag;
    public float PiecesGravityScale;

    public PieceConfigSO[] PiecesSO;

    public void SetupLevelConfig()
    {
        Physics2D.gravity = Gravity;

        foreach (PieceConfigSO pieceSO in PiecesSO)
        {
            pieceSO.SetupValues(PiecesXSpeed, PiecesYSpeed, PiecesDrag, PiecesAngularDrag, PiecesGravityScale);
        }
    }

    #if UNITY_EDITOR
    private void OnValidate()
    {
        SetupLevelConfig();
    }
    #endif
}