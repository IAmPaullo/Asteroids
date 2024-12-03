using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreData scoreData;
    [Header("Game Settings")]
    private int playerLives;
    [SerializeField] private GameEvents gameEvents;
    public int PlayerLives => playerLives;
    public GameEvents GameEvents => gameEvents;

    private void OnEnable()
    {
        gameEvents.OnPlayerDamaged += ProcessPlayerDamage;
        gameEvents.OnAsteroidDestroyed += AddScore;
    }

    private void OnDisable()
    {
        gameEvents.OnPlayerDamaged -= ProcessPlayerDamage;
        gameEvents.OnAsteroidDestroyed -= AddScore;
    }
    private void Start()
    {
        scoreData.ResetData();
        playerLives = scoreData.startingLivesAmount;
        UpdateHUD();
    }
    public void AddScore(int points)
    {
        scoreData.currentScore += points;
        UpdateHUD();
    }
    public void NextLevel()
    {
        scoreData.currentLevel++;
        UpdateHUD();
    }
    private void ProcessPlayerDamage()
    {
        playerLives--;
        Action processedAction = playerLives <= 0 ? GameOver : UpdateHUD;

        processedAction?.Invoke();
    }

    private void UpdateHUD()
    {
        gameEvents?.HUDUpdated();
    }
    private void GameOver()
    {
        Debug.Log("Game Over :( ");
        char[] name = { 'P', 'B', 'F' };
        scoreData.AddHighScore(scoreData.currentScore, name);
        gameEvents.GameOver();
    }
}
