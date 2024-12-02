using TMPro;
using UnityEngine;

public class GameHUDManager : MonoBehaviour
{
    [Header("Score Data")]
    [SerializeField] private ScoreData scoreData;

    [Header("Game Events")]
    [SerializeField] private GameEvents gameEvents;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI levelText;

    private void OnEnable()
    {
        gameEvents.OnLevelStart += UpdateHUD;
        gameEvents.HUDUpdate += UpdateHUD;
        UpdateHUD();
    }

    private void OnDisable()
    {
        gameEvents.OnLevelStart -= UpdateHUD;
        gameEvents.HUDUpdate -= UpdateHUD;
    }

    private void UpdateHUD()
    {
        scoreText.text = $"Score: {scoreData.currentScore}";
        livesText.text = $"Lives: {scoreData.currentLives}";
        levelText.text = $"Level: {scoreData.currentLevel}";
    }

}
