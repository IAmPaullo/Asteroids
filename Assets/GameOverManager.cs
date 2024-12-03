using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameOverManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverCanvas;

    [Header("Dependencies")]
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private GameEvents gameEvents;

    private bool isHighScore = false;

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

    private void Start()
    {
        isHighScore = CheckForHighScore();
        if (isHighScore)
            scoreData.AddHighScore(scoreData.currentScore, scoreData.playerName.ToCharArray());
    }
    private bool CheckForHighScore()
    {
        return scoreData.currentScore > scoreData.GetHighestScore();
    }

    public void RestartGame()
    {
        scoreData.ResetData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
