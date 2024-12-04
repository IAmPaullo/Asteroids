using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighScoresScreen : MonoBehaviour
{
    [SerializeField] private Button highScoreButton;
    [SerializeField] private GameObject highscorePanel;
    [SerializeField] private Transform highscoreContainer;
    [SerializeField] private TextMeshProUGUI highScoresTextPrefab;
    [SerializeField] private ScoreData scoreData;


    private void Start()
    {
        highScoreButton.onClick.AddListener(DisplayHighScores);
    }
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