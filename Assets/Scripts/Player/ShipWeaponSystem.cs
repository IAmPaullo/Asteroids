using System;
using UnityEngine;

public class ShipWeaponSystem : MonoBehaviour
{
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private GunConfiguration gunConfiguration;
    [SerializeField] private Transform bulletPoolParent;
    [SerializeField] private Transform bulletSpawnPosition;
    private GameObject bulletPrefab;
    private int initialPoolSize = 10;
    private ObjectPool<Bullet> bulletPool;
    private int maxProjectillesPerSecond;
    private float projectileSpeed;
    private Sprite[] projectileSprites;
    //private Sprite projectileSprite;


    private void OnEnable()
    {
        gameEvents.OnPlayerShoot += Shoot;
    }


    private void OnDisable()
    {
        gameEvents.OnPlayerShoot -= Shoot;

    }
    private void Start()
    {
        bulletPool = new ObjectPool<Bullet>(bulletPrefab, initialPoolSize);
    }
    private void Shoot()
    {
        Vector2 direction = transform.up;
        SpawnBullet(bulletSpawnPosition.position, direction, projectileSpeed, false);
    }


    public void InitWeaponSystem(GunConfiguration gunConfiguration)
    {
        bulletPrefab = gunConfiguration.bulletPrefab;
        maxProjectillesPerSecond = gunConfiguration.maxProjectillesPerSecond;
        projectileSpeed = gunConfiguration.projectileSpeed;
        projectileSprites = gunConfiguration.projectileSprites;
        initialPoolSize = maxProjectillesPerSecond + 2;
        bulletPrefab.GetComponentInChildren<SpriteRenderer>().sprite = projectileSprites[0];
        Debug.LogWarning("Ship Gun loaded");
    }

    public void SpawnBullet(Vector3 position, Vector2 direction, float speed = 10f, bool isEnemyBullet = false)
    {
        Bullet bullet = bulletPool.GetObject();
        if (bullet != null)
        {
            bullet.InitializeBullet(this, position, direction, speed, isEnemyBullet);
            bullet.gameObject.SetActive(true);
        }
    }

    public void ReturnBulletToPool(Bullet bullet)
    {
        bulletPool.ReturnObject(bullet, bulletPoolParent);
    }
}
