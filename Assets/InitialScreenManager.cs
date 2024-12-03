using TMPro;
using UnityEngine;

public class InitialsScreenManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject initialsScreenCanvas;
    [SerializeField] private TMP_InputField initialsInputField;
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private ScoreData scoreData;

    private void Start()
    {
        initialsScreenCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void ConfirmInitials()
    {
        string initials = initialsInputField.text.ToUpper();

        if (string.IsNullOrEmpty(initials) || initials.Length > 3)
        {
            Debug.LogWarning("Initials must be 1 to 3 characters long!");
            return;
        }

        scoreData.AddPlayer(initials);

        initialsScreenCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}