using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [Header("Black Hole Settings")]
    [SerializeField] private float maxSize = 1f;
    [SerializeField] private float growthDuration = 2f;
    [SerializeField] private float moveSpeed = 3f;

    private Vector3 targetDirection;
    private bool isGrowing = true;
    private Vector3 originalScale = Vector3.zero;
    private Coroutine movementCoroutine;

    public void Initialize(Vector3 startPosition)
    {
        ResetBlackHole(startPosition);
        Vector3 randomDirection = new(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
        targetDirection = randomDirection.normalized;
        StartGrowth();
    }

    private void StartGrowth()
    {
        transform.DOScale(maxSize, growthDuration).OnComplete(() =>
        {
            isGrowing = false;
            StartMovement();
        });
    }
    private void ResetBlackHole(Vector3 startPosition)
    {
        transform.localScale = originalScale;

        transform.position = startPosition;

        GetComponent<PointEffector2D>().enabled = true;

        DOTween.Kill(transform);
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
            movementCoroutine = null;
        }
        isGrowing = true;
    }

    private void StartMovement()
    {
        movementCoroutine = StartCoroutine(MoveBlackHole());
    }

    private IEnumerator MoveBlackHole()
    {
        while (!isGrowing)
        {
            transform.Translate(moveSpeed * Time.deltaTime * targetDirection);
            yield return null;
        }
    }
}
