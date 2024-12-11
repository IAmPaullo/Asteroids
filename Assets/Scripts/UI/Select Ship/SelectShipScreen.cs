using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectShipScreen : MonoBehaviour, IMenuScreen
{
    private UIDocument document;
    private Button button;
    private List<Button> menuButtons = new();
    private AudioSource audioSource;
    [SerializeField] private List<Ship> availableShips = new();





    private void Awake()
    {
        document = GetComponent<UIDocument>();
        audioSource = GetComponent<AudioSource>();

        button = document.rootVisualElement.Q("Confirm") as Button;

        menuButtons = document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    public void OnAllButtonsClick(ClickEvent evt)
    {

    }
}
public interface IMenuScreen
{
    void OnAllButtonsClick(ClickEvent evt);
}