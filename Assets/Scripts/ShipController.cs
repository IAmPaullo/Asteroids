using System;
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
    private bool isThrusting;
    private float turnDirection;

    private void Update()
    {
        UpdateShipDirection();
        Shoot();

    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))//trocar pro new input
        {
            Vector2 direction = transform.up;
            bulletSpawner.SpawnBullet(bulletSpawnPosition.position, direction);
        }
    }

    private void FixedUpdate()
    {
        MoveShip();
    }

    private void MoveShip()
    {
        if (isThrusting)
            rigidBody.AddForce(transform.up * thrustSpeed);
        if (turnDirection != 0f)
            rigidBody.AddTorque(turnDirection * turnSpeed);
    }

    private void UpdateShipDirection()
    {
        isThrusting = Input.GetKey(KeyCode.UpArrow);
        if (Input.GetKey(KeyCode.LeftArrow))
            turnDirection = 1.0f;
        else if (Input.GetKey(KeyCode.RightArrow))
            turnDirection = -1.0f;
        else
            turnDirection = 0.0f;
    }
}
