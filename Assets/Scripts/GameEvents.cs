using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GameEvents", menuName = "Game/GameEvents")]
public class GameEvents : ScriptableObject
{
    public event Action<int> OnAsteroidDestroyed;
    public event Action OnPlayerDamaged;        
    public event Action OnLevelUp;              

    public void AsteroidDestroyed(int points)
    {
        OnAsteroidDestroyed?.Invoke(points); 
    }

    public void PlayerDamaged()
    {
        OnPlayerDamaged?.Invoke(); 
    }

    public void LevelUp()
    {
        OnLevelUp?.Invoke(); 
    }
}
