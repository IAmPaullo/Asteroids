using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Misc References")]
    [SerializeField] private Rigidbody2D rigidBody;

    [Header("Size")]
    [SerializeField] private float currentSize;
    [SerializeField] private float minSize = 0.35f;
    [SerializeField] private float maxSize = 1.35f;

    [Header("Settings")]
    [SerializeField] private float speed;
    [SerializeField] private AsteroidSize asteroidSize;


    [Header("Sprites")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] smallAsteroidsSprites;
    [SerializeField] private Sprite[] mediumAsteroidsSprites;
    [SerializeField] private Sprite[] largeAsteroidsSprites;



    public enum AsteroidSize { Large, Medium, Small }

    private AsteroidSpawner spawner; // Refer�ncia ao spawner para pooling

    public void InitializeAsteroid(AsteroidSpawner spawner, AsteroidSize size, float speed, Vector3 spawnPosition)
    {
        this.spawner = spawner;
        this.asteroidSize = size;
        this.speed = speed;

        currentSize = Random.Range(minSize, maxSize);
        transform.localScale = Vector3.one * currentSize;


        transform.position = spawnPosition;
        SetRandomDirection();
    }

    private Sprite SetAsteroidSprite(AsteroidSize size)
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

        spawner.ReturnAsteroidToPool(this);
    }

    private void SpawnChildAsteroids(AsteroidSize size, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            Asteroid child = spawner.GetAsteroidFromPool();
            if (child != null)
            {
                child.InitializeAsteroid(spawner, size, speed, transform.position);
            }
        }
    }

    private void OnBecameInvisible()
    {
        spawner.ReturnAsteroidToPool(this);
    }
}
