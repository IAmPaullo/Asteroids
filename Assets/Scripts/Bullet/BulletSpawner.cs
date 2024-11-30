using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Transform bulletPoolParent;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int initialPoolSize = 10;

    private ObjectPool<Bullet> bulletPool;

    private void Start()
    {
        bulletPool = new ObjectPool<Bullet>(bulletPrefab, initialPoolSize, bulletPoolParent);
    }

    public void SpawnBullet(Vector3 position, Vector2 direction)
    {
        Bullet bullet = bulletPool.GetObject();

        if (bullet != null)
        {
            bullet.InitializeBullet(this, position, direction);
            bullet.gameObject.SetActive(true);
        }
    }

    public void ReturnBulletToPool(Bullet bullet)
    {
        bulletPool.ReturnObject(bullet);
    }
}
