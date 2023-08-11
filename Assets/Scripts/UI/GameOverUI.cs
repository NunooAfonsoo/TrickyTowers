using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private StringEventSO gameOverEvent;

    [SerializeField] private GameObject background;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private GameObject buttonsParent;
    [SerializeField] private Button restart;
    [SerializeField] private Button goToMainMenu;

    private void Awake()
    {
        restart.onClick.AddListener(Restart);
        goToMainMenu.onClick.AddListener(GoToMainMenu);
    }

    private void OnEnable()
    {
        gameOverEvent.OnEventRaised += ShowGameOverScreen;
    }

    private void OnDisable()
    {
        gameOverEvent.OnEventRaised -= ShowGameOverScreen;
    }

    /// <summary>
    /// Shows game over screen
    /// </summary>
    private void ShowGameOverScreen(string text)
    {
        Time.timeScale = 0f;

        background.SetActive(true);
        gameOverText.SetText(text);
        gameOverText.gameObject.SetActive(true);
        buttonsParent.SetActive(true);
    }

    /// <summary>
    /// Loads Main Menu scene
    /// </summary>
    private void GoToMainMenu()
    {
        ScenesManager.LoadScene(SceneNames.MAIN_MENU);
    }

    /// <summary>
    /// Reloads current level
    /// </summary>
    private void Restart()
    {
        ScenesManager.RestartScene();
    }
}
