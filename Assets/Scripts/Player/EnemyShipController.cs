using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    public enum ShipType { Simple, Advanced }

    [Header("Enemy Ship Settings")]
    [SerializeField] private int points = 250;
    [SerializeField] private ShipType shipType;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float shootingRate = 2f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private BulletSpawner bulletSpawner;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameEvents gameEvents;
    private float nextShootTime;
    private bool isEnabled;
    private Transform player;



    public void Initialize(Transform player)
    {
        this.player = player;
        isEnabled = true;
    }

    private void Update()
    {
        if (player == null) return;

        MoveTowardsPlayer();
        ShootHandler();
    }

    private void ShootHandler()
    {
        if (Time.time >= nextShootTime)
        {
            if (shipType == ShipType.Advanced && Vector2.Distance(transform.position, player.position) <= detectionRange)
            {
                ShootAtPlayer();
            }
            else if (shipType == ShipType.Simple)
            {
                ShootAtPlayer();
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        if (shipType == ShipType.Advanced)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += speed * Time.deltaTime * direction;
        }
        else
        {
            transform.Translate(speed * Time.deltaTime * Vector3.left);
        }
    }

    private void ShootAtPlayer()
    {
        var direction = (player.position - transform.position);
        bulletSpawner.SpawnBullet(bulletSpawnPoint.position, direction, true);
        nextShootTime = Time.time + shootingRate;
        gameEvents.OnShoot();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            DestroyShip();
        }
    }

    public void DestroyShip()
    {
        gameEvents.AsteroidDestroyed(points);
        Destroy(gameObject); //TODO implement alien pool??
    }
}
