using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Ship", menuName = "Ships/New Ship")]
public class Ship : ScriptableObject
{
    [Header("<b>Identification</b>")]
    public int ID;
    public string shipName;
    public string shipDescription;
    [Header("<b>Stats</b>")]
    [Header("Life")]
    [SerializeField, Range(1, 10)] private int shipHealth = 3;
    [SerializeField, Range(1, 3)] private int shipShield = 0;
    [Header("Speed")]
    [SerializeField, Range(50f, 1000f)] private float maxThrustValue = 50f;
    [SerializeField, Range(.5f, 5f)] private float turnSpeed = 1f; //maneuverability?????????
    [SerializeField, Range(1f, 10f)] private float accelerationRate = 5f;
    [SerializeField, Range(1f, 10f)] private float decelerationRate = 5f;
    [SerializeField, Range(0.01f, .1f)] private float decelerationThreshold = 0.01f;
    [Header("Shooting")]
    [SerializeField, Range(0.1f, 1f)] private float shootCooldown = .4f;
    [Header("<b>Ship Sprites</b>")]
    [SerializeField] private Sprite shipSprite;
    [SerializeField] private Sprite[] brokenShipPiecesSprite;
    [SerializeField] private Sprite projectileSprite;
    [Header("<b>Particle System</b>")]
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private ParticleSystem thrustersParticles;
}
