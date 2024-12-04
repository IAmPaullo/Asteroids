using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class AnimationHandler : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float cameraShakeIntensity = .4f;
    [SerializeField] private float cameraShakeDuration = .4f;
    [SerializeField] private float pulseDuration = 0.5f;


    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Color[] backgroundColors;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI miscText;
    [SerializeField] private Image lowLifePanel;


    [Header("Game Events")]
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private ScoreData scoreData;

    private void OnEnable()
    {
        gameEvents.OnPlayerDamaged += HandlePlayerDamage;
        gameEvents.OnLevelComplete += HandleLevelComplete;
        gameEvents.OnAsteroidDestroyed += HandleAsteroidDestroyed;
    }

    private void OnDisable()
    {
        gameEvents.OnPlayerDamaged -= HandlePlayerDamage;
        gameEvents.OnLevelComplete -= HandleLevelComplete;
        gameEvents.OnAsteroidDestroyed -= HandleAsteroidDestroyed;
    }

    private void HandlePlayerDamage()
    {
        ShakeCamera();
        if (scoreData.currentLives < 2)
        {
            PulsateLifeLowPanel();
        }
    }

    private void HandleLevelComplete()
    {
        AnimateTextChange(levelText, scoreData.currentLevel - 1, scoreData.currentLevel,
            Color.green, Color.white, 0.75f, "Level: ");
        AnimateBackgroundColorChange(mainCamera.backgroundColor, backgroundColors[Random.Range(0, backgroundColors.Length)], .33f);
    }


    private void HandleAsteroidDestroyed(int points)
    {
        AnimateTextChange(scoreText, scoreData.currentScore - points, scoreData.currentScore,
            Color.green, Color.white, 0.75f, "Score: ");
    }

    private void ShakeCamera()
    {
        if (mainCamera != null)
        {
            mainCamera.transform.DOShakePosition(cameraShakeDuration, cameraShakeIntensity);
            Debug.Log("Camera shake triggered!");
        }
    }

    private void PulsateLifeLowPanel()
    {
        if (lowLifePanel != null)
        {
            lowLifePanel.DOFade(0.75f, pulseDuration).SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void AnimateBackgroundColorChange(Color fromColor, Color toColor, float duration)
    {
        mainCamera.backgroundColor = fromColor;

        DOTween.To(() => mainCamera.backgroundColor,
                   x => mainCamera.backgroundColor = x,
                   toColor,
                   duration)
               .SetEase(Ease.InOutSine);
    }

    public static void AnimateTextChange(TextMeshProUGUI textElement, int startValue, int endValue, Color changeColor, Color defaultColor, float duration, string prefix = "")
    {
        DOTween.Kill(textElement);
        textElement.DOColor(changeColor, duration / 2).OnComplete(() =>
        {
            textElement.DOColor(defaultColor, duration / 2);
        });

        DOTween.To(() => startValue, x =>
        {
            textElement.text = $"{prefix}{x}";
        }, endValue, duration);
    }
    public static void AnimateTextChange(TextMeshProUGUI textElement, string startValue, string endValue, Color changeColor, Color defaultColor, float duration)
    {
        DOTween.Kill(textElement);

        textElement.DOColor(changeColor, duration / 2).OnComplete(() =>
        {
            textElement.DOColor(defaultColor, duration / 2);
        });

        DOTween.To(() => startValue, x =>
        {
            textElement.text = x;
        }, endValue, duration);
    }
}
