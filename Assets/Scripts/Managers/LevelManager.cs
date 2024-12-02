using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Game Events")]
    [SerializeField] private int baseAsteroids = 3;
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private ScoreData scoreData;

    private int remainingAsteroids;

    private void OnEnable()
    {
        gameEvents.OnLevelComplete += StartLevel;
        gameEvents.OnAsteroidDestroyed += HandleAsteroidDestroyed;
        gameEvents.OnAddAsteroids += AddAsteroids;
    }

    private void OnDisable()
    {
        gameEvents.OnLevelComplete -= StartLevel;
        gameEvents.OnAsteroidDestroyed -= HandleAsteroidDestroyed;
        gameEvents.OnAddAsteroids -= AddAsteroids;
    }

    private void Start()
    {
        StartLevel();
    }

    private void StartLevel()
    {
        remainingAsteroids = CalculateAsteroidsForLevel();

        Debug.Log($"Starting Level with {remainingAsteroids} asteroids.");
        gameEvents.SetAsteroidsToSpawn(remainingAsteroids);
        gameEvents.LevelStart();
    }

    public void AddAsteroids(int count)
    {
        remainingAsteroids += count;
    }
    private void HandleAsteroidDestroyed(int _)//ISSUE repurpose asteroid destroyed event? >spaghetti<
    {
        remainingAsteroids--;

        Debug.Log($"Asteroid destroyed! Remaining: {remainingAsteroids}");

        if (remainingAsteroids <= 0)
        {
            scoreData.NextLevel();
            gameEvents.LevelComplete();
        }
    }

    private int CalculateAsteroidsForLevel()
    {
        return baseAsteroids + (scoreData.currentLevel - 1);
    }
}
