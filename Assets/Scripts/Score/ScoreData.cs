using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "Game/ScoreData")]
public class ScoreData : ScriptableObject
{
    [Header("Current Score")]
    public int currentScore;

    [Header("High Scores")]
    public int[] highScores = new int[10];

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
    public void ResetCurrentScore()
    {
        currentScore = 0;
    }
}
