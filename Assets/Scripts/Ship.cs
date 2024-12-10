using UnityEngine;

[CreateAssetMenu(fileName = "Gun Configuration", menuName = "Ship/New Ship", order = 0)]
public class Ship : ScriptableObject
{
    [Header("<b>Identification</b>")]
    public int ID;
    public string ShipName;
    public string ShipDescription;

    [Header("<b>Stats</b>")]
    [SerializeField, Range(1, 10)] private int shipHealth = 3;
    [SerializeField, Range(1, 3)] private int shipShield = 0;
    [SerializeField, Range(50f, 1000f)] private float maxThrustValue = 50f;
    [SerializeField, Range(.5f, 5f)] private float turnSpeed = 1f;
    [SerializeField, Range(1f, 10f)] private float accelerationRate = 5f;
    [SerializeField, Range(1f, 10f)] private float decelerationRate = 5f;
    [SerializeField, Range(0.01f, .1f)] private float decelerationThreshold = 0.01f;
    [SerializeField, Range(0.1f, 1f)] private float shootCooldown = .4f;

    [Header("<b>Ship Sprites</b>")]
    [SerializeField] private Sprite shipSprite;
    [SerializeField] private Sprite[] brokenShipPiecesSprite;
    //[SerializeField] private Sprite projectileSprite;

    [Header("<b>Particle System</b>")]
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private ParticleSystem thrustersParticles;



    [Header("Stats")]
    public int ShipHealth => shipHealth;
    public int ShipShield => shipShield;


    [Header("Movement")]
    public float MaxThrustValue => maxThrustValue;
    public float TurnSpeed => turnSpeed;
    public float AccelerationRate => accelerationRate;
    public float DecelerationRate => decelerationRate;
    public float DecelerationThreshold => decelerationThreshold;
    public float ShootCooldown => shootCooldown;



    public Sprite ShipSprite => shipSprite;
    public Sprite[] BrokenShipPiecesSprite => brokenShipPiecesSprite;
}
