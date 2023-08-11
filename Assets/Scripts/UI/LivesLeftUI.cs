using TMPro;
using UnityEngine;

public class LivesLeftUI : MonoBehaviour
{
    [SerializeField] private LevelConfigSO levelConfig;
    [SerializeField] private IntEventSO lifeLostEvent;
    [SerializeField] private TextMeshProUGUI livesLeftText;

    private void Awake()
    {
        livesLeftText.SetText(UITexts.LIVES_LEFT_TEXT + levelConfig.AvailableLives);
    }

    private void OnEnable()
    {
        lifeLostEvent.OnEventRaised += OnPieceDestroyed;
    }

    private void OnDisable()
    {
        lifeLostEvent.OnEventRaised += OnPieceDestroyed;
    }

    private void OnPieceDestroyed(int livesLeft)
    {
        livesLeftText.SetText(UITexts.LIVES_LEFT_TEXT + livesLeft);
    }
}
