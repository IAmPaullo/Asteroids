using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject shipPartPrefab;
    [SerializeField] private Sprite[] shipParts;
    [Header("Movement")]
    [SerializeField] private float thrustSpeed = 1f;
    [SerializeField] private float turnSpeed = 1f;
    [Header("Shooting")]
    [SerializeField] private BulletSpawner bulletSpawner;
    [SerializeField] private Transform bulletSpawnPosition;
    [Header("Game Events")]
    [SerializeField] private GameEvents gameEvents;

    // thrusters
    private float thrustInputValue;
    private bool isThrusterActive;
    private float lastUpdateTime;
    private bool isAlive = true;
    public bool isThrusting;

    private bool IsMobilePlatform
    {
        get
        {
            return UnityEngine.Device.Application.isMobilePlatform;
        }
    }

    private void OnEnable()
    {
        gameEvents.OnPlayerDeath += ShipDeath;
        gameEvents.OnGameReset += ShipReset;
    }

    private void OnDisable()
    {
        gameEvents.OnPlayerDeath -= ShipDeath;
        gameEvents.OnGameReset -= ShipReset;
    }


    private void Update()
    {
        if (!isAlive) return;
        HandleMovement();
        Shoot();
    }
    private void FixedUpdate()
    {
        if (!isAlive) return;

        if (IsMobilePlatform)
        {
            if (isThrusting)
                HandleThrust();
            else
                thrustInputValue = 0f;
        }
        else
            HandleThrust();

    }

    #region Mobile Controls

    public void OnThrustButtonPressed()
    {
        isThrusting = true;
    }
    public void OnThrustButtonReleased()
    {
        isThrusting = false;
    }
    #endregion
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))//trocar pro new input
        {
            Vector2 direction = transform.up;
            bulletSpawner.SpawnBullet(bulletSpawnPosition.position, direction);
            gameEvents.OnShoot();
        }
    }

    public void ShootMobile()
    {
        Vector2 direction = transform.up;
        bulletSpawner.SpawnBullet(bulletSpawnPosition.position, direction);
        gameEvents.OnShoot();
    }

    private void HandleMovement()
    {
        float inputValue = IsMobilePlatform ? Input.acceleration.x : Input.GetAxis("Horizontal");
        float rotation = -inputValue * turnSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotation);
    }
    private void HandleThrust()
    {
        thrustInputValue = IsMobilePlatform ? 0.85f : Input.GetAxis("Vertical");
        Debug.LogError(IsMobilePlatform);
        float thrust = thrustInputValue * thrustSpeed * Time.deltaTime;
        thrust = thrust < 0 ? 0 : thrust;
        rigidBody.linearVelocity = transform.up * thrust;
        HandleThrusterAudio(thrust);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Asteroid"))
        {
            Damage();
        }
    }
    public void Damage()
    {
        gameEvents?.PlayerDamaged();
        Debug.Log("Player hit by asteroid");
    }

    private void HandleThrusterAudio(float thruster)
    {
        if (Time.time - lastUpdateTime > 0.2f)
        {
            if (thruster > 0)
            {
                if (!isThrusterActive)
                {
                    gameEvents.ThrusterStart();
                    isThrusterActive = true;
                }
            }
            else
            {
                if (isThrusterActive)
                {
                    gameEvents.ThrusterStop();
                    isThrusterActive = false;
                }
            }
            lastUpdateTime = Time.time;
        }
    }

    private void ShipDeath()
    {
        isAlive = false;
        gameEvents.ThrusterStop();
        rigidBody.linearVelocity = Vector2.zero;
        rigidBody.angularVelocity = 0f;
        spriteRenderer.enabled = false;
        for (int i = 0; i < shipParts.Length; i++)
        {
            Vector2 rnd = new(Random.Range(-1, 1f), Random.Range(-1, 1f));
            var part = Instantiate(shipPartPrefab, transform.position, Quaternion.identity);
            part.GetComponent<SpriteRenderer>().sprite = shipParts[i];
            part.GetComponent<Rigidbody2D>().AddForce(rnd, ForceMode2D.Impulse);
        }

    }
    private void ShipReset()
    {
        isAlive = true;
    }
}



