using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour
{
    [Header("Input")]
    public InputReader Input;


    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject shipPartPrefab;
    [SerializeField] private Sprite[] shipParts;
    [Header("Movement")]
    [SerializeField, Range(50f, 1000f)] private float maxThrustValue = 1f;
    [SerializeField, Range(50f, 1000f)] private float thrustSpeed = 1f;
    [SerializeField, Range(1f, 3f)] private float turnSpeed = 1f;
    [SerializeField] private float lerpSpeed = 5f;
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
    private float targetRotation;

    private bool IsMobilePlatform
    {
        get
        {
            return UnityEngine.Device.Application.isMobilePlatform;
        }
    }
    #region enable
    private void OnEnable()
    {
        #region Input
        Input.RotateEvent += HandleRotation;
        Input.ThrustEvent += OnThrust;
        Input.StopThrustEvent += OnStopThrust;
        Input.ShootEvent += Shoot;
        #endregion

        gameEvents.OnPlayerDeath += ShipDeath;
        gameEvents.OnGameReset += ShipReset;
    }

    private void OnDisable()
    {
        #region Input
        Input.RotateEvent -= HandleRotation;
        Input.ThrustEvent -= OnThrust;
        Input.StopThrustEvent -= OnStopThrust;
        Input.ShootEvent += Shoot;
        #endregion
        gameEvents.OnPlayerDeath -= ShipDeath;
        gameEvents.OnGameReset -= ShipReset;
    }
    #endregion
    private void Update()
    {
        if (!isAlive) return;
        Rotate();
    }


    private void FixedUpdate()
    {
        if (!isAlive) return;
        if (!isThrusting) return;
        Thrust();

    }
    private void Rotate()
    {
        transform.Rotate(0, 0, targetRotation);
    }
    private void Thrust()
    {
        float lerpValue = isThrusting ? 0f : 1f;
        thrustInputValue = Mathf.Lerp(thrustInputValue, lerpValue, Time.deltaTime * lerpSpeed);
        float thrust = Mathf.Clamp(thrustInputValue * thrustSpeed * Time.deltaTime, 0f, maxThrustValue);
        rigidBody.linearVelocity = transform.up * thrust;
        HandleThrusterAudio(thrust);
    }
    private void OnThrust()
    {
        isThrusting = true;
    }
    private void OnStopThrust()
    {
        isThrusting = false;
    }

    public void Shoot()
    {
        if (!isAlive) return;
        Vector2 direction = transform.up;
        bulletSpawner.SpawnBullet(bulletSpawnPosition.position, direction);
        gameEvents.OnShoot();
    }

    private void HandleRotation(Vector2 rotationInput)
    {
        if (!isAlive) return;
        float inputValue = IsMobilePlatform ? UnityEngine.Input.acceleration.x : rotationInput.x;
        targetRotation = -inputValue * turnSpeed;
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



