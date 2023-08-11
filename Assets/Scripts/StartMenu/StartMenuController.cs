using UnityEngine;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    [SerializeField] private Button singlePlayer;
    [SerializeField] private Button aiMode;
    [SerializeField] private Button exit;

    private void Awake()
    {
        singlePlayer.onClick.AddListener(PLaySinglePLayer);
        aiMode.onClick.AddListener(PlayAIMode);
        exit.onClick.AddListener(ExitGame);
    }

    private void PLaySinglePLayer()
    {
        ScenesManager.LoadScene(SceneNames.SINGLE_PLAYER_SCENE);
    }

    private void PlayAIMode()
    {
        ScenesManager.LoadScene(SceneNames.AI_MODE_SCENE);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
