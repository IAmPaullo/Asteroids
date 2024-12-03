using UnityEngine;
using TMPro;

public class HighScoresScreen : MonoBehaviour
{
    [SerializeField] private GameObject highscorePanel;
    [SerializeField] private Transform highscoreContainer;
    [SerializeField] private TextMeshProUGUI highScoresTextPrefab;
    [SerializeField] private ScoreData scoreData;

    public void DisplayHighScores()
    {
        highscorePanel.SetActive(true);
        foreach (Transform child in highscoreContainer)
        {
            Destroy(child.gameObject);
        }

        int highscoreLength = scoreData.highScores.Count;
        for (int i = 0; i < highscoreLength; i++)
        {
            var currentText = Instantiate(highScoresTextPrefab, highscoreContainer);
            currentText.text = $"{i + 1}. {scoreData.highScores[i]}";
            var entry = scoreData.highScores[i];
            currentText.text = $"{i + 1}. {new string(entry.name)} - {entry.score}";
        }
    }
}