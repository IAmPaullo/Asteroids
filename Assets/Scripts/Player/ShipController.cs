using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [Header("Movement")]
    [SerializeField] private float thrustSpeed = 1f;
    [SerializeField] private float turnSpeed = 1f;
    [Header("Shooting")]
    [SerializeField] private BulletSpawner bulletSpawner;
    [SerializeField] private Transform bulletSpawnPosition;
    [Header("Game Events")]
    [SerializeField] private GameEvents gameEvents;

    // thrusters
    private bool isThrusterActive;
    private float lastUpdateTime;
    private bool isAlive = true;
    private bool isMobilePlatform => Application.isMobilePlatform;

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
        HandleThrust();
    }

    #region Mobile Controls
    private void HandleMobileInput()
    {
        float tilt = Input.acceleration.x;
        float rotation = -tilt * turnSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotation);
    }
    private void HandleMobileThrust()
    {
        float thrust = Input.acceleration.y * thrustSpeed * Time.deltaTime;
        thrust = thrust < 0 ? 0 : thrust;
        rigidBody.linearVelocity = transform.up * thrust * thrustSpeed;
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
        float inputValue = isMobilePlatform ? Input.acceleration.x : Input.GetAxis("Horizontal");
        Debug.Log(inputValue);
        float rotation = -inputValue * turnSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotation);
    }
    private void HandleThrust()
    {
        float inputValue = isMobilePlatform ? Input.acceleration.y : Input.GetAxis("Vertical");
        float thrust = inputValue * thrustSpeed * Time.deltaTime;
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
        rigidBody.linearVelocity = Vector2.zero;
        rigidBody.angularVelocity = 0f;
    }
    private void ShipReset()
    {
        isAlive = true;
    }


}



