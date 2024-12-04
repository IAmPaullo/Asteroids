using System.Text;
using TMPro;
using UnityEngine;

public class GameHUDManager : MonoBehaviour
{
    [Header("Score Data")]
    [SerializeField] private ScoreData scoreData;

    [Header("Game Events")]
    [SerializeField] private GameEvents gameEvents;

    [SerializeField] private RandomWordList wordList;

    [Header("UI Elements")]
    [SerializeField] private GameObject gameHUD;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI miscText;
    [SerializeField] private TextMeshProUGUI levelText;


    private void OnEnable()
    {
        gameEvents.OnLevelStart += UpdateHUD;
        gameEvents.OnLevelComplete += UpdateMiscText;
        gameEvents.OnGameOver += DisableHUD;
        gameEvents.HUDUpdate += UpdateHUD;
        UpdateHUD();
    }

    private void OnDisable()
    {
        gameEvents.OnLevelStart -= UpdateHUD;
        gameEvents.OnLevelComplete -= UpdateMiscText;
        gameEvents.OnGameOver -= DisableHUD;
        gameEvents.HUDUpdate -= UpdateHUD;
    }

    private void UpdateHUD()
    {
        scoreText.text = $"Score: {scoreData.currentScore}";
        levelText.text = $"Level: {scoreData.currentLevel}";
    }

    private void UpdateMiscText()
    {
        var newText = wordList.GenerateRandomSentence();
        var currentText = miscText.text;
        AnimationHandler.AnimateTextChange(miscText, currentText, newText, Color.green, Color.white, 0.55f);
    }

    private void DisableHUD()
    {
        gameHUD.SetActive(false);
    }

}
