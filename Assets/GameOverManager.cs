using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverCanvas;
    [Header("HighScore Display")]
    [SerializeField] private TextMeshProUGUI highScoresText;
    [SerializeField] private TMP_InputField initialsInputField;
    [SerializeField] private GameObject initialsInputContainer;
    [SerializeField] private TextMeshProUGUI highscoreTextPrefab;

    [SerializeField] private Transform highscoreContainer;

    [Header("Dependencies")]
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private GameEvents gameEvents;

    private bool isHighScore = false;

    private void OnEnable()
    {
        gameEvents.OnGameOver += ShowGameOverScreen;
    }

    private void OnDisable()
    {
        gameEvents.OnGameOver -= ShowGameOverScreen;
    }
    private void ShowGameOverScreen()
    {
        if (gameOverCanvas == null) return;
        gameOverCanvas.SetActive(true);
    }

    private void Start()
    {
        DisplayHighScores();

        isHighScore = CheckForHighScore();

        if (isHighScore)
            initialsInputContainer.SetActive(true);
        else
            initialsInputContainer.SetActive(false);
    }

    private void DisplayHighScores()
    {
        highScoresText.text = "High Scores";

        int highscoreLength = scoreData.highScores.Length;
        for (int i = 0; i < highscoreLength; i++)
        {
            var currentText = Instantiate(highscoreTextPrefab, highscoreContainer);
            currentText.text = $"{i + 1}. {scoreData.highScores[i]}";
        }
    }

    private bool CheckForHighScore()
    {
        return scoreData.currentScore > scoreData.highScores[^1];
    }

    public void SubmitHighScore()
    {
        string initials = initialsInputField.text;

        if (string.IsNullOrEmpty(initials))
        {
            Debug.LogWarning("Initials cannot be empty!");
            return;
        }
        UpdateHighScores(initials, scoreData.currentScore);
        initialsInputContainer.SetActive(false);
        DisplayHighScores();
    }

    private void UpdateHighScores(string initials, int score)
    {
        for (int i = 0; i < scoreData.highScores.Length; i++)
        {
            if (score > scoreData.highScores[i])
            {
                for (int j = scoreData.highScores.Length - 1; j > i; j--)
                {
                    scoreData.highScores[j] = scoreData.highScores[j - 1];
                }
                scoreData.highScores[i] = score;
                Debug.Log($"New HighScore: {initials} - {score}");
                break;
            }
        }
    }

    public void RestartGame()
    {
        scoreData.ResetData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
