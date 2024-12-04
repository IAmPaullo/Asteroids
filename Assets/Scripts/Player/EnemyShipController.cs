using System.Collections;
using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    public enum ShipType { Simple, Advanced }

    [Header("Enemy Ship Settings")]
    [SerializeField] private float colliderEnableDelay = 1f;
    [SerializeField] private int points = 250;
    [SerializeField] private ShipType shipType;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float shootingRate = 2f;
    [SerializeField] private BulletSpawner bulletSpawner;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameEvents gameEvents;
    private float nextShootTime;
    private bool isEnabled;
    private Transform player;

    public void Initialize(Transform player)
    {
        shipType = GetShipType();
        this.player = player;
        isEnabled = true;
        StartCoroutine(EnableCollider());
    }

    private IEnumerator EnableCollider()
    {
        Collider2D collider2D = GetComponent<Collider2D>();
        yield return new WaitForSeconds(colliderEnableDelay);
        collider2D.enabled = true;
    }

    private void Update()
    {
        if (player == null || !isEnabled) return;

        MoveTowardsPlayer();
        ShootHandler();
    }
    private ShipType GetShipType()
    {
        float rnd = Random.value;
        ShipType type = rnd >= 0.55f ? ShipType.Advanced : ShipType.Simple;
        return type;
    }
    private void ShootHandler()
    {
        if (Time.time >= nextShootTime)
        {
            ShootAtPlayer();
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
        Vector2 direction = shipType == ShipType.Advanced ?
            direction = (player.position - transform.position) :
            new(Random.Range(-1f, -1f), Random.Range(-1f, -1f));

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
