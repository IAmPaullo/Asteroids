using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class MainMenuEvent : MonoBehaviour
{
    private UIDocument document;
    private Button button;
    private List<Button> menuButtons = new();
    private AudioSource audioSource;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        audioSource = GetComponent<AudioSource>();

        button = document.rootVisualElement.Q("StartGameButton") as Button;

        menuButtons = document.rootVisualElement.Query<Button>().ToList();

        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }


    }

    private void OnDisable()
    {
        button.UnregisterCallback<ClickEvent>(OnPlayGameClicked);
    }

    private void OnPlayGameClicked(ClickEvent evt)
    {
        Debug.Log("Loaded");
    }
    private void OnAllButtonsClick(ClickEvent evt)
    {
        audioSource.Play();
    }
}
