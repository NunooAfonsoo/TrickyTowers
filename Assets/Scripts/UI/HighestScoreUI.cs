using TMPro;
using UnityEngine;

public class HighestScoreUI : MonoBehaviour
{
    [SerializeField] private IntEventSO newHighScoreEvent;

    private TextMeshProUGUI highScoreText;

    private void Awake()
    {
        highScoreText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        newHighScoreEvent.OnEventRaised += OnNewHighScore;
    }
    private void OnDisable()
    {
        newHighScoreEvent.OnEventRaised -= OnNewHighScore;
    }

    private void OnNewHighScore(int newHighScore)
    {
        highScoreText.SetText(UITexts.HIGH_SCORE_TEXT + newHighScore);
    }
}
