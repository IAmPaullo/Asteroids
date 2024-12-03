using System.Text;
using TMPro;
using UnityEngine;

public class GameHUDManager : MonoBehaviour
{
    [Header("Score Data")]
    [SerializeField] private ScoreData scoreData;

    [Header("Game Events")]
    [SerializeField] private GameEvents gameEvents;

    [Header("UI Elements")]
    [SerializeField] private GameObject gameHUD;
    [SerializeField] private TextMeshProUGUI scoreText;
    //[SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI levelText;

    private void OnEnable()
    {
        gameEvents.OnLevelStart += UpdateHUD;
        gameEvents.OnGameOver += DisableHUD;
        gameEvents.HUDUpdate += UpdateHUD;
        UpdateHUD();
    }

    private void OnDisable()
    {
        gameEvents.OnLevelStart -= UpdateHUD;
        gameEvents.OnGameOver -= DisableHUD;
        gameEvents.HUDUpdate -= UpdateHUD;
    }

    private void UpdateHUD()
    {
        scoreText.text = $"Score: {scoreData.currentScore}";
        levelText.text = $"Level: {scoreData.currentLevel}";
    }

    private void UpdateLives()
    {
        //int amount = scoreData.currentLives;
        //StringBuilder sb = new();
        //for (int i = 0; i < amount; i++)
        //{
        //    sb.Append("♥");
        //}
        ////livesText.text = $"Lives: {sb}";
    }

    private void DisableHUD()
    {
        gameHUD.SetActive(false);
    }

}
