using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private Button pause;
    [SerializeField] private Button resume;
    [SerializeField] private Button goToMainMenu;
    [SerializeField] private Button restart;

    private void Awake()
    {
        pause.onClick.AddListener(Pause);
        resume.onClick.AddListener(Resume);
        goToMainMenu.onClick.AddListener(GoToMainMenu);
        restart.onClick.AddListener(Restart);
    }

    /// <summary>
    /// Pauses the game
    /// </summary>
    private void Pause()
    {
        Time.timeScale = 0f;

        background.SetActive(true);
        pause.gameObject.SetActive(false);
        resume.gameObject.SetActive(true);
        goToMainMenu.gameObject.SetActive(true);
        restart.gameObject.SetActive(true);
    }

    /// <summary>
    /// Resumes the game
    /// </summary>
    private void Resume()
    {
        Time.timeScale = 1f;

        background.SetActive(false);
        pause.gameObject.SetActive(true);
        resume.gameObject.SetActive(false);
        goToMainMenu.gameObject.SetActive(false);
        restart.gameObject.SetActive(false);
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
