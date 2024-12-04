using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [Header("Black Hole Settings")]
    private Transform player;
    [SerializeField] private BlackHole blackHole;

    [Header("Spawn Settings")]
    [SerializeField, Range(0f, 15f)] private float asteroidSpawnInterval = 15f;
    [SerializeField] private float asteroidSpawnAreaWidth = 10f;
    [SerializeField] private float asteroidSpawnAreaHeight = 10f;

    [Header("Enemy Ship Settings")]
    [SerializeField] private GameObject enemyShipPrefab;
    [SerializeField] private float spawnCooldown = 2f;
    private float nextSpawnTime;

    [SerializeField] private ScoreData scoreData;
    [SerializeField] private GameEvents gameEvents;
    private int playerLives;
    public int PlayerLives => playerLives;
    public GameEvents GameEvents => gameEvents;


    private void OnEnable()
    {
        gameEvents.OnPlayerDamaged += ProcessPlayerDamage;
        gameEvents.OnAsteroidDestroyed += AddScore;
        gameEvents.OnLevelComplete += HandleLevelComplete;
    }

    private void OnDisable()
    {
        gameEvents.OnPlayerDamaged -= ProcessPlayerDamage;
        gameEvents.OnAsteroidDestroyed -= AddScore;
        gameEvents.OnLevelComplete -= HandleLevelComplete;

    }
    private void Start()
    {
        scoreData.ResetData();
        playerLives = scoreData.startingLivesAmount;
        UpdateHUD();
        StartCoroutine(SpawnBlackHolePeriodically());
        player = GameObject.FindWithTag("Player")?.transform;
        Time.timeScale = 1f;
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
    private void HandleLevelComplete()
    {
        if (scoreData.currentLevel % 2 == 0)
        {
            SpawnEnemyShip();
        }
    }
    private void SpawnEnemyShip()
    {
        if (Time.time >= nextSpawnTime)
        {
            Vector3 spawnPosition = new(UnityEngine.Random.Range(-8f, 8f), UnityEngine.Random.Range(-4f, 4f), 0f);
            var enemy = Instantiate(enemyShipPrefab, spawnPosition, Quaternion.identity);
            gameEvents.SpawnEnemyShip();
            enemy.GetComponent<EnemyShipController>().Initialize(player.transform);
            nextSpawnTime = Time.time + spawnCooldown;
        }
    }
    private void UpdateHUD()
    {
        gameEvents?.HUDUpdated();
    }
    private void GameOver()
    {
        Debug.Log("Game Over :( ");
        if (CheckForHighScore())
        {
            char[] placeHolder = { 'P', 'B', 'F' };
            char[] name = scoreData.playerName == string.Empty ? placeHolder : scoreData.playerName.ToCharArray();
            scoreData.AddHighScore(scoreData.currentScore, name);
        }
        gameEvents.GameOver();
    }
    private bool CheckForHighScore()
    {
        return scoreData.currentScore > scoreData.GetHighestScore();
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
        Vector3 randomSpawnPosition = new(
            UnityEngine.Random.Range(-asteroidSpawnAreaWidth / 2f, asteroidSpawnAreaWidth / 2f),
            UnityEngine.Random.Range(-asteroidSpawnAreaHeight / 2f, asteroidSpawnAreaHeight / 2f),
            0f
        );
        gameEvents.SpawnBlackHole();
        blackHole.Initialize(randomSpawnPosition);
    }
}
