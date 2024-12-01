using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Game Events")]
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private ScoreData scoreData;

    private int remainingAsteroids;

    private void OnEnable()
    {
        gameEvents.OnLevelComplete += StartLevel;
        gameEvents.OnAsteroidDestroyed += HandleAsteroidDestroyed;
    }

    private void OnDisable()
    {
        gameEvents.OnLevelComplete -= StartLevel;
        gameEvents.OnAsteroidDestroyed -= HandleAsteroidDestroyed;
    }

    private void Start()
    {
        StartLevel();
    }

    private void StartLevel()
    {
        remainingAsteroids = CalculateAsteroidsForLevel();

        Debug.Log($"Starting Level with {remainingAsteroids} asteroids.");
        gameEvents.LevelStart();
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
        int baseAsteroids = 4; 
        return baseAsteroids + (scoreData.currentLevel - 1); 
    }
}
