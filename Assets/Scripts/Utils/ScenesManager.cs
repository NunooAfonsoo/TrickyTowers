using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScenesManager
{
    public static void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(sceneName);
    }

    public static void RestartScene()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}