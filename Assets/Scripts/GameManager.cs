using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreData scoreData;
    [Header("Game Settings")]
    [SerializeField] private int playerLives = 3;
    [SerializeField] private GameEvents gameEvents;
    public int PlayerLives => playerLives;
    public GameEvents GameEvents => gameEvents;

    private void OnEnable()
    {
        gameEvents.OnPlayerDamaged += ProcessPlayerDamage;
    }

    private void OnDisable()
    {
        gameEvents.OnPlayerDamaged -= ProcessPlayerDamage;
    }
    private void Start()
    {
        scoreData.Reset();
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

        //if (playerLives <= 0)
        //    GameOver();
        //else
        //    UpdateHUD();
    }

    private void UpdateHUD()
    {
        gameEvents?.HUDUpdated();
    }
    private void GameOver()
    {
        Debug.Log("Game Over :( ");
    }
}
