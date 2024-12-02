using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "Game/ScoreData")]
public class ScoreData : ScriptableObject
{


    [SerializeField] private int startingLivesAmount;

    [Header("High Scores")]
    public int[] highScores = new int[10];

    [Header("Lives")]
    public int currentLives;

    [Header("Current Score")]
    public int currentScore;

    [Header("Current Level")]
    public int currentLevel;

    [Header("Dependencies")]
    public GameEvents gameEvents;

    private void OnEnable()
    {
        if (gameEvents == null) return;
        gameEvents.OnAsteroidDestroyed += AddScore;
        gameEvents.OnPlayerDamaged += OnPlayerDamaged;
        gameEvents.OnPlayerDeath += OnPlayerDeath;
    }
    private void OnDisable()
    {
        if (gameEvents == null) return;
        gameEvents.OnAsteroidDestroyed -= AddScore;
        gameEvents.OnPlayerDamaged -= OnPlayerDamaged;
        gameEvents.OnPlayerDeath -= OnPlayerDeath;
        ResetData();
    }

    private void AddScore(int points)
    {
        currentScore += points;
        Debug.Log($"Score updated: {currentScore}");
    }
    private void OnPlayerDamaged()
    {
        currentLives--;
        Debug.LogWarning($"Lives updated: {currentLives}");
        if (currentLives <= 0)
        {
            Debug.LogWarning("Player is dead!");
            gameEvents.PlayerDeath();
        }
    }
    public void AddHighScore(int score)
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            if (score > highScores[i])
            {
                for (int j = highScores.Length - 1; j > i; j--)
                {
                    highScores[j] = highScores[j - 1];
                }
                highScores[i] = score;
                break;
            }
        }
    }
    public void OnPlayerDeath()
    {
        //throw new NotImplementedException();
    }
    public void NextLevel()
    {
        currentLevel++;
        gameEvents.HUDUpdated();
    }
    public void ResetData()
    {
        currentScore = 0;
        currentLives = startingLivesAmount;
        currentLevel = 1;
    }
}
