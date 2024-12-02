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

    private void Update()
    {
        HandleMovement();
        Shoot();
    }
    private void FixedUpdate()
    {
        HandleThrust();
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))//trocar pro new input
        {
            Vector2 direction = transform.up;
            bulletSpawner.SpawnBullet(bulletSpawnPosition.position, direction);
            gameEvents.OnShoot();
        }
    }
    private void HandleMovement()
    {
        float rotation = -Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotation);
    }
    private void HandleThrust()
    {
        float thrust = Input.GetAxis("Vertical") * thrustSpeed * Time.deltaTime;
        thrust = thrust < 0 ? 0 : thrust;
        rigidBody.linearVelocity = transform.up * thrust;
        HandleThrusterAudio(thrust);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Asteroid"))
        {
            gameEvents?.PlayerDamaged();
            Debug.Log("Player hit by asteroid");
        }
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
}
