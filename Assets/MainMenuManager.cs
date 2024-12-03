using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject highScoresCanvas;
    [SerializeField] private GameObject controlsCanvas;
    [SerializeField] private GameObject soundToggleButton;
    [SerializeField] private TextMeshProUGUI soundButtonText;

    [Header("Game Settings")]
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private PlayerConfig playerConfig;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowHighScores()
    {
        highScoresCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void ShowControls()
    {
        controlsCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void ToggleSound()
    {
        playerConfig.SwitchAudio();
        soundButtonText.text = playerConfig.isSoundActivated ? "Sound: ON" : "Sound: OFF";
    }

    public void ReturnToMainMenu()
    {
        highScoresCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}
