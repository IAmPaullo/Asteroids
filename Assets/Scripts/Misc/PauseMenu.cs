using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenuCanvas; 
    [SerializeField] private Button resumeButton; 
    [SerializeField] private Button mainMenuButton; 
    [SerializeField] private Button audioButton;
    [SerializeField] private Slider volumeSlider;

    [SerializeField] private PlayerConfig playerConfig;

    private bool isPaused = false; 

    private void Start()
    {
        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        audioButton.onClick.AddListener(GoToMainMenu);
        ChangeAudioVolumeSlider();
        pauseMenuCanvas.SetActive(false);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)) // new input
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = .15f;
        pauseMenuCanvas.SetActive(true); 

    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);
    }
    private void SwitchAudio()
    {
        playerConfig.SwitchAudio();
    }
    private void ChangeAudioVolumeSlider()
    {
        volumeSlider.value = playerConfig.gameAudioVolume;
        volumeSlider.onValueChanged.AddListener(playerConfig.SetAudioVolume);
    }
    private void GoToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(playerConfig.MainMenuScene);
    }
}