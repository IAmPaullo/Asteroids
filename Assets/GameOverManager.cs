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
        foreach (Transform child in highscoreContainer)
        {
            if (child.GetComponent<TextMeshProUGUI>() != highScoresText) ///TODO remover o highscore do parent?? Desnecessario?
            {
                Destroy(child.gameObject);
            }
        }
        highScoresText.text = "High Scores";

        int highscoreLength = scoreData.highScores.Count;
        for (int i = 0; i < highscoreLength; i++)
        {
            var currentText = Instantiate(highscoreTextPrefab, highscoreContainer);
            var entry = scoreData.highScores[i];
            currentText.text = $"{i + 1}. {new string(entry.name)} - {entry.score}";
        }
    }

    private bool CheckForHighScore()
    {
        return scoreData.currentScore > scoreData.GetHighestScore();
    }

    public void SubmitHighScore()
    {
        string initials = initialsInputField.text;
        if (string.IsNullOrEmpty(initials) || initials.Length > 3)
        {
            Debug.LogWarning("Initials must be 1 to 3 characters long!");
            return;
        }
        char[] initialsArray = initials.ToUpper().ToCharArray();
        scoreData.AddHighScore(scoreData.currentScore, initialsArray);
        initialsInputContainer.SetActive(false);
        DisplayHighScores();
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
