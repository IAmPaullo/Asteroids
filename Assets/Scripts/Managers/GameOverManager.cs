using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverCanvas;

    [Header("Dependencies")]
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private PlayerConfig playerConfig;

    private void OnEnable()
    {
        gameEvents.OnGameOver += ShowGameOverScreen;
    }

    private void OnDisable()
    {
        gameEvents.OnGameOver -= ShowGameOverScreen;
    }
    private void ShowGameOverScreen()
    {
        if (gameOverCanvas == null) return;
        gameOverCanvas.SetActive(true);
    }
    public void RestartGame()
    {
        scoreData.ResetData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(playerConfig.MainMenuScene);
    }
}
