using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [Header("Black Hole Settings")]
    [SerializeField] private BlackHole blackHole;

    [Header("Spawn Settings")]
    [SerializeField, Range(0f, 15f)] private float asteroidSpawnInterval = 15f;
    [SerializeField] private float asteroidSpawnAreaWidth = 10f;
    [SerializeField] private float asteroidSpawnAreaHeight = 10f;

    [SerializeField] private ScoreData scoreData;
    [SerializeField] private GameEvents gameEvents;
    private int playerLives;
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
        StartCoroutine(SpawnBlackHolePeriodically());
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

    private IEnumerator SpawnBlackHolePeriodically()
    {
        while (true)
        {
            SpawnBlackHole();
            yield return new WaitForSeconds(asteroidSpawnInterval);
        }
    }
    private void SpawnBlackHole()
    {
        Vector3 randomSpawnPosition = new Vector3(
            UnityEngine.Random.Range(-asteroidSpawnAreaWidth / 2f, asteroidSpawnAreaWidth / 2f),
            UnityEngine.Random.Range(-asteroidSpawnAreaHeight / 2f, asteroidSpawnAreaHeight / 2f),
            0f
        );
        blackHole.Initialize(randomSpawnPosition);
    }


}
