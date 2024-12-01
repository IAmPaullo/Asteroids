using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "Game/ScoreData")]
public class ScoreData : ScriptableObject
{

    [Header("Current Score")]
    public int currentScore;

    [Header("High Scores")]
    public int[] highScores = new int[10];

    [Header("Dependencies")]
    public GameEvents gameEvents;

    [Header("Lives")]
    [SerializeField] private int startingLivesAmount;
    public int playerLives;

    [Header("Current Level")]
    public int currentLevel;

    private void OnEnable()
    {
        if (gameEvents == null) return;
        gameEvents.OnAsteroidDestroyed += AddScore;
        gameEvents.OnPlayerDamaged += ResetScoreOnDeath;
    }
    private void OnDisable()
    {
        if (gameEvents == null) return;
        gameEvents.OnAsteroidDestroyed -= AddScore;
        gameEvents.OnPlayerDamaged -= ResetScoreOnDeath;
    }

    private void AddScore(int points)
    {
        currentScore += points;
        Debug.Log($"Score updated: {currentScore}");
    }

    private void ResetScoreOnDeath()
    {
        Debug.Log("Player damaged. Resetting score.");
        currentScore = 0;
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
    public void NextLevel()
    {
        currentLevel++;
    }
    public void Reset()
    {
        currentScore = 0;
        playerLives = startingLivesAmount;
        currentLevel = 1;
    }
}
