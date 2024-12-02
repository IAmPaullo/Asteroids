using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class AnimationHandler : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float cameraShakeIntensity = .4f;
    [SerializeField] private float cameraShakeDuration = .4f;

    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI lifeText;

    [Header("Fade Settings")]
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 1f;

    [Header("Scale Settings")]
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector3 scalePunch = new Vector3(0.2f, 0.2f, 0f);
    [SerializeField] private float scaleDuration = 0.3f;

    [Header("Pulse Settings")]
    [SerializeField] private float pulseDuration = 0.5f;
    [SerializeField] private float pulseAmplitude = 0.2f;

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
        AnimateTextChange(lifeText, scoreData.currentLives + 1, scoreData.currentLives,
            Color.red, Color.white, 0.75f, "Lives: ");
    }

    private void HandleLevelComplete()
    {
        AnimateTextChange(levelText, scoreData.currentLevel - 1, scoreData.currentLevel,
            Color.green, Color.white, 0.75f, "Level: ");
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

    private void FadeScreen(Color fadeColor)
    {
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.gameObject.SetActive(true);
            fadeCanvasGroup.DOFade(1, fadeDuration).OnComplete(() =>
            {
                fadeCanvasGroup.DOFade(0, fadeDuration).OnComplete(() =>
                {
                    fadeCanvasGroup.gameObject.SetActive(false);
                });
            });
        }
    }

    private void PunchScale(Transform target)
    {
        if (target != null)
        {
            target.DOPunchScale(scalePunch, scaleDuration, 10, 0.5f);
        }
    }

    public void PulseValue(float startValue, float targetValue, System.Action<float> onUpdate)
    {
        DOTween.To(() => startValue, x => onUpdate(x), targetValue, pulseDuration)
            .SetLoops(2, LoopType.Yoyo)
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

}
