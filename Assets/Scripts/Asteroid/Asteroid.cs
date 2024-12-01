using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Misc References")]
    [SerializeField] private Rigidbody2D rigidBody;


    [Header("Settings")]
    [SerializeField] private float speed;
    [SerializeField] private AsteroidSize asteroidSize;


    [Header("Sprites")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] smallAsteroidsSprites;
    [SerializeField] private Sprite[] mediumAsteroidsSprites;
    [SerializeField] private Sprite[] largeAsteroidsSprites;

    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private int points;

    public enum AsteroidSize { Large, Medium, Small }
    private AsteroidSpawner spawner;

    private bool isDestroyed = false;

    public void InitializeAsteroid(AsteroidSpawner spawner, AsteroidSize size, float speed, Vector3 spawnPosition)
    {
        this.spawner = spawner;
        this.asteroidSize = size;
        this.speed = speed;
        isDestroyed = false;
        spriteRenderer.sprite = GetAsteroidSprite(size);
        transform.position = spawnPosition;
        SetRandomDirection();
    }

    private Sprite GetAsteroidSprite(AsteroidSize size)
    {
        Sprite[] selectedArray = size switch
        {
            AsteroidSize.Large => largeAsteroidsSprites,
            AsteroidSize.Medium => mediumAsteroidsSprites,
            AsteroidSize.Small => smallAsteroidsSprites,
            _ => throw new System.Exception($"Invalid Asteroid Size: {size}")
        };

        if (selectedArray == null || selectedArray.Length == 0)
        {
            throw new System.Exception($"No sprites available for asteroid size: {size}");
        }

        int rnd = Random.Range(0, selectedArray.Length);
        return selectedArray[rnd];
    }

    private void SetRandomDirection()
    {
        Vector2 direction = Random.insideUnitCircle.normalized;
        rigidBody.linearVelocity = direction * speed;
    }

    public void BreakAsteroid()
    {
        isDestroyed = true;
        gameEvents.AsteroidDestroyed(points);

        AsteroidSize? nextSize = asteroidSize switch
        {
            AsteroidSize.Large => AsteroidSize.Medium,
            AsteroidSize.Medium => AsteroidSize.Small,
            AsteroidSize.Small => null,
            _ => throw new System.Exception($"Invalid Asteroid Size: {asteroidSize}")
        };

        if (nextSize != null)
        {
            SpawnChildAsteroids(nextSize.Value, 2);
        }
        gameObject.SetActive(false);
        spawner.ReturnAsteroidToPool(this);

    }

    private void SpawnChildAsteroids(AsteroidSize size, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            Asteroid child = spawner.GetAsteroidFromPool();
            if (child != null)
            {
                Vector3 spawnPosition = transform.position;
                float randomOffset = 0.5f;
                spawnPosition += new Vector3(
                    Random.Range(-randomOffset, randomOffset),
                    Random.Range(-randomOffset, randomOffset),
                    0
                );
                child.InitializeAsteroid(spawner, size, Random.Range(1f, 3f), spawnPosition);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDestroyed) return;
        if (collision.TryGetComponent<Bullet>(out _))
        {
            BreakAsteroid();
        }
    }


}
