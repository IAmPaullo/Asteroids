using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;
    private float lifeTimer;


    private ShipWeaponSystem spawner;
    private Vector2 direction;
    private bool isEnemyBullet;
    [SerializeField] private SpriteRenderer spriteRenderer;


    //implement bullet sprite
    public void InitializeBullet(ShipWeaponSystem spawner, Vector3 position, Vector2 direction, float speed = 10f, bool isEnemyBullet = false)
    {
        this.spawner = spawner;
        this.direction = direction;
        transform.position = position;
        lifeTimer = lifetime;
        this.isEnemyBullet = isEnemyBullet;
        this.speed = speed;
        spriteRenderer.transform.DORotate(new Vector3(0, 0, 360), lifeTimer, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental);
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
        //if (!isEnemyBullet)
        //{
        //    if (collision.TryGetComponent<Asteroid>(out Asteroid asteroid))
        //    {
        //        asteroid.BreakAsteroid();
        //        ReturnToPool();
        //    }
        //    else if (collision.TryGetComponent(out EnemyShipController enemy))
        //    {
        //        enemy.DestroyShip();
        //    }
        //}
        //else
        //{
        //    if (collision.TryGetComponent<Asteroid>(out Asteroid asteroid))
        //    {
        //        asteroid.BreakAsteroid(false);
        //        ReturnToPool();
        //    }
        //    else if (collision.TryGetComponent(out ShipController player))
        //    {
        //        player.Damage();
        //    }
        //}
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
        spawner.ReturnBulletToPool(this);
    }
}
