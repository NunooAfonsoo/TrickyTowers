using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private LevelConfigSO levelConfig;

    [SerializeField] private StringEventSO gameOverEvent;
    [SerializeField] private IntEventSO newHighScoreEvent;
    [SerializeField] private IntEventSO lifeLostEvent;
    [SerializeField] private VoidEventSO piecePlacedEvent;
    [SerializeField] private BoolEventSO pieceDestroyedEvent;

    [SerializeField] private Transform winningHeightSprite;
    [SerializeField] private Transform activePiecePath;
    [SerializeField] private Transform startingGroundCheck;
    [SerializeField] private LayerMask landedPiecesLayer;

    private NormalPieceController activePiece;
    private int highScore;
    private int lives;

    protected override void Awake()
    {
        base.Awake();

        SetupLevelConfig();
        lifeLostEvent.RaiseEvent(lives);
    }

    private void SetupLevelConfig()
    {
        levelConfig.SetupLevelConfig();

        highScore = 0;
        lives = levelConfig.AvailableLives;
        winningHeightSprite.localPosition = new Vector3(0, levelConfig.LevelWinningHeight, 0);

        if (!levelConfig.CanSeePiecePath)
        {
            activePiecePath.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        piecePlacedEvent.OnEventRaised += OnPiecePlaced;
        pieceDestroyedEvent.OnEventRaised += OnPieceDestroyed;
    }

    private void OnDisable()
    {
        piecePlacedEvent.OnEventRaised -= OnPiecePlaced;
        pieceDestroyedEvent.OnEventRaised -= OnPieceDestroyed;
    }

    private void LateUpdate()
    {
        if (activePiece == null)
        {
            return;
        }

        activePiecePath.position = activePiece.transform.position;

        switch (activePiece.transform.rotation.eulerAngles.z)
        {
            case 0:
            case 180:
                activePiecePath.localScale = new Vector3(activePiece.Dimensions.x, Constants.PIECES_PATH_HEIGHT);
                break;
            case 90:
            case 270:
                activePiecePath.localScale = new Vector3(activePiece.Dimensions.y, Constants.PIECES_PATH_HEIGHT);
                break;
        }
    }

    public void OnPiecePlaced()
    {
        CalculateNewTowerHeight();
    }

    /// <summary>
    /// Calculates the new tower height based on ray hits
    /// </summary>
    private void CalculateNewTowerHeight()
    {
        Vector3 ground = startingGroundCheck.position;

        for (int currentScoreStep = 1; currentScoreStep <= levelConfig.LevelWinningHeight; currentScoreStep++)
        {
            RaycastHit2D ray = Physics2D.Raycast(ground, Vector2.right, 20, landedPiecesLayer);

            if (ray.rigidbody != null)
            {
                if (currentScoreStep > highScore)
                {
                    highScore = currentScoreStep;
                    newHighScoreEvent.RaiseEvent(highScore);

                    if (highScore >= levelConfig.LevelWinningHeight)
                    {
                        GameWon();
                        break;
                    }
                }
            }
            else
            {
                break;
            }

            ground += Vector3.up;
        }
    }

    /// <summary>
    /// Raises event for game won 
    /// </summary>
    private void GameWon()
    {
        gameOverEvent.RaiseEvent(levelConfig.WinningMessage);
    }

    public void OnPieceDestroyed(bool spawnNewPiece)
    {
        LifeLost();
    }

    private void LifeLost()
    {
        lives--;
        lifeLostEvent.RaiseEvent(lives);

        if (lives == 0)
        {
            GameLost();
        }
    }

    /// <summary>
    /// Raises event for game lost 
    /// </summary>
    private void GameLost()
    {
        gameOverEvent.RaiseEvent(levelConfig.LosingMessage);
    }

    public void SetActivePiece(NormalPieceController piece)
    {
        activePiece = piece;
    }
}
