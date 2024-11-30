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
        rigidBody.linearVelocity = transform.up * thrust;
    }
}