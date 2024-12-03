using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "Game/ScoreData")]
public class ScoreData : ScriptableObject
{
    [Header("High Scores")]
    public List<HighScoreEntry> highScores = new(5);


    [Header("Name")]
    public string playerName;

    [Header("Lives")]
    public int startingLivesAmount;
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
    public void AddPlayer(string playerName)
    {
        this.playerName = playerName;
        Debug.Log($"Added player: {playerName}");
    }

    public void AddHighScore(int score, char[] name)
    {
        HighScoreEntry newEntry = new HighScoreEntry { name = name, score = score };
        highScores.Add(newEntry);


        SortHighScores();
    }

    public int GetHighestScore()
    {
        if (highScores.Count == 0) return 0;
        return highScores[0].score;
    }

    public int GetLowestScore()
    {
        if (highScores.Count == 0) return 0;
        return highScores[highScores.Count - 1].score;
    }

    public void SortHighScores()
    {

        highScores.Sort((a, b) => b.score.CompareTo(a.score));
    }

    public void OnPlayerDeath()
    {
        //
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
