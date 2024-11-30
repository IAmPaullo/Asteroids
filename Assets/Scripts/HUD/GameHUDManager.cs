using TMPro;
using UnityEngine;

public class GameHUDManager : MonoBehaviour
{
    [Header("Score Data")]
    [SerializeField] private ScoreData scoreData;

    [Header("Game Events")]
    [SerializeField] private GameEvents gameEvents;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI levelText;


    [Header("Game Settings")]
    [SerializeField] private int playerLives = 3;
    private int currentLevel = 1;

    private void OnEnable()
    {
        if (gameEvents == null) return;
        gameEvents.OnAsteroidDestroyed += AddScore;
        gameEvents.OnPlayerDamaged += RemoveLife;
        gameEvents.OnLevelUp += NextLevel;
    }
    private void OnDisable()
    {
        if (gameEvents == null) return;
        gameEvents.OnAsteroidDestroyed -= AddScore;
        gameEvents.OnPlayerDamaged -= RemoveLife;
        gameEvents.OnLevelUp -= NextLevel;
    }
    private void Start()
    {
        scoreData.ResetCurrentScore();
        UpdateHUD();
    }

    public void AddScore(int points)
    {
        scoreData.currentScore += points;
        UpdateHUD();
    }

    public void RemoveLife()
    {
        playerLives--;
        UpdateHUD();

        if (playerLives <= 0)
        {
            GameOver();
        }
    }

    public void NextLevel()
    {
        currentLevel++;
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        scoreText.text = "Score: " + scoreData.currentScore;
        livesText.text = "Lives: " + playerLives;
        levelText.text = "Level: " + currentLevel;
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        scoreData.AddHighScore(scoreData.currentScore);
    }
}
