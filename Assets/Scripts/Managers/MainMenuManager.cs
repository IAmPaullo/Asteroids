using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject highScoresCanvas;
    [SerializeField] private GameObject controlsCanvas;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button highScoresButton;
    [SerializeField] private Button soundToggleButton;
    [SerializeField] private Sprite soundOnImage;
    [SerializeField] private Sprite soundOffImage;
    [SerializeField] private Button controlsButton;

    [Header("Game Settings")]
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private PlayerConfig playerConfig;


    public void Start()
    {
        playButton.onClick.AddListener(StartGame);
        highScoresButton.onClick.AddListener(ShowHighScores);
        soundToggleButton.onClick.AddListener(ToggleSound);
        controlsButton.onClick.AddListener(ShowControls);
        soundToggleButton.GetComponent<Image>().sprite = playerConfig.isSoundActivated ? soundOnImage : soundOffImage;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(playerConfig.GameScene);
    }

    public void ShowHighScores()
    {
        highScoresCanvas.SetActive(true);
        //mainMenuCanvas.SetActive(false);
    }

    public void ShowControls()
    {
        controlsCanvas.SetActive(true);
        //mainMenuCanvas.SetActive(false);
    }

    public void ToggleSound()
    {
        var buttonImage = soundToggleButton.GetComponent<Image>();
        buttonImage.sprite = playerConfig.isSoundActivated ? soundOffImage : soundOnImage;
        playerConfig.SwitchAudio();
    }

    public void ReturnToMainMenu()
    {
        highScoresCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}
