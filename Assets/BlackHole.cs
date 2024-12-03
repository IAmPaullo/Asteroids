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
    public void Initialize(Vector3 startPosition)
    {
        GetComponent<PointEffector2D>().enabled = true;
        transform.position = startPosition;
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
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
    private void StartMovement()
    {
        StartCoroutine(MoveBlackHole());
    }
    private IEnumerator MoveBlackHole()
    {
        while (!isGrowing)
        {
            transform.Translate(targetDirection * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
