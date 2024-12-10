using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Player/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    [SerializeField] Ship currentShip;


    [Header("Shield")]
    public int maxHealth;
    public int currentHealth;

    [Header("Shield")]
    public int maxShield;
    public int currentShield;





    //implement later
    private float currentMoney;

}
