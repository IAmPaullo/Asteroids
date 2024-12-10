using UnityEngine;

public class ShipWeaponSystem : MonoBehaviour
{
    [SerializeField] private GunConfiguration gunConfiguration;
    [SerializeField] private Transform bulletPoolParent;
    private GameObject bulletPrefab;
    private int initialPoolSize = 10;
    private ObjectPool<Bullet> bulletPool;
    private int maxProjectillesPerSecond;
    private float projectileSpeed;
    private Sprite[] projectileSprites;
    //private Sprite projectileSprite;


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

    private void Start()
    {
        bulletPool = new ObjectPool<Bullet>(bulletPrefab, initialPoolSize, bulletPoolParent);
    }

    public void SpawnBullet(Vector3 position, Vector2 direction, float speed = 10f, bool isEnemyBullet = false)
    {
        Bullet bullet = bulletPool.GetObject();
        if (bullet != null)
        {
            bullet.InitializeBullet(this, position, direction, projectileSpeed, isEnemyBullet);
            bullet.gameObject.SetActive(true);
        }
    }

    public void ReturnBulletToPool(Bullet bullet)
    {
        bulletPool.ReturnObject(bullet);
    }
}
