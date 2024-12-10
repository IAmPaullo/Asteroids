using System;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private PlayerStatsSO playerStats;

    [Header("Debug")]
    private ShipWeaponSystem shipWeaponSystem;
    public GunConfiguration gun; // teste
    public Ship ship;// teste


    private bool isAlive
    {
        get
        {
            return playerStats.IsAlive;
        }
    }



    private void OnEnable()
    {
        if (inputReader != null)
        {
            inputReader.RotateEvent += OnRotateInput;
            inputReader.ShootEvent += OnShootInput;
            inputReader.ThrustEvent += OnThrustInput;
            inputReader.StopThrustEvent += OnStopThrustInput;
        }

        gameEvents.OnPlayerDeath += OnDeath;
        gameEvents.OnGameReset += OnReset;
        gameEvents.OnPlayerDamaged += OnDamage;
        gameEvents.OnPlayerHeal += OnHeal;
        gameEvents.OnShieldIncrease += OnIncreaseShield;
    }

    private void OnDisable()
    {
        if (inputReader != null)
        {
            inputReader.RotateEvent -= OnRotateInput;
            inputReader.ShootEvent -= OnShootInput;
            inputReader.ThrustEvent -= OnThrustInput;
            inputReader.StopThrustEvent -= OnStopThrustInput;
        }
        gameEvents.OnPlayerDeath -= OnDeath;
        gameEvents.OnGameReset -= OnReset;
        gameEvents.OnPlayerHeal -= OnHeal;
        gameEvents.OnShieldIncrease -= OnIncreaseShield;
    }

    private void Awake()
    {
        InitializeShip();
        LoadPlayerStats();
    }

    private void LoadPlayerStats()
    {
        playerStats.RegisterPlayerStats(ship);
    }

    private void InitializeShip()
    {
        var shipWeaponSystem = GetComponent<ShipWeaponSystem>();
        var movement = GetComponent<ShipMovement>();
        var graphics = GetComponentInChildren<ShipGraphics>();
        shipWeaponSystem.InitWeaponSystem(gun);
        movement.ConfigureMovement(ship);
        graphics.InitGraphics(ship.ShipSprite);
    }

    private void OnRotateInput(Vector2 input)
    {
        if (!isAlive) return;
        gameEvents.RotateEvent(input);
    }

    private void OnShootInput()
    {
        if (!isAlive) return;
        gameEvents.OnShoot();
    }

    private void OnThrustInput()
    {
        if (!isAlive) return;
        gameEvents.ThrusterStart();
    }

    private void OnStopThrustInput()
    {
        if (!isAlive) return;
        gameEvents.ThrusterStop();
    }

    private void OnDeath()
    {
        gameEvents.PlayerDeath();
    }
    private void OnDamage()
    {
        if (!isAlive) return;
        playerStats.Damage();
    }
    private void OnHeal(int amount)
    {
        if (!isAlive) return;
        playerStats.Heal(amount);
    }

    private void OnIncreaseShield()
    {
        if (!isAlive) return;
        playerStats.IncreaseCurrentShield();
    }

    private void OnReset()
    {
        playerStats.ResetPlayerStats();
    }
}
