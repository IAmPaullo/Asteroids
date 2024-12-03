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
    private bool isSoundOn = true; 

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
        isSoundOn = !isSoundOn;
        soundButtonText.text = isSoundOn ? "Sound: ON" : "Sound: OFF";

        AudioListener.volume = isSoundOn ? 1f : 0f;
    }

    public void ReturnToMainMenu()
    {
        highScoresCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}
