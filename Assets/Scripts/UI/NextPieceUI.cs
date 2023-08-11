using UnityEngine;
using UnityEngine.UI;

public class NextPieceUI : MonoBehaviour
{
    [SerializeField] private LevelConfigSO levelConfig;
    [SerializeField] private SpriteEventSO spawnNewPieceEvent;
    [SerializeField] private Image nextPiece;

    private void Awake()
    {
        if (!levelConfig.CanSeeNextPiece)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        spawnNewPieceEvent.OnEventRaised += OnNewPieceSpawned;
    }

    private void OnDisable()
    {
        spawnNewPieceEvent.OnEventRaised -= OnNewPieceSpawned;
    }

    public void OnNewPieceSpawned(Sprite nextPieceSprite)
    {
        nextPiece.sprite = nextPieceSprite;
    }
}
