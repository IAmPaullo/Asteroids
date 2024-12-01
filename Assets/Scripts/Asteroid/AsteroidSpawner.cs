using Unity.VisualScripting;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroid Pool Settings")]
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private int initialPoolSize = 10;
    [SerializeField] private int spawnAmount = 4;
    [SerializeField] private Vector2 spawnBounds = Vector2.zero;
    [Space]
    [SerializeField] private GameEvents gameEvents;
    private ObjectPool<Asteroid> asteroidPool;
    private void OnEnable()
    {
        gameEvents.OnLevelStart += SpawnAsteroids;
    }

    private void OnDisable()
    {
        gameEvents.OnLevelStart -= SpawnAsteroids;
    }
    private void Start()
    {

    }

    private void SpawnAsteroids()
    {
        if (asteroidPool == null)
            asteroidPool = new ObjectPool<Asteroid>(asteroidPrefab, initialPoolSize, this.transform);
        for (int i = 0; i < spawnAmount; i++)
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
