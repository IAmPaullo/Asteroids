using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvents", menuName = "Game/GameEvents")]
public class GameEvents : ScriptableObject
{
    public event Action<int> OnAsteroidDestroyed;
    public event Action OnWrapAround;
    public event Action OnPlayerDamaged;
    public event Action OnLevelUp;
    public event Action HUDUpdate;

    #region Sound Events
    public event Action OnShootSound;
    public event Action OnExplosionSound;
    public event Action OnPlayerDamagedSound;
    #endregion

    public void AsteroidDestroyed(int points)
    {
        OnAsteroidDestroyed?.Invoke(points);
        PlayExplosionSound();
    }
    public void PlayerDamaged()
    {
        OnPlayerDamaged?.Invoke();
    }
    public void LevelUp()
    {
        OnLevelUp?.Invoke();
    }
    public void HUDUpdated()
    {
        HUDUpdate?.Invoke();
    }
    public void WrapAround()
    {
        OnWrapAround?.Invoke();
    }
    public void OnShoot()
    {
        OnShootSound?.Invoke();
    }
    public void PlayExplosionSound()
    {
        OnExplosionSound?.Invoke();
    }
    public void PlayPlayerDamagedSound()
    {
        OnPlayerDamagedSound?.Invoke();
    }
}
