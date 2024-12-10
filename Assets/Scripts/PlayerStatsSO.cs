using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Player/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    [SerializeField] Ship currentShip;
    [Header("Shield")]
    public int maxHealth;
    private int currentHealth;

    [Header("Shield")]
    public int maxShield;
    private int currentShield;


    public bool IsAlive
    {
        get { return currentHealth > 0; }
    }

    //implement later
    private float currentMoney;


    public void RegisterPlayerStats(int maxHealth, int maxShield)
    {
        this.maxHealth = maxHealth;
        this.maxShield = maxShield;
        currentHealth = maxHealth;
        currentShield = maxShield;
        Debug.Log($"Player Stats registered! Health: {maxHealth}, Shield: {maxShield}");
    }
    public void RegisterPlayerStats(Ship currentShip)
    {
        maxHealth = currentShip.ShipHealth;
        maxShield = currentShip.ShipShield;
        currentHealth = maxHealth;
        currentShield = maxShield;
        this.currentShip = currentShip;
        Debug.Log($"Player Stats registered! Ship: {currentShip.ShipName}");
    }


    [ContextMenu("Reset Player Stats")]
    public void ResetPlayerStats()
    {
        maxHealth = currentHealth = 0;
        maxShield = currentShield = 0;
        Debug.Log("Reseted Player Stats!");
    }


    public void Damage()
    {
        if (IsAlive)
            currentHealth--;
        else
            Debug.LogWarning("Player is dead already.");
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
    public void IncreaseCurrentShield()
    {
        currentShield++;
        if (currentShield > maxShield)
            currentShield = maxShield;
    }
    private void DecreaseShield()
    {
        if (currentShield <= 0) return;
        currentShield--;
    }


}
