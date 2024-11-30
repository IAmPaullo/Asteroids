using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroid Pool Settings")]
    public GameObject asteroidPrefab;
    public int initialPoolSize = 10;
    public int spawnAmount = 4;
    [SerializeField] private Vector2 spawnBounds = Vector2.zero;



    private ObjectPool<Asteroid> asteroidPool;

    private void Start()
    {
        asteroidPool = new ObjectPool<Asteroid>(asteroidPrefab, initialPoolSize);

        for (int i = 0; i < spawnAmount; i++)
        {
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        Asteroid asteroid = asteroidPool.GetObject();

        if (asteroid != null)
        {
            Vector3 spawnPosition = new(
                Random.Range(spawnBounds.x, spawnBounds.y),
                Random.Range(spawnBounds.x, spawnBounds.y),
                0
            );

            asteroid.InitializeAsteroid(this, Asteroid.AsteroidSize.Large, Random.Range(1f, 3f), spawnPosition);
        }
    }

    public void ReturnAsteroidToPool(Asteroid asteroid)
    {
        asteroidPool.ReturnObject(asteroid);
    }

    public Asteroid GetAsteroidFromPool()
    {
        return asteroidPool.GetObject();
    }
}
