using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private BulletSpawner spawner;
    private Vector2 direction;

    public void InitializeBullet(BulletSpawner spawner, Vector3 position, Vector2 direction)
    {
        this.spawner = spawner;
        this.direction = direction;


        transform.position = position;
    }

    private void Update()
    {

        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {

        spawner.ReturnBulletToPool(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.TryGetComponent<Asteroid>(out Asteroid asteroid))
        {
            asteroid.BreakAsteroid();
            gameObject.SetActive(false);
            spawner.ReturnBulletToPool(this);
        }
    }
}
//TODO add bullet lifetime