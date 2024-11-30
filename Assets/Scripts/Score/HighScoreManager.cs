using TMPro;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    [Header("Score Data")]
    [SerializeField] private ScoreData scoreData;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI highScoresText;

    private void Start()
    {
        DisplayHighScores();
    }

    private void DisplayHighScores()
    {
        highScoresText.text = "High Scores:\n";
        int len = highScoresText.text.Length;
        for (int i = 0; i < len; i++)
        {
            highScoresText.text += $"{i + 1}. {scoreData.highScores[i]}\n";
        }
    }
}
