using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;
    private float lifeTimer;


    private BulletSpawner spawner;
    private Vector2 direction;

    public void InitializeBullet(BulletSpawner spawner, Vector3 position, Vector2 direction)
    {
        this.spawner = spawner;
        this.direction = direction;
        transform.position = position;
        lifeTimer = lifetime;

    }

    private void Update()
    {

        transform.Translate(speed * Time.deltaTime * direction);
        lifeTimer -= Time.deltaTime;

        if (lifeTimer <= 0)
        {
            ReturnToPool();
        }
    }

    private void OnBecameInvisible()
    {
        ReturnToPool();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.TryGetComponent<Asteroid>(out Asteroid asteroid))
        {
            asteroid.BreakAsteroid();
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
        spawner.ReturnBulletToPool(this);
    }
}
