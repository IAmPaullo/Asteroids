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
        gameEvents.OnPlayerDamaged += ResetScoreOnDeath;
        gameEvents.OnPlayerDeath += OnPlayerDeath;
    }
    private void OnDisable()
    {
        if (gameEvents == null) return;
        gameEvents.OnAsteroidDestroyed -= AddScore;
        gameEvents.OnPlayerDamaged -= ResetScoreOnDeath;
        gameEvents.OnPlayerDeath -= OnPlayerDeath;
        ResetData();
    }

    private void AddScore(int points)
    {
        currentScore += points;
        Debug.Log($"Score updated: {currentScore}");
    }

    private void ResetScoreOnDeath()
    {
        //Debug.Log("Player damaged. Resetting score.");
        //currentScore = 0;
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
